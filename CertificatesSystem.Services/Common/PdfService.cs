using System.Text;
using System.Text.RegularExpressions;
using Spire.Pdf;

namespace CertificatesSystem.Services.Common;

public static class PdfService
{
    public static string[] ExtractStudentsInfoFromPdf(Stream pdfStream)
    {
        var pdfDocument = new PdfDocument();
        pdfDocument.LoadFromStream(pdfStream);

        var text = new StringBuilder();
        foreach (PdfPageBase page in pdfDocument.Pages)
        {
            text.Append(page.ExtractText());
        }
        pdfDocument.Close();

        var textLines = text.Replace("Evaluation Warning : The document was created with Spire.PDF for .NET.", "").Replace(",", " ,").Replace("\r\n","\n").ToString().Split("\n");
        var studentsInfo = new List<string>();

        for (var index = 0; index < textLines.Length; index++)
        {
            textLines[index] = Regex.Replace(textLines[index], @"\s+", " ");
            textLines[index] = textLines[index].Trim();

            if (string.IsNullOrEmpty(textLines[index])) continue;
            
            var words = textLines[index].Split(" ");

            if (!int.TryParse(words[0], out _)) continue;

            var studentDataPosition = words.Length - 2;
            
            var info = words[1..studentDataPosition];
            studentsInfo.Add(string.Join(" ", info));
        }

        return studentsInfo.ToArray();
    }
}