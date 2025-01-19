using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsharpFinalProject.Interfaces;

namespace CsharpFinalProject.CUI;

public class LoginRegisterMenu : IMenu
{
    private List<MenuChoice> _menuChoices = new()
    {
        new() { Id = 1, Description = "Log In" },
        new() { Id = 2, Description = "Create new Accaunt" },
        new() { Id = 3, Description = "Exit the App" },
    };

    public MenuChoice GetMenuChoice()
    {
        var choice = Console.ReadLine();
        if (int.TryParse(choice, out var result))
        {
            return _menuChoices[result - 1];
        }
        return _menuChoices[_menuChoices.Count - 1];
    }

    public void WriteMenu(){
        Console.WriteLine("Please choose an option:");
        foreach (var choice in _menuChoices)
        {
            Console.WriteLine($"{choice.Id}. {choice.Description}");
        }
    }
}
