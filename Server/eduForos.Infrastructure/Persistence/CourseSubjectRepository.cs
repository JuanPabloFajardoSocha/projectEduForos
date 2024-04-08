using Azure.Core;
using eduforos.Application.Common.Interfaces.Persistence;
using eduForos.Domain.Entities;
using eduForos.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eduforos.Infrastructure.Persistence
{
    internal class CourseSubjectRepository : ICourseSubjectRepository
    {
        private readonly EduForosDbContext _context;
        public CourseSubjectRepository(EduForosDbContext dbContext)
        {
            _context = dbContext;
        }

        public bool AddCourseToSubject(Course course, Subject subject)
        {
            Boolean status = false;
            CourseSubject courseSubject = new CourseSubject();
            courseSubject.IdCourse = course.IdCourse;
            courseSubject.IdSubject = subject.IdSubject;



            var exist = _context.CourseSubjects.FirstOrDefault(cs => cs.IdSubject == subject.IdSubject && cs.IdCourse==course.IdCourse);

            if (exist != null)
            {
                throw new Exception("La asignatura ya esta registrada en el curso");
            }
            else
            {
                try
                {
                    _context.CourseSubjects.Add(courseSubject);
                    status = true;
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }
                finally
                {
                    _context.SaveChanges();
                }
            }

            return status;
        }

        public bool DeleteCourseToSubject(int idCourse, int idSubject)
        {           
            bool status = true;

            CourseSubject courseSubject = _context.CourseSubjects.FirstOrDefault(cs => cs.IdCourse == idCourse && cs.IdSubject == idSubject);

            if (courseSubject != null)
            {
                try
                {
                    _context.CourseSubjects.Remove(courseSubject);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    status = false;
                }
                finally
                {
                    _context.SaveChanges();
                }

            }            

            return status;
        }


        public List<Subject> GetSubjectsByCourse(int idCourse)
        {
            var subjects = _context.CourseSubjects
            .Where(cs => cs.IdCourse == idCourse)
            .Join(_context.Subjects,
                  cs => cs.IdSubject,
                  s => s.IdSubject,
                  (cs, s) => s)
                  .Distinct()
                  .ToList();

            if (subjects.Any())
            {
                return subjects;
            }
            else
            {
                return null;
            }
        }
    }
}
