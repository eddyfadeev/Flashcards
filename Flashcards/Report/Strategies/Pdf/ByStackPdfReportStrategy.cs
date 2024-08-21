using Flashcards.Interfaces.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Flashcards.Report.Strategies.Pdf;

internal sealed class ByStackPdfReportStrategy : PdfReportStrategyBaseClass
{
    private readonly List<IStudySession> _studySessions;

    private protected override string[] ReportColumns =>
        [
            "Session Date", "Score", "Percentage"
        ];
    
    public override string DocumentTitle { get; }
    public override PageSize PageSize => PageSizes.A4.Portrait();

    public ByStackPdfReportStrategy(List<IStudySession> studySessions)
    {
        _studySessions = studySessions;
        DocumentTitle = $"Report for {studySessions[0].StackName}";
    }
    
    public override void ConfigureTable(TableDescriptor table)
    {
        DefineReportColumnsHeader(table);
        DefineReportColumns(table);
    }

    public override void PopulateTable(TableDescriptor table)
    {
        foreach (var studySession in _studySessions)
        {
            AddTableRow(
                table,
                studySession.Date.ToShortDateString(),
                $"{ studySession.CorrectAnswers } out of { studySession.Questions }",
                $"{ studySession.Percentage }%"
                );
        }
    }
}