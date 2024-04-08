
using eduforos.Application.Services.Messages;
using eduforos.Contracts.Messages.Request;
using eduforos.Contracts.Messages.Response;
using eduForos.Contracts.User;
using eduForos.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace eduForos.Api.Controllers
{
    [Route("/api/Message/")]

    public class MessageController : ApiController
    {
        private readonly IMessagesService _messagesService;
        public MessageController(IMessagesService messagesService)
        {
            _messagesService = messagesService;
        }

        [HttpPost("CreateMessage")]
        public async Task<IActionResult> CreateMessage([FromForm] CreateMessageRequest request, IFormFile? file)
        {
            Message message = new Message();
            message.Message1 = request.message;
            message.Date = DateTime.Now;
            message.IdForum = request.idForum;
            message.IdUser = request.idUser;


            var response = await _messagesService.AddMessage(message, file);
            if (response)
            {
                return Ok("Message creado correctamente");
            }
            else
            {
                return BadRequest();
            }

            
        }

        [HttpPost("GetMessagesByForum")]

        public IActionResult GetMessagesByForum([FromBody] GetMessagesByForumRequest request)
        {
            var messagesResponse=_messagesService.GetMessagesByForum(request.idForum);

            List<GetMessagesByForumResponse> messages = messagesResponse.ConvertAll(message => new GetMessagesByForumResponse(
                message.Message.IdMessage,
                message.Message.Message1,
                message.Message.UrlFile,
                message.Message.AssetId,
                message.Message.Date,
                message.Message.Calification,
                message.Message.IdForum,
                message.Message.IdUser,
                message.FirtsNameUser+" "+message.SurNameUser
                ));

            return Ok(messages);
        }

        [HttpPost("DeleteMessage")]
        public IActionResult DeleteMessage(DeleteMessageRequest request)
        {
            if (_messagesService.DeleteMessage(request.idMessage))
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
