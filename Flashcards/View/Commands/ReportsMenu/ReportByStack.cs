using Flashcards.Interfaces.Handlers;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Repositories;
using Flashcards.Interfaces.View.Commands;

namespace Flashcards.View.Commands.ReportsMenu;

internal sealed class ReportByStack : ICommand
{
    private readonly IStacksRepository _stacksRepository;
    private readonly IStudySessionsRepository _studySessionsRepository;
    private readonly IEditableEntryHandler<IStack> _stackEntryHandler;
    
    public ReportByStack(
        IStacksRepository stacksRepository, 
        IStudySessionsRepository studySessionsRepository,
        IEditableEntryHandler<IStack> stackEntryHandler)
    {
        _stacksRepository = stacksRepository;
        _studySessionsRepository = studySessionsRepository;
        _stackEntryHandler = stackEntryHandler;
    }
    
    public void Execute()
    {
        var stacks = _stacksRepository.GetStackNames().ToList();
        var selectedStack = _stackEntryHandler.HandleEditableEntry(stacks);
        
        
    }
}