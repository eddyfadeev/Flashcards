using Flashcards.Interfaces.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Flashcards.Report.Strategies;

internal sealed class ByStackReportStrategy : ReportStrategyBaseClass<IStudySession>
{
    public override List<IStudySession> Data { get; }

    public override string[] ReportColumns =>
        [
            "Session Date", "Score", "Percentage"
        ];
    
    public override string DocumentTitle { get; }
    public override PageSize PageSize => PageSizes.A4.Portrait();

    public ByStackReportStrategy(List<IStudySession> studySessions)
    {
        Data = studySessions;
        DocumentTitle = $"Report for {studySessions[0].StackName}";
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
                $"{ studySession.CorrectAnswers } out of { studySession.Questions }",
                $"{ studySession.Percentage }%"
                );
        }
    }
}