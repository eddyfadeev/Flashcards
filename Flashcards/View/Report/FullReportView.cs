using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Report.Strategies;
using Spectre.Console;

namespace Flashcards.View.Report;

/// <summary>
/// Represents a full report view.
/// </summary>
internal class FullReportView : ReportViewBaseClass<IStudySession>
{
    public FullReportView(IReportStrategy<IStudySession> reportStrategy) : base(reportStrategy)
    {
    }

    private protected override Table PopulateReportTable(Table table)
    {
        foreach (var session in ReportStrategy.Data)
        {
            table.AddRow(
                session.Date.ToShortDateString(),
                session.StackName!,
                $"{session.CorrectAnswers} out of {session.Questions}",
                $"{session.Percentage}%",
                session.Time.ToString("g")[..7]
            );
        }

        return table;
    }
}