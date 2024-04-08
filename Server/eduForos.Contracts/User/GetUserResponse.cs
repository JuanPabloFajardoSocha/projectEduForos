namespace eduForos.Contracts.User;

public record GetUsersResponse(
    Guid Id,
    string UserDocumentType,
    string UserDocument,
    string UrlPhoto,
    string FirtsName,
    string SurName,
    string Age,  
    string Telephone,
    string InstitutionalEmail,
    string PersonalEmail,
    string UserType,
    string Profession    
);


