﻿namespace SQLite.Services.Student
{
    using Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IStudentsService
    {
        Task<Student> GetStudentById(int studentId);
        Task<List<Student>> GetAllStudents();
        Task<Student> LookForStudent(string query);
    }
}