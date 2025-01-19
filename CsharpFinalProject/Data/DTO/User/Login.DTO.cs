using System.Security.Cryptography.X509Certificates;

namespace CsharpFinalProject.Data.DTO;

public record Login_DTO(
    string username,
    string password
);