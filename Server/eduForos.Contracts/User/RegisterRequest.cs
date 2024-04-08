using Microsoft.AspNetCore.Http;

namespace eduForos.Contracts.User;

public record RegisterRequest
(
    string UserDocumentType,
    string UserDocument,
    IFormFile? UrlPhoto,
    string? AssetId,
    string FirstName,
    string Surname,
    string Age,
    string Telephone,
    string InstitutionalEmail,
    string? PersonalEmail,
    string Password,
    string UserType,
    string Profession

);