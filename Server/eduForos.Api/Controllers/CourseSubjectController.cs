using eduforos.Application.Services.CourseSubject;
using eduforos.Contracts.Course.Request;
using eduforos.Contracts.Course.Results;
using eduforos.Contracts.CourseSubject.Request;
using eduforos.Contracts.CourseSubject.Response;
using eduForos.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace eduForos.Api.Controllers
{
    [Route("/api/CourseSubject/")]
    public class CourseSubjectController : ApiController
    {
        private readonly ICourseSubjectService _courseSubjectService;

        public CourseSubjectController(ICourseSubjectService courseSubjectService)
        {
            _courseSubjectService = courseSubjectService;
        }

        [HttpPost("AddCourseToSubject")]
        public IActionResult AddCourseToSubject(AddCourseToSubjectRequest request)
        {
            _courseSubjectService.AddCourseToSubject(request.idCourse, request.idSubject);
            return Ok();
        }

        [HttpPost("GetSubjectsByCourse")]
        public IActionResult GetCourseByUser([FromBody] GetSubjectsByCourseRequest request)
        {
            var subjectsResponse = _courseSubjectService.GetSubjectsByCourse(request.idCourse);


            List<GetSubjectsByCourseResponse> Subjects = subjectsResponse.ConvertAll(subject => new GetSubjectsByCourseResponse(
             subject.IdSubject,
             subject.Name
             ));

            return Ok(new
            {
                subjects = Subjects
            });
        }

        [HttpPost("DeleteSubjectToCourse")]
        public IActionResult DeleteUserToCourse([FromBody] DeleteSubjectToCourseRequest request)
        {            
            if (_courseSubjectService.DeleteCourseToSubject(request.idCourse, request.idSubject))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
