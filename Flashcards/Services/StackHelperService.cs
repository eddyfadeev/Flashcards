﻿using Flashcards.Enums;
using Flashcards.Interfaces.View.Factory;

namespace Flashcards.Services;

internal static class StackHelperService
{
    internal static void GetStacks(IMenuCommandFactory<StackMenuEntries> menuCommandFactory)
    {
        var chooseCommand = menuCommandFactory.Create(StackMenuEntries.ChooseStack);
        chooseCommand.Execute();
    }
}