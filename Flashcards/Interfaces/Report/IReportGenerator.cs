using Flashcards.Interfaces.Models;
using QuestPDF.Infrastructure;
using Spectre.Console;

namespace Flashcards.Interfaces.Report;

internal interface IReportGenerator
{
    internal Table GetReportToDisplay(List<IStudySession> studySessions);
    internal IDocument GenerateReportToFile(List<IStudySession> studySessions);
    internal void SaveFullReportToPdf(IDocument pdfDocument);
}