using System.Text.Json;
using CsharpFinalProject.Data.DTO.User;
using CsharpFinalProject.Data.Model;
using CsharpFinalProject.Interfaces;

namespace CsharpFinalProject.Implementations;

public class UserService : IUserService
{
    private List<User> _users = new List<User>();
    public User CurrentUser { get; private set; } = new User();

    public UserService(){
        var json = File.ReadAllText("./Data/Users.json");

        if (json.Length > 0)
            _users = JsonSerializer.Deserialize<List<User>>(json)!;
    }

    public User LoginUser(LoginDto loginDto){
        if (!ValidateService.ValidateLogin(loginDto))
            throw new Exception("Invalid login credentials");

        if (_users.Count == 0)
            throw new Exception("No users found");

        foreach (var user in _users)
        {
            if (user.Username == loginDto.username && user.Password == loginDto.password)
            {
                Console.WriteLine("User logged in successfully");
                CurrentUser = user;
                return user;
            }
        }
        throw new Exception("No users found with this username");
    }

    public void RegisterUser(RegisterDto registerDto)
    {
        if (!ValidateService.ValidateRegister(registerDto))
            throw new Exception("Invalid registration credentials");
        if (!ValidateService.ValidateRegisterConfirmPassword(registerDto))
            throw new Exception("The confirm password field does not match with the password!");
            
        string json = File.ReadAllText("./Data/Users.json");

        if (json.Length > 0)
            _users = JsonSerializer.Deserialize<List<User>>(json)!;

        _users.Add(MapToUser(registerDto));

        var jsonString = JsonSerializer.Serialize(_users);

        File.WriteAllText("./Data/Users.json", jsonString);

        Console.WriteLine("User registered successfully");
    }

    private User MapToUser(RegisterDto registerDto) { 
        return new User { 
            Username = registerDto.Username, 
            Password = registerDto.Password
        }; 
    }
}
