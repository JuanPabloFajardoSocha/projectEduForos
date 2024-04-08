namespace eduForos.Contracts.User.Request;

public record RecoverPasswordRequest(
    
    string UserDocumentType,
    string UserDocument,
    string Email

    );