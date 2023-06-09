﻿using Domain.Entities;

namespace Domain.Responses
{
    public class CourseListResponse : Response
    {
        public IList<Course> Courses { get; set; }

        public CourseListResponse(int statusCode, string message, bool isSuccess)
            : base(statusCode, message, isSuccess)
        {
            Courses = new List<Course>();
        }

        public CourseListResponse(int statusCode, string message, bool isSuccess, IList<Course> courses)
            : base(statusCode, message, isSuccess)
        {
            Courses = courses;
        }
    }
}
