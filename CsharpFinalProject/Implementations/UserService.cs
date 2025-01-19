using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using CsharpFinalProject.Data.DTO;
using CsharpFinalProject.Data.Model;
using CsharpFinalProject.Interfaces;

namespace CsharpFinalProject.Implementations;

public class UserService : IUserService
{
    private List<User> _users { get; set; } = new List<User>();

    public UserService(){
        var json = File.ReadAllText("./Data/Users.json");

        if (json.Length > 0)
            _users = JsonSerializer.Deserialize<List<User>>(json)!;
    }

    public User LoginUser(Login_DTO login_DTO){
        if (!ValidateService.ValidateLogin(login_DTO))
            throw new Exception("Invalid login credentials");

        if (_users.Count == 0)
            throw new Exception("No users found");

        foreach (var user in _users)
        {
            if (user.Username == login_DTO.username && user.Password == login_DTO.password)
            {
                Console.WriteLine("User logged in successfully");
                return user;
            }
        }
        throw new Exception("Invalid login credentials");
    }

    public void RegisterUser(Register_DTO register_DTO)
    {
        if (!ValidateService.ValidateRegister(register_DTO))
            throw new Exception("Invalid registration credentials");
        if (!ValidateService.ValidateRegisterConfirmPassword(register_DTO))
            throw new Exception("The confirm password field does not match with the password!");
            
        string json = File.ReadAllText("./Data/Users.json");

        if (json.Length > 0)
            _users = JsonSerializer.Deserialize<List<User>>(json)!;

        _users.Add(MapToUser(register_DTO));

        var jsonString = JsonSerializer.Serialize(_users);

        File.WriteAllText("./Data/Users.json", jsonString);

        Console.WriteLine("User registered successfully");
    }

    private User MapToUser(Register_DTO register_DTO) { 
        return new User { 
            Username = register_DTO.username, 
            Password = register_DTO.password,
            ShowroomId = Guid.NewGuid()
        }; 
    }
}
