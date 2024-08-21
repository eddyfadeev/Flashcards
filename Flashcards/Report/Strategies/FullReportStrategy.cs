using Flashcards.Interfaces.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Flashcards.Report.Strategies;

internal sealed class FullReportStrategy : ReportStrategyBaseClass<IStudySession>
{
    public override List<IStudySession> Data { get; }

    public override string[] ReportColumns =>
    [
        "Date", "Stack", "Result", "Percentage", "Duration"
    ];
    
    public override string DocumentTitle => "Study History";
    public override PageSize PageSize => PageSizes.A4.Portrait();

    public FullReportStrategy(List<IStudySession> studySessions)
    {
        Data = studySessions;
    }


    public override void ConfigureTable(TableDescriptor table)
    {
        DefineReportColumnsHeader(table);
        DefineReportColumns(table);
    }

    public override void PopulateTable(TableDescriptor table)
    {
        foreach (var studySession in Data)
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