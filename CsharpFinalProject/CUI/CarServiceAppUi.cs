using CsharpFinalProject.Data.DTO.Showroom;
using CsharpFinalProject.Data.DTO.User;
using CsharpFinalProject.Implementations;

namespace CsharpFinalProject.CUI;

public class CarServiceAppUi
{
    private readonly UserService? _userService = new UserService();
    private readonly ShowroomService? _showroomService = new ShowroomService();
    private readonly GetValueService? _getValueService = new GetValueService();
    private readonly Menu? _menu = new Menu();
    private bool running = true;

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
        new() { Id = 4, Description = "Edit car" },
        new() { Id = 5, Description = "Delete car" },
        new() { Id = 6, Description = "Sell car" },
        new() { Id = 7, Description = "Car sales statistics" },
        new() { Id = 8, Description = "Find car by model"},
        new() { Id = 9, Description = "Show all the cars"},
        new() { Id = 10, Description = "Exit the App" },
    };

    

    public void DisplayMainMenu()
    {
        while(running)
        {
            try
            {
                MenuChoice choice = _menu.MenuOperate(_loginRegisterMenuChoices);
                switch (choice.Id)
                {
                    case 1:
                        Console.Write("Enter your username: ");
                        string usernameLog = _getValueService.GetStringInputValue();

                        Console.Write("Enter your password: "); 
                        string passwordLog = _getValueService.GetStringInputValue();

                        LoginDto loginDto = new LoginDto(usernameLog, passwordLog);

                        _userService.LoginUser(loginDto);

                        DisplayUserMenu();
                        
                        break;
                    case 2:
                        Console.Write("Enter your username: ");
                        string usernameReg = _getValueService.GetStringInputValue();

                        Console.Write("Enter your password: ");
                        string passwordReg = _getValueService.GetStringInputValue();

                        Console.Write("Confirm your password: ");
                        string confirmPasswordReg = _getValueService.GetStringInputValue();

                        RegisterDto registerDto = new RegisterDto(usernameReg, passwordReg, confirmPasswordReg);

                        _userService.RegisterUser(registerDto);

                        DisplayUserMenu();
                        
                        break;
                    case 3:
                        running = false;
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

    public void DisplayUserMenu()
    {
        while (running)
        {
            try
            {
                MenuChoice choice = _menu.MenuOperate(_showroomMenuChoices);
                switch (choice.Id)
                {
                    case 1:
                        Console.Write("Enter the name of the showroom: ");
                        string showroomName = _getValueService.GetStringInputValue();

                        Console.Write("Enter car capacity of the showroom: "); 
                        int carCapacity = _getValueService.GetIntInputValue();

                        ShowroomDto showroomDto = new ShowroomDto(showroomName, carCapacity);
                        _showroomService.CreateShowroom(showroomDto);
                        break;
                    case 2:
                        _showroomService.WriteAllShowrooms();

                        Console.WriteLine("Choose the showroom to switch with the current one");
                        int showroomIndex = _getValueService.GetIntInputValue();

                        _showroomService.SetTheCurrentShowroomIndex(showroomIndex - 1);

                        break;
                    case 3:
                        Console.Write("Enter the brand of the car: ");
                        string makeCreate = _getValueService.GetStringInputValue();

                        Console.Write("Enter the model of the car: ");
                        string modelCreate = _getValueService.GetStringInputValue();

                        Console.Write("Enter the date of the car (dd-MM-yyyy)");
                        string carDateCreate = _getValueService.GetStringInputValue();
                        
                        if (!DateTime.TryParseExact(carDateCreate, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime carDateTimeCreate))
                            throw new Exception("Invalid date input");
                        
                        if(!ValidateService.ValidateCarDateTime(carDateTimeCreate))
                            throw new Exception("Invalid date input (the year of the car must be between 1950 and 2024)");

                        CarDto carDtoCreate = new CarDto(makeCreate, modelCreate, carDateTimeCreate);

                        _showroomService.CreateCar(carDtoCreate);

                        break;
                    case 4:
                        _showroomService.WriteAllCars();

                        Console.Write("Choose the index of the car that you want to Edit: ");
                        int carIndexEdit = _getValueService.GetIntInputValue();
                        

                        Console.Write("Enter the brand of the car: ");
                        string makeEdit = _getValueService.GetStringInputValue();

                        Console.Write("Enter the model of the car: ");
                        string modelEdit = _getValueService.GetStringInputValue();

                        Console.Write("Enter the date of the car (dd-MM-yyyy)");
                        string? carDateEdit = Console.ReadLine();
                        
                        if (!DateTime.TryParseExact(carDateEdit, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime carDateTimeEdit))
                            throw new Exception("Invalid date input");
                        
                        if(!ValidateService.ValidateCarDateTime(carDateTimeEdit))
                            throw new Exception("Invalid year input, the year of the car must be between 1950 and 2025");

                        CarDto carDtoEdit = new CarDto(makeEdit, modelEdit, carDateTimeEdit);

                        _showroomService.EditCar(carIndexEdit - 1, carDtoEdit);

                        break;
                    case 5:
                        _showroomService.WriteAllCars();

                        Console.Write("Choose the index of the car that you want to Edit: ");
                        int carIndexDelete = _getValueService.GetIntInputValue();

                        _showroomService.DeleteCar(carIndexDelete - 1);
                        break;
                    case 6:
                        _showroomService.WriteAllCars();

                        Console.Write("Choose the index of the car that you want to Sell: ");
                        int carIndexSell = _getValueService.GetIntInputValue();
                        
                        
                        Console.Write("Enter the date of the sale (dd-MM-yyyy)");
                        string? saleDate = Console.ReadLine();
                        if (!DateTime.TryParseExact(saleDate, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime saleDateTime))
                            throw new Exception("Invalid date input");

                        Console.Write("Enter the price of the car: ");
                        double price = _getValueService.GetDoubleInputValue();
                        
                        SaleDto saleDto = new SaleDto(saleDateTime, price, carIndexSell - 1);
                        
                        _showroomService.SellCar(saleDto);
                        
                        break;
                    case 7:
                        WriteSortTypes();
                        
                        Console.Write("Choose the sorting type: ");
                        int sortTypeIndex = _getValueService.GetIntInputValue();

                        if(!ValidateService.ValidateSortTypeIndex(sortTypeIndex))
                            throw new Exception("Invalid input");
                        
                        Console.Write("Enter count: ");
                        int count = _getValueService.GetIntInputValue();
                        
                        _showroomService.SortSalesByDate(count, sortTypeIndex);
                        
                        break;
                    case 8:
                        Console.Write("Enter the model of the car: ");
                        string findModelInput = _getValueService.GetStringInputValue();

                        _showroomService.SortCarsByModel(findModelInput);
                        break;
                    case 9:
                        _showroomService.WriteAllCars();

                        break;
                    case 10:
                        running = false;
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

    private void WriteSortTypes()
    {
        Console.WriteLine("1 - Sort by Day");
        Console.WriteLine("2 - Sort by Week");
        Console.WriteLine("3 - Sort by Month");
        Console.WriteLine("4 - Sort by Year");
    }
}