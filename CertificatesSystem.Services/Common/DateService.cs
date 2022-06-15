namespace CertificatesSystem.Services.Common;

public static class DateService
{
    private static readonly string[] Months = { "enero", "febrero", "marzo", "abril", "mayo", "junio", "julio", "agosto", "septiembre", "octubre", "noviembre", "diciembre" };

    public static string Convert(DateTime date, bool isUpper)
    {
        var dayInLetters = NumberToLettersService.Convert(date.Date.Day, false);
        var monthInLetters = MonthToLetters(date.Date.Month);
        var yearInLetters = NumberToLettersService.Convert(date.Date.Year, false);

        var dateInLetters = $"{dayInLetters} días del mes de {monthInLetters} del año {yearInLetters}";
        var dateInLettersReplaced = dateInLetters.Replace("  ", " ").Trim();

        return isUpper ? dateInLettersReplaced.ToUpper() : dateInLettersReplaced;
    }

    private static string MonthToLetters(int num)
    {
        var monthInLetters = Months[num - 1];
        return monthInLetters;
    }
}