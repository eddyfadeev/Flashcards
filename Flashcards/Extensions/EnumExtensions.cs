using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Flashcards.Extensions;

public static class EnumExtensions
{
    public static string GetDisplayName<T>(this T enumValue) where T : Enum
    {
        var displayName = enumValue.GetType()
            .GetMember(enumValue.ToString())
            .FirstOrDefault()?
            .GetCustomAttribute<DisplayAttribute>()?
            .GetName();

        return displayName ?? enumValue.ToString();
    }

    public static IEnumerable<string> GetDisplayNames<T>() where T : Enum
    {
        return Enum.GetValues(typeof(T)).Cast<T>().Select(e => e.GetDisplayName());
    }

    public static T GetValueFromDisplayName<T>(this string displayName) where T : Enum
    {
        return Enum.GetValues(typeof(T)).Cast<T>().First(e => e.GetDisplayName() == displayName);
    }

    public static bool IsReturnToMainMenu<T>(this T enumValue) where T : Enum
    {
        var isReturnToMainMenu = enumValue.GetDisplayName() == "Return to Main Menu";
        
        return isReturnToMainMenu;
    }
    
    public static bool IsExit<T>(this T enumValue) where T : Enum
    {
        var isExit = enumValue.GetDisplayName() == "Exit";
        
        return isExit;
    }
}