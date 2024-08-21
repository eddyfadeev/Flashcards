using Flashcards.Interfaces.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Flashcards.Report.Strategies.Pdf;

internal sealed class AverageYearlyPdfReportStrategy : PdfReportStrategyBaseClass
{
    private readonly List<IStackMonthlySessions> _monthlySessions;

    private protected override string[] ReportColumns =>
        [
            "Stack", "Jan.", "Feb.", "Mar.", 
            "Apr.", "May", "June", "July", 
            "Aug.", "Sept.", "Oct.", "Nov.", "Dec."
        ];
    
    public override string DocumentTitle { get; }
    public override PageSize PageSize => PageSizes.A4.Landscape();

    public AverageYearlyPdfReportStrategy(List<IStackMonthlySessions> monthlySessions, IYear year)
    {
        _monthlySessions = monthlySessions;
        DocumentTitle = $"Average Yearly Report for {year.ChosenYear}";
    }
    public override void ConfigureTable(TableDescriptor table)
    {
        DefineReportColumnsHeader(table);
        DefineReportColumns(table);
    }

    public override void PopulateTable(TableDescriptor table)
    {
        foreach (var monthlySession in _monthlySessions)
        {
            AddTableRow(
                table,
                monthlySession.StackName!,
                monthlySession.January.ToString(),
                monthlySession.February.ToString(),
                monthlySession.March.ToString(),
                monthlySession.April.ToString(),
                monthlySession.May.ToString(),
                monthlySession.June.ToString(),
                monthlySession.July.ToString(),
                monthlySession.August.ToString(),
                monthlySession.September.ToString(),
                monthlySession.October.ToString(),
                monthlySession.November.ToString(),
                monthlySession.December.ToString()
                );
        }
    }
}