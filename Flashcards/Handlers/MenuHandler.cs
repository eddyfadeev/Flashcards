using Flashcards.Extensions;
using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.View.Factory;
using Flashcards.View;
using Spectre.Console;

namespace Flashcards.Handlers;

internal class MenuHandler<T> : IMenuHandler<T> where T : Enum
{
    private readonly IMenuCommandFactory<T> _commandFactory;
    private readonly SelectionPrompt<string> _menuEntries;

    public MenuHandler(IMenuEntries<T> menuEntries, IMenuCommandFactory<T> commandFactory)
    {
        _menuEntries = menuEntries.GetMenuEntries();
        _commandFactory = commandFactory;
    }

    public void HandleMenu()
    {
        var userChoice = HandleUserChoice(_menuEntries);
        _commandFactory.Create(userChoice).Execute();
    }

    public T HandleChoosableEntry(SelectionPrompt<string> entries) => HandleUserChoice(entries);

    private static T HandleUserChoice(SelectionPrompt<string> entries) =>
        AnsiConsole.Prompt(entries).GetValueFromDisplayName<T>();
}