using Microsoft.AspNetCore.Http;

namespace eduForos.Contracts.User.Request;

public record EditUserRequest
(
    string UserDocumentType,
    string UserDocument,
	IFormFile? File,
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