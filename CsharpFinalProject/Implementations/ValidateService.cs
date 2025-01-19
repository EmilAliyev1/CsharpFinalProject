using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using CsharpFinalProject.Data.DTO;

namespace CsharpFinalProject.Implementations;

public static class ValidateService
{
    private static Regex loginRegex = new Regex(@"^(?=.*[A-Za-z0-9]$)[A-Za-z]([A-Za-z\d.-_]){0,19}$");
    private static Regex passwordRegex = new Regex(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%&_])[A-Za-z\d!@#$%&_]{8,16}$");
    private static Regex showroomRegex = new Regex(@"^\w[\w.\-#&\s]*$");
    
    public static bool ValidateLogin(Login_DTO login_DTO)
    {
        return loginRegex.IsMatch(login_DTO.username) && passwordRegex.IsMatch(login_DTO.password);
    }
    
    public static bool ValidateRegister(Register_DTO register_DTO)
    {
        return loginRegex.IsMatch(register_DTO.username) && passwordRegex.IsMatch(register_DTO.password);
    }

    public static bool ValidateRegisterConfirmPassword(Register_DTO register_DTO)
    {
        return register_DTO.password == register_DTO.confirmPassword;
    }

    public static bool ValidateShowroomName(Showroom_DTO showroom_DTO)
    {
        return showroomRegex.IsMatch(showroom_DTO.name);
    }
}