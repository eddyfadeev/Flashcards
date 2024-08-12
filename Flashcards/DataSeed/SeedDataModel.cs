using Flashcards.Models.Entity;

namespace Flashcards.DataSeed;

internal class SeedDataModel
{
    public List<Stack> Stacks { get; set; } = [];
    public List<Flashcard> Flashcards { get; set; } = [];
}