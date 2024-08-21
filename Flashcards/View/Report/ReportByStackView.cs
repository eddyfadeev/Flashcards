using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Report.Strategies;
using Spectre.Console;

namespace Flashcards.View.Report;

internal class ReportByStackView : ReportViewBaseClass<IStudySession>
{
    public ReportByStackView(IReportStrategy<IStudySession> reportStrategy) : base(reportStrategy)
    {
    }

    private protected override Table PopulateReportTable(Table table)
    {
        foreach (var session in ReportStrategy.Data)
        {
            table.AddRow(
                session.Date.ToShortDateString(),
                $"{session.CorrectAnswers} out of {session.Questions}",
                $"{session.Percentage}%"
            );
        }

        return table;
    }
}