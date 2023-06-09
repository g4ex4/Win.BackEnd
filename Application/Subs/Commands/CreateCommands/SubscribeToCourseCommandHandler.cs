﻿using Application.Interfaces;
using Application.Students.Commands.CreateCommands;
using Domain.Entities;
using Domain.Links;
using Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Subs.Commands.CreateCommands
{
    public class SubscribeToCourseCommandHandler : IRequestHandler<SubscribeToCourseCommand, SubscriptionResponse>
    {
        private readonly IStudentDbContext _studentRepository;
        private readonly ICourseDbContext _courseRepository;
        private readonly ISubDbContext _subDbContext;
        private readonly IStudentSubscriptionDbContext _studentsubDbContext;
        private readonly IStudentCourseDbContext _studentCourseDbContext;
        private readonly ICoursesSubscriptionsDbContext _CourseSubscripDbContext;
        private readonly ILogger<SubscribeToCourseCommandHandler> _logger;

        public SubscribeToCourseCommandHandler(IStudentDbContext studentRepository, ICourseDbContext courseRepository,
            ISubDbContext subscriptionRepository, IStudentSubscriptionDbContext studentSubscriptionDbContext,
            IStudentCourseDbContext studentCourseDbContext, ICoursesSubscriptionsDbContext coursesSubscriptionsDbContext,
            ILogger<SubscribeToCourseCommandHandler> logger)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _subDbContext = subscriptionRepository;
            _studentsubDbContext = studentSubscriptionDbContext;
            _studentCourseDbContext = studentCourseDbContext;
            _CourseSubscripDbContext = coursesSubscriptionsDbContext;
            _logger = logger;
        }

        public async Task<SubscriptionResponse> Handle(SubscribeToCourseCommand command, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.Students.FirstOrDefaultAsync(x => x.Id == command.StudentId);
            if (student == null)
                return new SubscriptionResponse(null, 404, "Student not found", false);

            var course = await _courseRepository.Courses.FirstOrDefaultAsync(x => x.Id == command.CourseId);
            if (course == null)
                return new SubscriptionResponse(null, 404, "Course not found", false);

            int CountSubscribedsStudent = await _studentCourseDbContext.StudentCourses
                .CountAsync(d => d.CourseId == command.CourseId && d.StudentId == command.StudentId);

            if (CountSubscribedsStudent > 0)
                return new SubscriptionResponse(null, 400, "The student is already subscribed to this course", false);

            try
            {
                var subscription = new Subscription
                {
                    StudentId = command.StudentId,
                    CourseId = command.CourseId,
                    DateSubscribed = DateTime.Now,
                    DateTimeAdded = DateTime.Now,
                    DateTimeUpdated = DateTime.Now
                };

                _subDbContext.Subs.Add(subscription);
                await _subDbContext.SaveChangesAsync(cancellationToken);

                var student_subscription = new StudentSubscription
                {
                    StudentId = command.StudentId,
                    SubscriptionId = subscription.Id
                };

                _studentsubDbContext.StudentSubscriptions.Add(student_subscription);
                await _studentsubDbContext.SaveChangesAsync(cancellationToken);

                var student_Coursecription = new StudentCourse
                {
                    StudentId = command.StudentId,
                    CourseId = command.CourseId
                };

                _studentCourseDbContext.StudentCourses.Add(student_Coursecription);
                await _studentCourseDbContext.SaveChangesAsync(cancellationToken);

                var Course_Subscription = new CourseSubscription
                {
                    SubscriptionId = subscription.Id,
                    CourseId = command.CourseId
                };

                _CourseSubscripDbContext.CoursesSubscriptions.Add(Course_Subscription);
                await _CourseSubscripDbContext.SaveChangesAsync(cancellationToken);

                var response = new SubscriptionResponse(subscription, 200, "Student successfully subscribed to the course", true);
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError($"{e}");
                return new SubscriptionResponse(null, 400, $"An error occurred while creating a subscription: {e.Message}", false);
            }
        }
    }
}
