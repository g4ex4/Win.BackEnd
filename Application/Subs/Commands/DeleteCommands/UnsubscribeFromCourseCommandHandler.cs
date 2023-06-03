using Application.Interfaces;
using Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Subs.Commands.DeleteCommands
{
    public class UnsubscribeFromCourseCommandHandler : IRequestHandler<UnsubscribeFromCourseCommand, Response>
    {
        private readonly ISubDbContext _subDbContext;
        private readonly IStudentSubscriptionDbContext _studentsubDbContext;
        private readonly IStudentCourseDbContext _studentCourseDbContext;
        private readonly ICoursesSubscriptionsDbContext _CourseSubscripDbContext;

        public UnsubscribeFromCourseCommandHandler(ISubDbContext subscriptionRepository,
            IStudentSubscriptionDbContext studentSubscriptionDbContext,
            IStudentCourseDbContext studentCourseDbContext,
            ICoursesSubscriptionsDbContext coursesSubscriptionsDbContext)
        {
            _subDbContext = subscriptionRepository;
            _studentsubDbContext = studentSubscriptionDbContext;
            _studentCourseDbContext = studentCourseDbContext;
            _CourseSubscripDbContext = coursesSubscriptionsDbContext;

        }

        public async Task<Response> Handle(UnsubscribeFromCourseCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var studentCourse = await _studentCourseDbContext.StudentCourses
                    .FirstOrDefaultAsync(x => x.StudentId == command.StudentId && x.CourseId == command.CourseId);

                if (studentCourse == null)
                    return new Response(404, "The student is not enrolled in this course", false);

                

                var coursSubs = await _CourseSubscripDbContext.CoursesSubscriptions
                    .FirstOrDefaultAsync(g => g.CourseId == command.CourseId);

                if (coursSubs == null)
                    return new Response(404, "Could not find course subscription record", false);

                var subs = await _subDbContext.Subs
                    .FirstOrDefaultAsync(w => w.Id == coursSubs.SubscriptionId);

                if (subs == null)
                    return new Response(404, "Unable to find course subscription", false);

                

                var studentSubs = await _studentsubDbContext.StudentSubscriptions
                    .FirstOrDefaultAsync(p => p.StudentId == command.StudentId && p.SubscriptionId == coursSubs.SubscriptionId);

                if (studentSubs == null)
                    return new Response(404, "Unable to find student subscription record", false);

                _subDbContext.Subs.Remove(subs);
                await _subDbContext.SaveChangesAsync(cancellationToken);

                _studentCourseDbContext.StudentCourses.Remove(studentCourse);
                await _studentCourseDbContext.SaveChangesAsync(cancellationToken);

                //_studentsubDbContext.StudentSubscriptions.Remove(studentSubs);
                //await _studentsubDbContext.SaveChangesAsync(cancellationToken);

                //_CourseSubscripDbContext.CoursesSubscriptions.Remove(coursSubs);
                //await _CourseSubscripDbContext.SaveChangesAsync(cancellationToken);

                return new Response(200, "Студент успешно отписан от курса", true);
            }
            catch (Exception e)
            {
                return new Response(400, $"Во время отписки от курса произошла ошибка: {e.Message}", false);
            }
        }
    }

}
