using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        return _menuChoices[_menuChoices.Count - 1];
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
