namespace eduForos.Contracts.Authentication;

public record LoginRequest(
    string InstitutionalEmail,
    string Password
);