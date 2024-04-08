using eduforos.Contracts.Forum.Request;
using eduForos.Application.Services.ForumServices;
using eduForos.Contracts.Forum.request;
using eduForos.Contracts.Forum.Result;
using eduForos.Contracts.User;
using eduForos.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace eduForos.Api.Controllers;

[Route("/api/Forums")]
public class ForumController : ApiController
{
    private readonly IForumService _forumService;

    public ForumController(IForumService forumService)
    {
        _forumService = forumService;
    }

    [HttpPost("CreateForum")]
    public async Task<ActionResult> CreateForum( [FromForm] CreateForumRequest request)
    {
        
        var forum = new Forum
        {
            Name = request.NameForum,
            Description = request.Description,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            IdSubject=request.IdSubject,
            IdCourse = request.IdCourse,
            IdUser = request.IdUser,    
        };

        var res = await _forumService.CreateForumAsync(forum, request.Photo);
        if (res != null)
        {         

            return Ok("Foro creado exitosamente");
        }
        else
        {
           return BadRequest();
        }
    }

    [HttpPost("ListForums")]
    public IActionResult GetForums([FromBody] GetForumsByUser user)
    {
        var forosResponse = _forumService.GetForumsByUser(user.idUser);
        

        List<GetForumsByUserResponse> Forums = forosResponse.ConvertAll(Foro => new GetForumsByUserResponse(
               Foro.IdForum,
               Foro.Name,
               Foro.Description,
               Foro.UrlPhoto,
               Foro.AssetId,
               Foro.StartDate,
               Foro.EndDate
                ));

        return Ok(Forums);
    }

    [HttpDelete("DeleteForum/{idForum}")]
    public async Task<ActionResult> DeleteForum(int idForum)
    {
        var res = await _forumService.DeleteForum(idForum);

        return Ok(new {mesage = "El foro " + res + " se se elimino"});
    }

    [HttpGet("GetForumsStudent/{idUser}")]
    public IActionResult GetForumsByCourse(Guid idUser)
    {
        var forosResponse = _forumService.GetForumsByCourse(idUser);

        List<GetForumsByUserResponse> Forums = forosResponse.ConvertAll(Foro => new GetForumsByUserResponse(
               Foro.IdForum,
               Foro.Name,
               Foro.Description,
               Foro.UrlPhoto,
               Foro.AssetId,
               Foro.StartDate,
               Foro.EndDate
                ));

        return Ok(Forums);
    }




}