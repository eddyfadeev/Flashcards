using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;

namespace Flashcards.Services;

internal static class StudySessionsHelpService
{
    internal static void SetStackIdInStudyRepository(IStudySessionsRepository studySessionsRepository, IStack stack)
    {
        if (StackChooserService.CheckStackForNull(stack))
        {
            return;
        }
        studySessionsRepository.StackId = stack.Id;
    }
}