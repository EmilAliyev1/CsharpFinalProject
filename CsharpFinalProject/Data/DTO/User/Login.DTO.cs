using System.Security.Cryptography.X509Certificates;

namespace CsharpFinalProject.Data.DTO.User;

public record LoginDto(
    string username,
    string password
);