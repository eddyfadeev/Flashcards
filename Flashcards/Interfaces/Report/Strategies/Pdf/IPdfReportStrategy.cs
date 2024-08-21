using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace Flashcards.Interfaces.Report.Strategies.Pdf;

internal interface IPdfReportStrategy
{
    internal string DocumentTitle { get; }
    internal PageSize PageSize { get; }
    internal void ConfigureTable(TableDescriptor table);
    internal void PopulateTable(TableDescriptor table);
}