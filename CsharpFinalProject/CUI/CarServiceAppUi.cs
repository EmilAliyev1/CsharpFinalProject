using CsharpFinalProject.Data.DTO.Showroom;
using CsharpFinalProject.Data.DTO.User;
using CsharpFinalProject.Implementations;

namespace CsharpFinalProject.CUI;

public class CarServiceAppUi
{
    private readonly UserService? _userService = new UserService();
    private readonly ShowroomService? _showroomService = new ShowroomService();
    private readonly Menu? _menu = new Menu();
    private bool flag = true;

    private List<MenuChoice> _loginRegisterMenuChoices = new()
    {
        new() { Id = 1, Description = "Log In" },
        new() { Id = 2, Description = "Create new Account" },
        new() { Id = 3, Description = "Exit the App" },
    };

    private List<MenuChoice> _showroomMenuChoices = new()
    {
        new() { Id = 1, Description = "Create new showroom" },
        new() { Id = 2, Description = "Switch the current showroom" },
        new() { Id = 3, Description = "Create car" },
        new() { Id = 4, Description = "Sell car" },
        new() { Id = 5, Description = "Car sales statistics" },
        new() { Id = 6, Description = "Exit the App" },
    };

    public void DisplayMainMenu()
    {
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

                        LoginDto loginDto = new LoginDto(usernameLog, passwordLog);

                        _userService.LoginUser(loginDto);

                        DisplayUserMenu();
                        
                        break;
                    case 2:
                        Console.Write("Enter your username: ");
                        string usernameReg = Console.ReadLine();

                        Console.Write("Enter your password: ");
                        string passwordReg = Console.ReadLine();

                        Console.Write("Confirm your password: ");
                        string confirmPasswordReg = Console.ReadLine();

                        RegisterDto registerDto = new RegisterDto(usernameReg, passwordReg, confirmPasswordReg);

                        _userService.RegisterUser(registerDto);

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
                    case 2:
                        _showroomService.WriteAllShowrooms();

                        Console.WriteLine("Choose the showroom to switch with the current one");
                        int.TryParse(Console.ReadLine(), out int showroomIndex);

                        _showroomService.SetTheCurrentShowroomIndex(showroomIndex - 1);

                        break;
                    case 3:
                        Console.Write("Enter the brand of the car: ");
                        string make = Console.ReadLine();

                        Console.Write("Enter the model of the car: ");
                        string model = Console.ReadLine();

                        Console.Write("Enter the date of the car (dd-MM-yyyy)");
                        string date = Console.ReadLine();
                        if (!DateTime.TryParse(date, out DateTime dateTime)){
                            if(!ValidateService.ValidateCarDateTime(dateTime))
                                throw new Exception("Invalid year input, the year of the car must be between 1950 and 2025");
                            throw new Exception("Invalid date input");
                        }

                        CarDto carDto = new CarDto(make, model, dateTime);

                        _showroomService.CreateCar(carDto);

                        break;
                    case 4:
                        
                    case 6:
                        flag = false;
                        Console.WriteLine("Goodbye");
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