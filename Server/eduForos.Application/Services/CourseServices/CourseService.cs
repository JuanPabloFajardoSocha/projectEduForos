using System.Reflection.Metadata;
using eduForos.Application.Common.Interfaces.Persistence;
using eduForos.Domain.Entities;

namespace eduForos.Application.Services.CourseService;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _courseRepository;

    public CourseService(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public List<Course> List()
    {
        var result = _courseRepository.List();
        if (result.Count == 0)
        {
            throw new Exception("No hay cursos registrados");
        }
        else
        {
            return result;
        }
    }

    public void AddCourse(Course course)
    {

        if (string.IsNullOrEmpty(course.Name))
        {
            throw new Exception("El nombre del curso es requerido");
        }
        else if (course.Description != null)
        {
            if (course.Description.Length > 500)
            {
                throw new Exception("La descripción del curso no puede exceder los 500 caracteres");
            }
        }

        bool result = _courseRepository.Add(course);
        if (result)
        {
            return;
        }
        else
        {
            throw new Exception("El curso ya existe");
        }

    }

    public Course DeleteCourse(int id)
    {
        if (id <= 0)
        {
            throw new Exception("El id del curso es requerido");
        }
        else
        {

            var res = _courseRepository.Delete(id);
            if (res == null)
            {
                throw new Exception("El curso no existe");
            }
            else
            {
                return res;
            }

        }
    }

    public Course? GetCourseById(int id)
    {
        return _courseRepository.GetCourseById(id);
    }

    public string? UpdateCourse(Course course)
    {
        if (course.Name == null)
        {
            throw new Exception("El nombre del curso es requerido");
        }
        else
        {
            string data = _courseRepository.Update(course);
            if (data == null)
            {
                throw new Exception("No existen datos!!!");
            }
            else
            {
                return data;
            }
        }

    }


}