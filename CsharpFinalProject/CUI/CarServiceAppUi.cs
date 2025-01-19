using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsharpFinalProject.Data.DTO;
using CsharpFinalProject.Data.Model;
using CsharpFinalProject.Implementations;

namespace CsharpFinalProject.CUI;

public class CarServiceAppUi
{
    private LoginRegisterMenu _loginRegisterMenu;
    private UserService _userService;

    public void DisplayMainMenu(){
        _loginRegisterMenu = new LoginRegisterMenu();
        _userService = new UserService();

        bool flag = true;

        while(flag){
            try
            {
                _loginRegisterMenu.WriteMenu();
                MenuChoice choice = _loginRegisterMenu.GetMenuChoice();
                switch (choice.Id)
                {
                    case 1:
                        Console.Write("Enter your username: ");
                        string username = Console.ReadLine();

                        Console.Write("Enter your password: "); 
                        string password = Console.ReadLine();

                        Login_DTO login_DTO = new Login_DTO(username, password);

                        _userService.LoginUser(login_DTO);
                        
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
}