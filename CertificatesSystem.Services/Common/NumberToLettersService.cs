namespace CertificatesSystem.Services.Common
{
    public static class NumberToLettersService
    {
        private static readonly string[] Units = { "", "uno", "dos ", "tres ", "cuatro ", "cinco ", "seis ", "siete ", "ocho ", "nueve " };
        private static readonly string[] Tens = { "diez ", "once ", "doce ", "trece ", "catorce ", "quince ", "dieciseis ", "diecisiete ", "dieciocho ", "diecinueve", "veinte ", "treinta ", "cuarenta ", "cincuenta ", "sesenta ", "setenta ", "ochenta ", "noventa " };
        private static readonly string[] Hundreds = {"", "ciento ", "doscientos ", "trecientos ", "cuatrocientos ", "quinientos ", "seiscientos ", "setecientos ", "ochocientos ", "novecientos "};
    
        public static string Convert(int num, bool isUpper) {
            var numberToLetters = num switch
            {
                0 => "cero",
                > 999 => GetThousands(num),
                > 99 => GetHundreds(num),
                > 9 => GetTens(num),
                _ => GetUnits(num)
            };

            return isUpper ? numberToLetters.ToUpper() : numberToLetters;
        }
    
        private static string GetUnits(int num)
        {
            var units = num % 10;
        
            return Units[units];
        }

        private static string GetTens(int num)
        {
            switch (num)
            {
                case <= 9:
                    return Units[num];
                case <= 20:
                    return Tens[num - 10];
                case <= 29:
                    return $"veinti{Units[num - 20]}";
            }

            var units = GetUnits(num);
            if (units.Equals(""))
            {
                return Tens[int.Parse(num.ToString()[..1]) + 8];
            }
        
            return Tens[int.Parse(num.ToString()[..1]) + 8] + "y " + units;
        }

        private static string GetHundreds(int num)
        {
            switch (num)
            {
                case <= 99:
                    return GetTens(num);
                case 100:
                    return " cien ";
                default:
                    return Hundreds[int.Parse(num.ToString()[..1])] + GetTens(int.Parse(num.ToString()[1..]));
            }
        }
    
        private static string GetThousands(int num)
        {
            var numberString = num.ToString();

            //Get the hundreds
            var hundredDigits = numberString[^3..];

            //Get the thousands
            var thousandsDigits = numberString[..^3];

            if (int.Parse(thousandsDigits) > 0)
            {
                var thousands = GetHundreds(int.Parse(thousandsDigits));
                return thousands + " mil " + GetHundreds(int.Parse(hundredDigits));
            }
        
            return "" + GetHundreds(int.Parse(hundredDigits));
        }
    }
}