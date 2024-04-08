using eduForos.Application.Common.Interfaces.Persistence;
using eduForos.Application.Common.Interfaces.Services.Cloudinary;
using eduForos.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eduforos.Application.Services.MessagesService
{
    public class MessageService : IMessagesService
    {
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMessageRepository _messageRepository;
        public MessageService(ICloudinaryService cloudinaryService, IMessageRepository messageRepository)
        {
            _cloudinaryService = cloudinaryService;
            _messageRepository = messageRepository;
        }
        public async Task<bool> CreateMessage(Message message, IFormFile file)
        {


            if (message.Message1.IsNullOrEmpty())
            {
                Console.WriteLine("mensaje---------------------->" + message.Message1);
                

                if (file == null)
                {
                    Console.WriteLine("La imagen es null");
                }
                if (file.ContentType != "image/jpeg" && file.ContentType != "image/png"
                        && file.ContentType != "image/jpg" && file.ContentType != "image/gif")
                {
                    Console.WriteLine("El formato de la imagen no es valido");
                }

                if (file.Length > 1048576)
                {
                    Console.WriteLine("El tamaño de la imagen es muy grande");
                }


                var img = await _cloudinaryService.UploadImageAsync(file, "Messages");
                if (img != null)
                {
                    message.UrlFile = img.Url;
                    message.AssetId = img.AssetId;
                    _messageRepository.CreateMessage(message);
                }
                else
                {
                    Console.WriteLine("No se pudo cargar el archivo a cloud");
                }




            }



            return false;
        }

        public bool DeleteMessage(int idMessage)
        {
            throw new NotImplementedException();
        }

        public List<Message> GetMessagesByForum(int idForum)
        {
            throw new NotImplementedException();
        }
    }
}

