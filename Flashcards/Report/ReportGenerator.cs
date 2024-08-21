using Flashcards.Enums;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Report;
using Flashcards.Interfaces.Report.Strategies;
using Flashcards.Interfaces.View.Report;
using Flashcards.Services;
using Flashcards.View.Report;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Spectre.Console;

namespace Flashcards.Report;

/// <summary>
/// Generates reports for study sessions.
/// </summary>
internal class ReportGenerator<TEntity> : IReportGenerator
{
    private readonly IReportStrategy<TEntity> _reportStrategy;
    private readonly ReportType _reportType;

    public ReportGenerator(IReportStrategy<TEntity> reportStrategy, ReportType reportType)
    {
        _reportStrategy = reportStrategy;
        _reportType = reportType;
        SetLicence();
    }

    public Table GetReportToDisplay()
    {
        var reportViewMappings = new Dictionary<(Type, ReportType), Func<IReportView>>
        {
            { (typeof(IStudySession), ReportType.FullReport), () => new FullReportView((IReportStrategy<IStudySession>)_reportStrategy) },
            { (typeof(IStudySession), ReportType.ReportByStack), () => new ReportByStackView((IReportStrategy<IStudySession>)_reportStrategy) },
            { (typeof(IStackMonthlySessions), ReportType.AverageYearlyReport), () => new AverageYearlyReportView((IReportStrategy<IStackMonthlySessions>)_reportStrategy) }
        };
        
        if (!reportViewMappings.TryGetValue((typeof(TEntity), _reportType), out var reportView))
        {
            AnsiConsole.MarkupLine(Messages.Messages.UnsupportedReportTypeMessage);
            GeneralHelperService.ShowContinueMessage();
        }

        return reportView!().GetReportToDisplay();
    }

    public void SaveReportToPdf()
    {
        if (!AskToSaveReport())
        {
            return;
        }

        var pdfDocument = GenerateReportToFile();
        var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        var filePath = Path.Combine(desktopPath, $"{_reportStrategy.DocumentTitle}-{DateTime.Today.ToShortDateString()}.pdf");

        pdfDocument.GeneratePdf(filePath);
        AnsiConsole.MarkupLine($"Saved report to [bold]{filePath}[/]");
    }
    
    private ReportDocument<TEntity> GenerateReportToFile()
    {
        var document = new ReportDocument<TEntity>(_reportStrategy);
        return document;
    }

    private static bool AskToSaveReport()
    {
        var confirm = AnsiConsole.Confirm(Messages.Messages.SaveAsPdfMessage);
        return confirm;
    }

    private static void SetLicence() =>
        QuestPDF.Settings.License = LicenseType.Community;
}