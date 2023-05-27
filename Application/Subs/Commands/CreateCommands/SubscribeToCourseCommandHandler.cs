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

        public SubscribeToCourseCommandHandler(IStudentDbContext studentRepository, ICourseDbContext
            courseRepository, ISubDbContext subscriptionRepository,
            IStudentSubscriptionDbContext studentSubscriptionDbContext, IStudentCourseDbContext studentCourseDbContext)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _subDbContext = subscriptionRepository;
            _studentsubDbContext = studentSubscriptionDbContext;
            _studentCourseDbContext = studentCourseDbContext;

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
            var isAlreadySubscribed = await _subDbContext.Subs
                .Include(pp => pp.StudentSubscription.Where(d => d.StudentId == command.StudentId))
                .Include(o => o.CourseSubscription.Where(f => f.CourseId == command.CourseId))
                .ToListAsync();
            //.AnyAsync(x => x == command.StudentId && x.CourseId == command.CourseId);
            if (isAlreadySubscribed.Count > 0)
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

                 
                    return new Response(200, "Студент успешно подписан на курс", true);
                }
                catch(Exception e)
                {
                    return new Response(400, $"Во время создания подписка произошла ошибка { e.Message }", false);
                }
        }
    }
}
