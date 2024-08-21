using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Report.Strategies;
using Spectre.Console;

namespace Flashcards.View.Report;

internal class AverageYearlyReportView : ReportViewBaseClass<IStackMonthlySessions>
{
    public AverageYearlyReportView(IReportStrategy<IStackMonthlySessions> reportStrategy) : base(reportStrategy)
    {
    }

    private protected override Table PopulateReportTable(Table table)
    {
        foreach (var session in ReportStrategy.Data)
        {
            table.AddRow(
                session.StackName,
                session.January.ToString(),
                session.February.ToString(),
                session.March.ToString(),
                session.April.ToString(),
                session.May.ToString(),
                session.June.ToString(),
                session.July.ToString(),
                session.August.ToString(),
                session.September.ToString(),
                session.October.ToString(),
                session.November.ToString(),
                session.December.ToString()
            );
        }

        return table;
    }
}