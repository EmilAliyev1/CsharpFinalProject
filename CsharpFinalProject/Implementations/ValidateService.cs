using System.Text.RegularExpressions;
using CsharpFinalProject.Data.DTO.User;
using CsharpFinalProject.Data.DTO.Showroom;
using CsharpFinalProject.Enums;

namespace CsharpFinalProject.Implementations;

public static class ValidateService
{
    private static Regex loginRegex = new Regex(@"^(?=.*[A-Za-z0-9]$)[A-Za-z]([A-Za-z\d.-_]){0,19}$");
    private static Regex passwordRegex = new Regex(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%&_])[A-Za-z\d!@#$%&_]{8,16}$");
    private static Regex showroomRegex = new Regex(@"^\w[\w.\-#&\s]*$");
    
    public static bool ValidateLogin(LoginDto login_DTO)
    {
        return loginRegex.IsMatch(login_DTO.username) && passwordRegex.IsMatch(login_DTO.password);
    }
    
    public static bool ValidateRegister(RegisterDto register_DTO)
    {
        return loginRegex.IsMatch(register_DTO.Username) && passwordRegex.IsMatch(register_DTO.Password);
    }

    public static bool ValidateRegisterConfirmPassword(RegisterDto register_DTO)
    {
        return register_DTO.Password == register_DTO.ConfirmPassword;
    }

    public static bool ValidateShowroomName(ShowroomDto showroom_DTO)
    {
        return showroomRegex.IsMatch(showroom_DTO.Name);
    }

    public static bool ValidateCarDateTime(DateTime dateTime){
        return dateTime.Year > 1950 && dateTime.Year < DateTime.Now.Year;
    }

    public static bool ValidateSortTypeIndex(int sortTypeIndex)
    {
        return sortTypeIndex > 0 && sortTypeIndex <= (int)SortType.YEAR;
    }
}