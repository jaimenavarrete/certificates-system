using System.Text;
using System.Text.RegularExpressions;
using CertificatesSystem.Models.Exceptions;
using Spire.Pdf;

namespace CertificatesSystem.Services.Common;

public static class PdfService
{
    public static string[] ExtractStudentsInfoFromPdf(Stream? pdfStream)
    {
        if (pdfStream is null)
            throw new BusinessException("Debe agregar un PDF válido.");

        var pdfText = GetPdfText(pdfStream);
        var cleanPdfText = CleanPdfText(pdfText);
        
        var textLines = cleanPdfText.Split("\n");
        var studentsInfo = new List<string>();

        for (var index = 0; index < textLines.Length; index++)
        {
            if (string.IsNullOrEmpty(textLines[index])) continue;
            
            textLines[index] = textLines[index].Trim();
            
            var words = textLines[index].Split(" ");

            if (!int.TryParse(words[0], out _)) continue;

            var studentDataPosition = words.Length - 2;
            
            var info = words[1..studentDataPosition];
            studentsInfo.Add(string.Join(" ", info));
        }

        return studentsInfo.ToArray();
    }
    
    
    // Get PDF text with Spire.PDF
    // OTHER OPTIONS: PDFTron, PDFPig
    
    private static string GetPdfText(Stream pdfStream)
    {
        var pdfDocument = new PdfDocument();
        pdfDocument.LoadFromStream(pdfStream);
    
        var text = new StringBuilder();
        foreach (PdfPageBase page in pdfDocument.Pages)
        {
            text.Append(page.ExtractText());
        }
        pdfDocument.Close();
    
        return text.ToString();
    }

    private static string CleanPdfText(string pdfText)
    {
        var watermarkRemoved = 
            pdfText.Replace("Evaluation Warning : The document was created with Spire.PDF for .NET.", "");
        var windowsLineBreakChanged = 
            watermarkRemoved.Replace(",", " ,").Replace("\r\n", "\n");
        var whiteSpacesRemoved = 
            Regex.Replace(windowsLineBreakChanged, @"[ ]+", " ");
        
        return whiteSpacesRemoved;
    }
}