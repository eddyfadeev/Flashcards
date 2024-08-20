using Flashcards.Enums;
using Flashcards.Interfaces.Models;
using Flashcards.Interfaces.Report;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Spectre.Console;

namespace Flashcards.Report
{
    /// <summary>
    /// Generates reports for study sessions.
    /// </summary>
    internal class ReportGenerator : IReportGenerator
    {
        private static readonly string[] FullReportColumns = ["Date", "Stack", "Result", "Percentage", "Duration"];
        private static readonly string[] AverageYearlyReportColumns =
        [
            "Stack Name", "January", "February", "March", "April", "May", "June", 
            "July", "August", "September", "October", "November", "December"
        ];
        private static readonly string[] ReportByStackColumns = ["Session Date", "Score", "Percentage"];
        private static readonly TableBorder ReportTableBorder = TableBorder.Rounded;

        public ReportGenerator()
        {
            SetLicence();
        }

        public Table GetReportToDisplay(List<IStudySession> studySessions, ReportType reportType) => 
            GenerateReportTable(studySessions, reportType);

        public Table GetReportToDisplay(List<IStackMonthlySessions> stackMonthlySessions, IYear year) =>
            GenerateReportTable(stackMonthlySessions, year);

        public IDocument GenerateReportToFile(List<IStudySession> studySessions, ReportType reportType)
        {
            var document = new ReportDocument(studySessions, reportType);
            return document;
        }

        public IDocument GenerateReportToFile(List<IStackMonthlySessions> stackMonthlySessions, IYear year, ReportType reportType)
        {
            var document = new ReportDocument(stackMonthlySessions, year, reportType);
            return document;
        }

        public void SaveReportToPdf(List<IStudySession> studySessions, ReportType reportType)
        {
            var reportName = reportType == ReportType.FullReport ? "Study Report" : $"Report for {studySessions[0].StackName}";
            if (!AskToSaveReport())
            {
                return;
            }

            var pdfDocument = GenerateReportToFile(studySessions, reportType);
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var filePath = Path.Combine(desktopPath, $"{reportName}-{DateTime.Today.Date.ToShortDateString()}.pdf");

            pdfDocument.GeneratePdf(filePath);
            AnsiConsole.MarkupLine($"Saved report to [bold]{filePath}[/]");
        }

        public void SaveReportToPdf(List<IStackMonthlySessions> stackMonthlySessions, IYear year, ReportType reportType)
        {
            if (!AskToSaveReport())
            {
                return;
            }

            var pdfDocument = GenerateReportToFile(stackMonthlySessions, year, reportType);
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var filePath = Path.Combine(desktopPath, $"Average Yearly Report-{year.ChosenYear}.pdf");

            pdfDocument.GeneratePdf(filePath);
            AnsiConsole.MarkupLine($"Saved report to [bold]{filePath}[/]");
        }

        private static Table GenerateReportTable(List<IStudySession> studySessions, ReportType reportType)
        {
            var table = new Table();

            if (reportType == ReportType.FullReport)
            {
                table = InitializeFullReportTable();
                table = PopulateFullReportTable(table, studySessions);
            }
            else if (reportType == ReportType.ReportByStack)
            {
                table = InitializeReportByStackTable(studySessions);
                table = PopulateReportByStackTable(table, studySessions);
            }
            
            return table;
        }

        private static Table GenerateReportTable(List<IStackMonthlySessions> stackMonthlySessions, IYear year)
        {
            var table = InitializeAverageYearlyReportTable(year);
            table = PopulateAverageYearlyReportTable(table, stackMonthlySessions);
            return table;
        }

        private static Table InitializeFullReportTable()
        {
            const string header = "[bold]Study History[/]";
            var table = new Table().Title(header);
            table.Border = ReportTableBorder;
            table.AddColumns(FullReportColumns);
            return table;
        }

        private static Table InitializeAverageYearlyReportTable(IYear year)
        {
            var header = $"[bold]Average Yearly Report for the {year.ChosenYear}[/]";
            var table = new Table().Title(header);
            table.Border = ReportTableBorder;
            table.AddColumns(AverageYearlyReportColumns);
            return table;
        }

        private static Table InitializeReportByStackTable(List<IStudySession> studySessions)
        {
            var header = $"[bold]Report for {studySessions[0].StackName}[/]";
            var table = new Table().Title(header);
            table.Border = ReportTableBorder;
            table.AddColumns(ReportByStackColumns);
            return table;
        }

        private static Table PopulateFullReportTable(Table table, List<IStudySession> studySessions)
        {
            foreach (var session in studySessions)
            {
                table.AddRow(
                    session.Date.ToShortDateString(),
                    session.StackName!,
                    $"{session.CorrectAnswers} out of {session.Questions}",
                    $"{session.Percentage}%",
                    session.Time.ToString("g")[..7]
                );
            }
            return table;
        }

        private static Table PopulateAverageYearlyReportTable(Table table, List<IStackMonthlySessions> stackMonthlySessions)
        {
            foreach (var session in stackMonthlySessions)
            {
                table.AddRow(
                    session.StackName,
                    session.January.ToString(),
                    session.February.ToString(),
                    session.March.ToString(),
                    session.April.ToString(),
                    session.May.ToString(),
                    session.June.ToString(),
                    session.July.ToString(),
                    session.August.ToString(),
                    session.September.ToString(),
                    session.October.ToString(),
                    session.November.ToString(),
                    session.December.ToString()
                );
            }
            return table;
        }

        private static Table PopulateReportByStackTable(Table table, List<IStudySession> studySessions)
        {
            foreach (var session in studySessions)
            {
                table.AddRow(
                    session.Date.ToShortDateString(),
                    $"{session.CorrectAnswers} out of {session.Questions}",
                    $"{session.Percentage}%"
                );
            }
            return table;
        }

        private static bool AskToSaveReport()
        {
            var confirm = AnsiConsole.Confirm(Messages.Messages.SaveAsPdfMessage);
            return confirm;
        }

        private static void SetLicence() =>
            QuestPDF.Settings.License = LicenseType.Community;
    }
}