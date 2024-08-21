using Flashcards.Interfaces.Report.Strategies;
using Flashcards.Interfaces.View.Report;
using Spectre.Console;

namespace Flashcards.View.Report;

internal abstract class ReportViewBaseClass<TEntity> : IReportView
{
    private protected IReportStrategy<TEntity> ReportStrategy { get; }
    
    protected ReportViewBaseClass(IReportStrategy<TEntity> reportStrategy)
    {
        ReportStrategy = reportStrategy;
    }

    public Table GetReportToDisplay()
    {
        var table = InitializeReportTable();
        table = PopulateReportTable(table);

        return table;
    }
    
    private Table InitializeReportTable()
    {
        var table = new Table
        {
            Border = TableBorder.Rounded,
            Title = new TableTitle(ReportStrategy.DocumentTitle)
        };
        table.AddColumns(ReportStrategy.ReportColumns);
        
        return table;
    }

    private protected abstract Table PopulateReportTable(Table table);
}