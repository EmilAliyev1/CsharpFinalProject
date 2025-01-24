using CsharpFinalProject.CUI;

namespace CsharpFinalProject.Interfaces;

public interface IMenu
{
    MenuChoice GetMenuChoice();
    void WriteMenu();
    public MenuChoice MenuOperate(List<MenuChoice> menuChoices);
}