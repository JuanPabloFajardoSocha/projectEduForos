using eduforos.Application.Services.SubjectService;
using eduforos.Contracts.Subject.Request;
using eduforos.Contracts.Subject.Response;
using eduforos.Contracts.User;
using eduForos.Contracts.User;
using eduForos.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace eduForos.Api.Controllers
{
    [Route("/api/Subject/")]
    public class SubjectsController : ApiController
    {
        private readonly ISubjectService _subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpPost("Register")]
        public IActionResult RegisterSubject([FromBody] SubjectRegisterRequest request)
        {
            var response = _subjectService.RegisterSubject(request.Name);
            return Ok();
            
        }

        [HttpGet("GetSubjects")]
        public IActionResult GetSubjects()
        {
            var response = _subjectService.GetSubjects();

            List<GetSubjectsResponse> Users = response.ConvertAll(Subject => new GetSubjectsResponse(Subject.IdSubject, Subject.Name));

            return Ok(new
            {
                subjects= Users
            });

        }

        [HttpPost("DeleteSubject")]
        public IActionResult DeleteSubjects([FromBody]  SubjectDeleteRequest request)
        {
            _subjectService.DeleteSubject(request.idSubject);
            return Ok();

        }
    }
}
