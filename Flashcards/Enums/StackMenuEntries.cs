﻿using System.ComponentModel.DataAnnotations;

namespace Flashcards.Enums;

internal enum StackMenuEntries
{
    [Display(Name = "View Stacks")] ChooseStack,
    [Display(Name = "Add Stack")] AddStack,
    [Display(Name = "Delete Stack")] DeleteStack,
    [Display(Name = "Edit Stack")] EditStack,

    [Display(Name = "Return to Main Menu")]
    ReturnToMainMenu
}