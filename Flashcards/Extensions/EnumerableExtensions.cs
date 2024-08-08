using Flashcards.Interfaces.Models;

namespace Flashcards.Extensions;

internal static class EnumerableExtensions
{
    internal static List<string> ExtractNamesToList(this List<IStack> stacks) =>
        stacks.Count == 0 ? [] : stacks.Select(stack => stack.Name ?? "Name is not found").ToList();

    internal static List<string> ExtractNamesToList(this List<IFlashcard> flashcards) =>
        flashcards.Count == 0 ? [] : flashcards.Select(flashcard => flashcard.Question ?? "Question is not found").ToList();
}