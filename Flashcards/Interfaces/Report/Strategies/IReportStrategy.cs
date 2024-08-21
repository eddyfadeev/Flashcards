using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Flashcards.Interfaces.Report.Strategies;

internal interface IReportStrategy<TEntity>
{
    internal List<TEntity> Data { get; }
    internal string[] ReportColumns { get; }
    internal string DocumentTitle { get; }
    internal PageSize PageSize { get; }
    internal void ConfigureTable(TableDescriptor table);
    internal void PopulateTable(TableDescriptor table);
}