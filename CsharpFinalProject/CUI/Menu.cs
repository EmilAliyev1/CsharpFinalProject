namespace CsharpFinalProject.CUI;

public class Menu
{
    private List<MenuChoice> _menuChoices {get; set;} = [];

    private MenuChoice GetMenuChoice()
    {
        var choice = Console.ReadLine();
        if (int.TryParse(choice, out var result))
        {
            return _menuChoices[result - 1];
        }
        throw new Exception("Invalid input");
    }

    private void WriteMenu()
    {
        Console.WriteLine("Please choose an option:");
        foreach (var choice in _menuChoices)
        {
            Console.WriteLine($"{choice.Id}. {choice.Description}");
        }
    }

    public MenuChoice MenuOperate(List<MenuChoice> menuChoices){
        _menuChoices = menuChoices;
        
        WriteMenu();
        MenuChoice choice = GetMenuChoice();

        return choice;
    }
}
