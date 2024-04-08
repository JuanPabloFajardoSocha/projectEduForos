using eduforos.Application.Services.UserServices.Delete;
using eduforos.Application.Services.UserServices.Edit;
using eduforos.Application.Services.UserServices.Get;
using eduforos.Application.Services.UserServices.RecoverPassword;
using eduforos.Application.Services.UserServices.Registration;
using eduforos.Contracts.User;
using eduforos.Contracts.User.Request;
using eduForos.Contracts.User;
using eduForos.Contracts.User.Request;
using eduForos.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace eduForos.Api.Controllers;

[Route("/api/user/")]
public class UserController : ApiController
{
    private readonly IRegisterService _registerService;
    private readonly IGetUsersService _getUsersService;
    private readonly IEditUserService _editService;
    private readonly IDeleteUsers _deleteUsers;
    private readonly IRecoverPassUser _recoverPassUser;

    public UserController(IRegisterService registerService, IGetUsersService getStudentsService, IDeleteUsers DeleteUsers,
        IEditUserService editService, IRecoverPassUser recoverPassUser)
    {
        _registerService = registerService;
        _getUsersService = getStudentsService;
        _deleteUsers = DeleteUsers;
        _editService = editService;
        _recoverPassUser = recoverPassUser;
    }

    [HttpPost("Register")]
    // [ErrorHandlingFilter]
    public async Task<IActionResult> Register([FromForm] RegisterRequest request)
    {

        var registerResult = new User
        {
            UserDocumentType = request.UserDocumentType,
            UserDocument = request.UserDocument,
            // UrlPhoto = request.Photo.FileName,
            FirtsName = request.FirstName,
            SurName = request.Surname,
            Age = request.Age,
            // AssetId = request.Address,
            Telephone = request.Telephone,
            InstitutionalEmail = request.InstitutionalEmail,
            PersonalEmail = request.PersonalEmail,
            Password = request.Password,
            UserType = request.UserType,
            Profession = request.Profession
        };

        var result = await _registerService.Register(registerResult, request.UrlPhoto);

        var response = new RegisterResponse
        (
            registerResult.IdUser,
            registerResult.UserType
        );

        return Ok(response);
    }

    [HttpPost("GetRegisteredUsers")]
    public IActionResult GetRegisteredUsersByRole(GetUsersRequest userType)
    {
        var UsersResponse = _getUsersService.GetRegisteredUsers(userType.UserType);


        if (UsersResponse != null)
        {
            List<GetUsersResponse> Users = UsersResponse.ConvertAll(User => new GetUsersResponse(
                User.IdUser,               
                User.UserDocumentType,
                User.UserDocument,
                 User.UrlPhoto,
                User.FirtsName,
                User.SurName,
                User.Age,               
                User.Telephone,
                User.InstitutionalEmail,
                User.PersonalEmail,               
                User.UserType,
                User.Profession
                ));

            return Ok(new
            {
                Users
            });
        }
        else
        {
            return BadRequest(new
            {
                info = "No hay usuarios registrados con el rol: " + userType,
            });
        }
    }



    [HttpPost("GetUserById")]
    public IActionResult GetUserByUserId(GetUserByIdRequest request)
    {
          
        User User = _getUsersService.GetUserById(request.IdUser);
        if (User != null)
        {
            return Ok(new GetUsersResponse(
                User.IdUser,                
                User.UserDocumentType,
                User.UserDocument,
                User.UrlPhoto,
                User.FirtsName,
                User.SurName,
                User.Age,
                User.Telephone,
                User.InstitutionalEmail,
                User.PersonalEmail,
                User.UserType,
                User.Profession
            ));
        }
        else
        {
            return BadRequest(new
            {
                info = "El usuario no se encuentra registrado"
            });
        }

    }

    [HttpPut("EditUser/{idUser}")]
    public async Task<IActionResult> EditUser(Guid idUser, [FromForm] EditUserRequest request)
    {
        var user = new User
        {
            IdUser = idUser,
            UserDocumentType = request.UserDocumentType,
            UserDocument = request.UserDocument,
            FirtsName = request.FirstName,
            SurName = request.Surname,
            Age = request.Age,
            Telephone = request.Telephone,
            InstitutionalEmail = request.InstitutionalEmail,
            PersonalEmail = request.PersonalEmail,
            Password = request.Password,
            UserType = request.UserType,
            Profession = request.Profession
        };

        await _editService.EditUser(user, request.File);

        return Ok(new
        {
            info = "Usuario " + request.FirstName + " " + request.Surname + " editado correctamente"
        });
    }

    [HttpPost("delete")]
    public IActionResult DeleteUser(DeleteUserRequest IdUser)
    {

        var response = _deleteUsers.DeleteRegisteredUser(IdUser.IdUser);
        if (response)
        {
            return Ok(new DeleteUserResponse(
                "El usuario se ha eliminado exitosamente"
                ));
        }
        else
        {
            return BadRequest(new DeleteUserResponse(
                "Ocurrio un error al eliminar el usuario, Intentelo de nuevo"
                ));
        }

    }

    [HttpPut("RecoverPassword")]
    public IActionResult RecoverPasswordUser(RecoverPasswordRequest request)
    {

        var user = new User
        {
            UserDocumentType = request.UserDocumentType,
            UserDocument = request.UserDocument,
            InstitutionalEmail = request.Email
        };

        bool result = _recoverPassUser.RecoverPassword(user);
        if (result)
        {
            return Ok(new
            {
                info = "Se ha enviado un correo a " + request.Email + " con las instrucciones para recuperar su contraseña"
            });
        }
        else
        {
            return BadRequest(new
            {
                info = "Ocurrio un error al enviar el correo, Intentelo de nuevo"
            });
        }


    }

    [HttpPost("NewPassword/{Token}")]
    public IActionResult NewPassword(string Token, NewPasswordRequest request)
    {

        var user = new User
        {
            Password = request.Password
        };

        var result = _recoverPassUser.NewPassword(user, Token);

        if (!result)
        {
            return BadRequest(new
            {
                info = "Ocurrio un error al actualizar la contraseña, Intentelo de nuevo"
            });
        }

        return Ok(new
        {
            info = "Contraseña actualizada correctamente"
        });







    }


}

