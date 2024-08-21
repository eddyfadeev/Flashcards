using Flashcards.Enums;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Report.Strategies.Pdf;
using Flashcards.Report.Strategies.Pdf;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Flashcards.Report;

/// <summary>
/// Represents a report document for generating study history reports.
/// </summary>
internal class ReportDocument : IDocument
{
    private readonly IPdfReportStrategy _pdfReportStrategy;
    
    private readonly List<IStudySession>? _studySessions;
    private readonly List<IStackMonthlySessions>? _stackMonthlySessions;
    private readonly IYear? _year;

    public ReportDocument(List<IStudySession> studySessions, ReportType reportType)
    {
        _pdfReportStrategy = SetReportStrategy(reportType);
        _studySessions = studySessions;
    }

    public ReportDocument(List<IStackMonthlySessions> stackMonthlySessions, IYear year)
    {
        _stackMonthlySessions = stackMonthlySessions;
        _year = year;
        _pdfReportStrategy = SetReportStrategy(ReportType.AverageYearlyReport);
    }

    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Margin(20);
                page.Header().Text(_pdfReportStrategy.DocumentTitle).AlignCenter();
                page.Size(_pdfReportStrategy.PageSize);
                page.Content()
                    .Border(1)
                    .Table(table =>
                    {
                        _pdfReportStrategy.ConfigureTable(table);
                        _pdfReportStrategy.PopulateTable(table);
                    });
            });
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    
    private IPdfReportStrategy SetReportStrategy(ReportType reportType)
    {
        Dictionary<ReportType, Func<IPdfReportStrategy>> reportTypeStrategies = new()
        {
            { ReportType.FullReport, () => new FullPdfReportStrategy(_studySessions!) },
            { ReportType.AverageYearlyReport, () => new AverageYearlyPdfReportStrategy(_stackMonthlySessions!, _year!) },
            { ReportType.ReportByStack, () => new ByStackPdfReportStrategy(_studySessions!) }
        };
        
        return reportTypeStrategies[reportType]();
    }
}