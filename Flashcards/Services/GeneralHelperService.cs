using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories.Operations;
using Spectre.Console;

namespace Flashcards.Services;

internal static class GeneralHelperService
{
    internal static bool AskForConfirmation()
    {
        var userChoice = AnsiConsole.Confirm(Messages.Messages.DeleteConfirmationMessage);
        
        return userChoice;
    }
    
    internal static void ShowContinueMessage()
    {
        AnsiConsole.MarkupLine(Messages.Messages.AnyKeyToContinueMessage);
        Console.ReadKey();
    }

    internal static void SetStackIdInRepository(IAssignableStackId repository, IStack stack)
    {
        if (CheckForNull(stack))
        {
            return;
        }
        
        repository.StackId = stack.Id;
    }
    
    internal static void SetStackNameInRepository(IAssignableStackName repository, IStack stack)
    {
        if (CheckForNull(stack))
        {
            return;
        }
        
        repository.StackName = stack.Name;
    }

    internal static bool CheckForNull<TEntity>(TEntity? entity) where TEntity : class
    {
        if (entity is not null)
        {
            return false;
        }
        
        AnsiConsole.MarkupLine($"{ Messages.Messages.NullEntityMessage }: { entity }");
        ShowContinueMessage();
        
        return true;
    }
}