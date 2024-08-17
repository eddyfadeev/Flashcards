#if DEBUG
using Flashcards.Models.Entity;

namespace Flashcards.SeedData;

/// <summary>
/// The SeedDataModel class contains two properties: Stacks and Flashcards.
/// These properties are used to store the seed data that will be used to populate the database. 
/// The Stacks property is a list of Stack objects, which represent a collection of flashcards.
/// The Flashcards property is a list of Flashcard objects, which represent individual flashcards. 
/// The SeedDataModel class is internal.
/// </summary>
internal class SeedDataModel
{
    /// <summary>
    /// Represents a stack of flashcards.
    /// </summary>
    internal List<Stack> Stacks { get; set; } = [];

    /// <summary>
    /// Represents a flashcard entity.
    /// </summary>
    internal List<Flashcard> Flashcards { get; set; } = [];
}
#endif