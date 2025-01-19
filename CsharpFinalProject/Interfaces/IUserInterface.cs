using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsharpFinalProject.Data.DTO;
using CsharpFinalProject.Data.Model;

namespace CsharpFinalProject.Interfaces;

public interface IUserInterface
{
    User LoginUser(Login_DTO loginDto);
    void RegisterUser(Register_DTO registerDto);
}
