using eduforos.Contracts.Course.Request;
using eduforos.Contracts.Course.Results;
using eduForos.Application.Services.CourseService;
using eduForos.Application.Services.UserCourseServise;
using eduForos.Contracts.Course;
using eduForos.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace eduForos.Api.Controllers;

[Route("/api/Course/")]
public class CourseController : ApiController
{
    private readonly ICourseService _courseService;
    private readonly IUserCourseService _UserCourse;

    public CourseController(ICourseService courseRepository, IUserCourseService UserCourse)
    {
        _courseService = courseRepository;
        _UserCourse = UserCourse;
    }

    [HttpGet("List")]
    public IActionResult List()
    {
        try
        {
            var courses = _courseService.List();
            List<CourseResult> results = new List<CourseResult>();
            foreach (var course in courses)
            {
                CourseResult data = new CourseResult
                {
                    Id = course.IdCourse,
                    Name = course.Name,
                    Description = course.Description
                };
                results.Add(data);
            }
            return Ok(results);
        }
        catch
        {
            throw new Exception("Error al ingresar listar los cursos");
        }
    }

    [HttpPost("Create")]
    public IActionResult CreateCourse(CreateRequest request)
    {
        var course = new Course()
        {
            Name = request.Name,
            Description = request.Description
        };

        _courseService.AddCourse(course);
        return Ok("El curso " + request.Name + " ha sido creado");

    }

    [HttpGet("course/{id}")]
    public IActionResult GetCourseById(int id)
    {
        var course = _courseService.GetCourseById(id);
        if (course == null)
        {
            return StatusCode(StatusCodes.Status400BadRequest);
        }
        return Ok(course);
    }

    [HttpPut("Update/{id}")]
    public IActionResult UpdateCourse(int id, EditRequest request)
    {
        var data = new Course()
        {
            IdCourse = id,
            Name = request.Name,
            Description = request.Description
        };
        var res = _courseService.UpdateCourse(data);
        UpdateResult result = new UpdateResult(Name: res);
        return Ok("El curso " + result.Name + " ha sido actualizado a " + request.Name);
    }

    [HttpDelete("Delete/{id}")]
    public IActionResult DeleteCourse(int id)
    {
        try
        {
            var res = _courseService.DeleteCourse(id);
            UpdateResult result = new UpdateResult(Name: res.Name);
            return Ok("El curso " + result.Name + " ha sido eliminado");
        }
        catch (Exception e)
        {
            throw new Exception("Error: " + e.Message);
        }
    }

    [HttpPost("addUserToCourse")]
    public IActionResult GetCourseByUser([FromBody] AddUserToCourse userToCourse)
    {
        Guid iduserGuid = new Guid(userToCourse.IdUser);
        _UserCourse.AddParticipant(userToCourse.IdCourse, iduserGuid);
        return Ok();

    }


    [HttpPost("getUsersByCourse")]
    public IActionResult GetUsersByCourse([FromBody] GetUsersByCourseRequest course)
    {
        var usersResponse = _UserCourse.GetUsersByCourse(course.idCourse);


        List<GetUsersByCourseResponse> Users = usersResponse.ConvertAll(user => new GetUsersByCourseResponse(
         user.IdUser,
         user.FirtsName,
         user.SurName,
         user.UserDocument,
         user.UserType));

        return Ok(new
        {
            users = Users
        });
    }


    [HttpPost("getCoursesByUser")]
    public IActionResult GetCoursesByUser([FromBody] GetCoursesByUserRequest request)
    {
        var coursesResponse = _UserCourse.GetCoursesByUser(request.idUser);


        List<GetCoursesByUserResponse> Courses = coursesResponse.ConvertAll(course => new GetCoursesByUserResponse(
         course.IdCourse,
         course.Name,
         course.Description));

        return Ok(new
        {
            courses = Courses
        });
    }



    [HttpPost("DeleteUserToCourse")]    
    public IActionResult DeleteUserToCourse([FromBody] DeleteUserToCourseRequest request)
    {
        if(_UserCourse.DeleteUserToCourse(request.idCourse, request.idUser))
        {
            return Ok();
        }
        else
        {
            return BadRequest();
        }

    }
}