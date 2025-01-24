using CsharpFinalProject.Data.DTO.User;
using CsharpFinalProject.Data.Model;

namespace CsharpFinalProject.Interfaces;

public interface IUserService
{
    User LoginUser(LoginDto login_DTO);
    void RegisterUser(RegisterDto register_DTO);
}