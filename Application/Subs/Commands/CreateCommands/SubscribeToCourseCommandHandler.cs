using Application.Interfaces;
using Domain.Entities;
using Domain.Links;
using Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace Application.Subs.Commands.CreateCommands
{
    public class SubscribeToCourseCommandHandler : IRequestHandler<SubscribeToCourseCommand, Response>
    {
        private readonly IStudentDbContext _studentRepository;
        private readonly ICourseDbContext _courseRepository;
        private readonly ISubDbContext _subDbContext;
        private readonly IStudentSubscriptionDbContext _studentsubDbContext;
        private readonly IStudentCourseDbContext _studentCourseDbContext;
        private readonly ICoursesSubscriptionsDbContext _CourseSubscripDbContext;

        public SubscribeToCourseCommandHandler(IStudentDbContext studentRepository, ICourseDbContext
            courseRepository, ISubDbContext subscriptionRepository,
            IStudentSubscriptionDbContext studentSubscriptionDbContext,
            IStudentCourseDbContext studentCourseDbContext, 
            ICoursesSubscriptionsDbContext coursesSubscriptionsDbContext)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _subDbContext = subscriptionRepository;
            _studentsubDbContext = studentSubscriptionDbContext;
            _studentCourseDbContext = studentCourseDbContext;
            _CourseSubscripDbContext = coursesSubscriptionsDbContext;

        }

        public async Task<Response> Handle(SubscribeToCourseCommand command, CancellationToken cancellationToken)
        {
            // Проверяем, существуют ли студент и курс
            var student = await _studentRepository.Students.FirstOrDefaultAsync(x => x.Id == command.StudentId);
            if (student == null)
                return new Response(404, "Студент не найден", false);

            var course = await _courseRepository.Courses.FirstOrDefaultAsync(x => x.Id == command.CourseId);
            if (course == null)
                return new Response(404, "Курс не найден", false);


            //// Проверяем, не подписан ли студент уже на этот курс
            int CountSubscribedsStudent = await _studentCourseDbContext.StudentCourses
                .CountAsync(d => d.CourseId == command.CourseId && d.StudentId == command.StudentId);

            if (CountSubscribedsStudent > 0)
                return new Response(400, "Студент уже подписан на этот курс", false);

            try
            {
                var subscription = new Subscription
                {
                    DateSubscribed = DateTime.Now,
                    IsDeleted = false,
                    DateTimeAdded = DateTime.Now,
                    DateTimeUpdated = DateTime.Now
                };

                _subDbContext.Subs.Add(subscription);
                await _subDbContext.SaveChangesAsync(cancellationToken);


                // Создаем запись о подписке студента на курс
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

                    SubscriptionId= subscription.Id,
                    CourseId = command.CourseId

                };

                _CourseSubscripDbContext.CoursesSubscriptions.Add(Course_Subscription);
                await _CourseSubscripDbContext.SaveChangesAsync(cancellationToken);

                return new Response(200, "Студент успешно подписан на курс", true);
            }
            catch(Exception e)
            {
                return new Response(400, $"Во время создания подписка произошла ошибка { e.Message }", false);
            }
        }
    }
}
