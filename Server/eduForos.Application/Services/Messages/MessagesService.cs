using Azure;
using eduforos.Domain.Modelos;
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

namespace eduforos.Application.Services.Messages
{
    internal class MessagesService : IMessagesService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly ICloudinaryService _cloudinaryService;
        public MessagesService(IMessageRepository messageRepository, ICloudinaryService cloudinaryService)
        {
            _messageRepository = messageRepository;
            _cloudinaryService = cloudinaryService;
        }
        public async Task<bool> AddMessage(Message message, IFormFile? file)
        {
            if (message.Message1.IsNullOrEmpty())
            {
                if (file == null)
                {
                    throw new Exception("El mensaje no puede estar vacio");
                }
                else
                {

                    if (file.ContentType != "image/jpeg" && file.ContentType != "image/png"
                        && file.ContentType != "image/jpg" && file.ContentType != "image/gif")
                    {
                        throw new Exception("El formato de la imagen no es valido");
                    }

                    if (file.Length > 1048576)
                    {
                        throw new Exception("El tamaño de la imagen es muy grande");
                    }

                    var img = await _cloudinaryService.UploadImageAsync(file, "Messages");

                    if (img != null)
                    {
                        message.UrlFile = img.Url;
                        message.AssetId = img.AssetId;
                       var response= _messageRepository.CreateMessage(message);
                        if (response)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        
                    }
                    else
                    {
                        throw new Exception("No se subio la imagen a cloud");
                    }                  

                }

            }
            else
            {
               var response= _messageRepository.CreateMessage(message);
                if (response)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                
            }

        }

        public bool DeleteMessage(int idMessage)
        {
            return _messageRepository.DeleteMessage(idMessage);
        }

        public List<MessagesWithUsers> GetMessagesByForum(int idForum)
        {
            var response=_messageRepository.GetMessagesByForum(idForum);
            if (response.Count==0)
            {
                throw new Exception("No hay mensajes en este foro");
            }
            else
            {
                return response;         
            }
        }


    }
}
