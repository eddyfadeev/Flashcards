using Flashcards.Interfaces.Report.Strategies;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Flashcards.Report.Strategies;

internal abstract class ReportStrategyBaseClass : IReportStrategy
{
    private protected abstract string[] ReportColumns { get; }
    
    public abstract string DocumentTitle { get; }

    public abstract PageSize PageSize { get; }
    
    public abstract void ConfigureTable(TableDescriptor table);
    public abstract void PopulateTable(TableDescriptor table);

    private protected void DefineReportColumnsHeader(TableDescriptor table)
    {
        table.Header(header =>
        {
            foreach (var columnName in ReportColumns)
            {
                header.Cell().Padding(5).Text(columnName).Bold();
            }
            
            header
                .Cell()
                .ColumnSpan((uint)ReportColumns.Length)
                .ExtendHorizontal()
                .Height(1)
                .Background(Colors.Black);
        });
    }

    private protected virtual void DefineReportColumns(TableDescriptor table)
    {
        table.ColumnsDefinition(col =>
        {
            foreach (var _ in ReportColumns)
            {
                col.RelativeColumn();
            }
        });
    }

    private protected static void AddTableRow(TableDescriptor table, params string[] rowNames)
    {
        foreach (var rowName in rowNames)
        {
            table.Cell().Element(CellStyle).Text(rowName);
        }
    }
    private protected static IContainer CellStyle(IContainer container) => container.Padding(5);
}
