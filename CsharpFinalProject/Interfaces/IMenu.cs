using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsharpFinalProject.CUI;

namespace CsharpFinalProject.Interfaces;

public interface IMenu
{
    MenuChoice GetMenuChoice();
    void WriteMenu();
    public MenuChoice MenuOperate(List<MenuChoice> menuChoices);
}