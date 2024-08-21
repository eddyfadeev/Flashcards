using Flashcards.Enums;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Report.Strategies;
using Flashcards.Report.Strategies;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Flashcards.Report;

/// <summary>
/// Represents a report document for generating study history reports.
/// </summary>
internal class ReportDocument : IDocument
{
    private readonly IReportStrategy _reportStrategy;
    
    private readonly List<IStudySession>? _studySessions;
    private readonly List<IStackMonthlySessions>? _stackMonthlySessions;
    private readonly IYear? _year;

    public ReportDocument(List<IStudySession> studySessions, ReportType reportType)
    {
        _reportStrategy = SetReportStrategy(reportType);
        _studySessions = studySessions;
    }

    public ReportDocument(List<IStackMonthlySessions> stackMonthlySessions, IYear year)
    {
        _stackMonthlySessions = stackMonthlySessions;
        _year = year;
        _reportStrategy = SetReportStrategy(ReportType.AverageYearlyReport);
    }

    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Margin(20);
                page.Header().Text(_reportStrategy.DocumentTitle).AlignCenter();
                page.Size(_reportStrategy.PageSize);
                page.Content()
                    .Border(1)
                    .Table(table =>
                    {
                        _reportStrategy.ConfigureTable(table);
                        _reportStrategy.PopulateTable(table);
                    });
            });
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    
    private IReportStrategy SetReportStrategy(ReportType reportType)
    {
        Dictionary<ReportType, Func<IReportStrategy>> reportTypeStrategies = new()
        {
            { ReportType.FullReport, () => new FullReportStrategy(_studySessions!) },
            { ReportType.AverageYearlyReport, () => new AverageYearlyReportStrategy(_stackMonthlySessions!, _year!) },
            { ReportType.ReportByStack, () => new ByStackReportStrategy(_studySessions!) }
        };
        
        return reportTypeStrategies[reportType]();
    }
}