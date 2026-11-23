using System;
using System.IO;

namespace SolidRefactoringLab
{
    // 1. Contract (Interface) for OCP
    public interface IReportFormatter
    {
        string FormatReport(string title, string content);
    }

    // 2. Three formatter classes (each implements IReportFormatter)
    public class PdfFormatter : IReportFormatter
    {
        public string FormatReport(string title, string content)
        {
            return $"=== PDF REPORT ===\nTitle: {title}\nContent: {content}";
        }
    }

    public class WordFormatter : IReportFormatter
    {
        public string FormatReport(string title, string content)
        {
            return $"=== WORD REPORT ===\nTitle: {title}\nContent: {content}";
        }
    }

    public class PlainTextFormatter : IReportFormatter
    {
        public string FormatReport(string title, string content)
        {
            return $"{title}\n{content}";
        }
    }

    // 3. SRP: Only builds the formatted report
    public class ReportManager
    {
        private readonly IReportFormatter _formatter;

        // Dependency Injection: we hand ReportManager whichever formatter to use
        public ReportManager(IReportFormatter formatter)
        {
            _formatter = formatter;
        }

        public string GenerateReport(string title, string content)
        {
            return _formatter.FormatReport(title, content);
        }
    }

    // 4. SRP: Only saves text to a file
    public class ReportSaver
    {
        public void SaveToFile(string fileName, string text)
        {
            File.WriteAllText(fileName, text);
            Console.WriteLine($"Saved file: {fileName}");
        }
    }

    // 5. Main method: runs and tests everything
    class Program
    {
        static void Main(string[] args)
        {
            string title = "Weekly Summary";
            string content = "All lab tasks completed on time.";

            ReportSaver saver = new ReportSaver();

            // Test PDF
            ReportManager pdfManager = new ReportManager(new PdfFormatter());
            string pdfReport = pdfManager.GenerateReport(title, content);
            Console.WriteLine(pdfReport);
            saver.SaveToFile("report_pdf.txt", pdfReport);

            Console.WriteLine();

            // Test Word
            ReportManager wordManager = new ReportManager(new WordFormatter());
            string wordReport = wordManager.GenerateReport(title, content);
            whConsole.WriteLine(wordReport);
            saver.SaveToFile("report_word.txt", wordReport);

            Console.WriteLine();

            // Test Plain Text
            ReportManager plainManager = new ReportManager(new PlainTextFormatter());
            string plainReport = plainManager.GenerateReport(title, content);
            Console.WriteLine(plainReport);
            saver.SaveToFile("report_plain.txt", plainReport);
        }
    }
}
