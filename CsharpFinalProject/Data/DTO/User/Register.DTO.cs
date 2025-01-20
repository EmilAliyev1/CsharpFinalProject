namespace CsharpFinalProject.Data.DTO.User;

public record RegisterDto(
    string Username,
    string Password,
    string ConfirmPassword
);
