using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsharpFinalProject.Data.DTO;
using CsharpFinalProject.Data.DTO.Showroom;
using CsharpFinalProject.Data.DTO.User;
using CsharpFinalProject.Data.Model;
using CsharpFinalProject.Implementations;

namespace CsharpFinalProject.CUI;

public class CarServiceAppUi
{
    private readonly UserService? _userService = new UserService();
    private readonly ShowroomService? _showroomService = new ShowroomService();
    private readonly Menu? _menu = new Menu();

    private List<MenuChoice> _loginRegisterMenuChoices = new()
    {
        new() { Id = 1, Description = "Log In" },
        new() { Id = 2, Description = "Create new Account" },
        new() { Id = 3, Description = "Exit the App" },
    };

    private List<MenuChoice> _showroomMenuChoices = new()
    {
        new() { Id = 1, Description = "Create new showroom" },
        new() { Id = 2, Description = "Create car" },
        new() { Id = 3, Description = "Sell car" },
        new() { Id = 4, Description = "Car sales statistics" },
        new() { Id = 5, Description = "Exit the App" },
    };

    public void DisplayMainMenu()
    {
        bool flag = true;
        
        while(flag)
        {
            MenuChoice choice = _menu.MenuOperate(_loginRegisterMenuChoices);
            try
            {
                switch (choice.Id)
                {
                    case 1:
                        Console.Write("Enter your username: ");
                        string usernameLog = Console.ReadLine();

                        Console.Write("Enter your password: "); 
                        string passwordLog = Console.ReadLine();

                        LoginDto login_DTO = new LoginDto(usernameLog, passwordLog);

                        _userService.LoginUser(login_DTO);

                        DisplayUserMenu();
                        
                        break;
                    case 2:
                        Console.Write("Enter your username: ");
                        string usernameReg = Console.ReadLine();

                        Console.Write("Enter your password: ");
                        string passwordReg = Console.ReadLine();

                        Console.Write("Confirm your password: ");
                        string confirmPasswordReg = Console.ReadLine();

                        RegisterDto register_DTO = new RegisterDto(usernameReg, passwordReg, confirmPasswordReg);

                        _userService.RegisterUser(register_DTO);

                        DisplayUserMenu();
                        
                        break;
                    case 3:
                        flag = false;
                        Console.WriteLine("Goodbye");
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
            catch (Exception e)
            {
                
                Console.WriteLine(e.Message);
            }
        }
    }

    public void DisplayUserMenu()
    {
        bool flag = true;

        while (flag)
        {
            MenuChoice choice = _menu.MenuOperate(_showroomMenuChoices);
            try
            {
                switch (choice.Id)
                {
                    case 1:
                        Console.Write("Enter the name of the showroom: ");
                        string showroomName = Console.ReadLine();

                        Console.Write("Enter car capacity of the showroom: "); 
                        int.TryParse(Console.ReadLine(), out int carCapacity);

                        ShowroomDto showroomDto = new ShowroomDto(showroomName, carCapacity);
                        _showroomService.CreateShowroom(showroomDto);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}