using Flashcards.Interfaces.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Flashcards.Report.Strategies.Pdf;

internal sealed class FullPdfReportStrategy : PdfReportStrategyBaseClass
{
    private readonly List<IStudySession> _studySessions;
    
    private protected override string[] ReportColumns =>
    [
        "Date", "Stack", "Result", "Percentage", "Duration"
    ];
    
    public override string DocumentTitle => "Study History";
    public override PageSize PageSize => PageSizes.A4.Portrait();

    public FullPdfReportStrategy(List<IStudySession> studySessions)
    {
        _studySessions = studySessions;
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
                studySession.StackName!,
                $"{ studySession.CorrectAnswers } out of { studySession.Questions }",
                $"{ studySession.Percentage }%",
                studySession.Time.ToString("g")[..7]);
        }
    }
}