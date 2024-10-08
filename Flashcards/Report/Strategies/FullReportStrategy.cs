﻿using Flashcards.Interfaces.Models;
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

    ///<summary>
    /// Configures the table in the report document with the necessary column headers and formatting.
    /// </summary>
    /// <param name="table">The table descriptor in the report document.</param>
    public override void ConfigureTable(TableDescriptor table)
    {
        DefineReportColumnsHeader(table);
        DefineReportColumns(table);
    }

    /// <summary>
    /// Populates a table with data from a list of study sessions.
    /// </summary>
    /// <param name="table">The table descriptor to populate.</param>
    /// <remarks>
    /// This method retrieves data from a list of study sessions and populates the provided table.
    /// It iterates through each study session and adds a row to the table for each session,
    /// including the session date, score, and percentage.
    /// The table should be properly configured before calling this method.
    /// </remarks>
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