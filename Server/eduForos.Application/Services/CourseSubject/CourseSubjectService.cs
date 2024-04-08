using Azure.Core;
using eduforos.Application.Common.Interfaces.Persistence;
using eduForos.Application.Common.Interfaces.Persistence;
using eduForos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eduforos.Application.Services.CourseSubject
{
    public class CourseSubjectService : ICourseSubjectService
    {
        private readonly ICourseSubjectRepository _courseSubjectRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ICourseRepository _courseRepository;

        public CourseSubjectService(ICourseSubjectRepository courseSubjectRepository, ICourseRepository courseRepository, ISubjectRepository subjectRepository)
        {
            _courseSubjectRepository = courseSubjectRepository;
            _subjectRepository = subjectRepository;
            _courseRepository = courseRepository;
        }


        public bool AddCourseToSubject(int idCourse, int idSubject)
        {
            Subject subject = _subjectRepository.GetSubjectById(idSubject);
            Course course = _courseRepository.GetCourseById(idCourse);
            Boolean status = false;


            if (subject == null)
            {
                throw new Exception("La asignatura no esta registrada");
            }
            else if (course == null)
            {
                throw new Exception("El curso no esta registrado");
            }
            else
            {
                if (_courseSubjectRepository.AddCourseToSubject(course, subject))
                {
                    status = true;
                }
            }

            return status;
        }

        public bool DeleteCourseToSubject(int idCourse, int idSubject)
        {            
            bool status = true;
            var response = _courseSubjectRepository.DeleteCourseToSubject(idCourse,idSubject);
            if (!response) {
                status = false;
                throw new Exception("Error al eliminar el registro");                
            }

            return status;
        }

        public List<Subject> GetSubjectsByCourse(int idCourse)
        {
            var subjects = _courseSubjectRepository.GetSubjectsByCourse(idCourse);
            if (subjects != null)
            {
                return subjects;
            }
            else
            {
                throw new Exception("No hay Asignaturas registradas en este curso");
            }
        }
    }

}
