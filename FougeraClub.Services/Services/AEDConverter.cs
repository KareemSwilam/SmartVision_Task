using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FougeraClub.Services.Services
{
    public class AEDConverter
    {
        private static readonly string[] ones = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
        private static readonly string[] teens = { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        private static readonly string[] tens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
        private static readonly string[] thousands = { "", "Thousand", "Million", "Billion" };

        public static string ConvertToWords(double amount)
        {
            if (amount < 0)
                return "Negative amounts not supported";

            if (amount == 0)
                return "AED Zero Dirhams";

            // Split into dirhams and fils
            long dirhams = (long)Math.Floor(amount);
            int fils = (int)Math.Round((amount - dirhams) * 100);

            StringBuilder result = new StringBuilder("AED ");

            // Convert dirhams
            if (dirhams == 0)
            {
                result.Append("Zero Dirhams");
            }
            else
            {
                result.Append(ConvertNumberToWords(dirhams));
                result.Append(dirhams == 1 ? " Dirham" : " Dirhams");
            }

            // Convert fils if present
            if (fils > 0)
            {
                result.Append(" and ");
                result.Append(ConvertNumberToWords(fils));
                result.Append(fils == 1 ? " Fil" : " Fils");
            }

            return result.ToString();
        }

        private static string ConvertNumberToWords(long number)
        {
            if (number == 0)
                return "Zero";

            StringBuilder words = new StringBuilder();
            int groupIndex = 0;

            while (number > 0)
            {
                int group = (int)(number % 1000);
                if (group != 0)
                {
                    string groupWords = ConvertGroupToWords(group);
                    if (groupIndex > 0)
                        groupWords += " " + thousands[groupIndex];

                    if (words.Length > 0)
                        words.Insert(0, groupWords + " ");
                    else
                        words.Insert(0, groupWords);
                }
                number /= 1000;
                groupIndex++;
            }

            return words.ToString().Trim();
        }

        private static string ConvertGroupToWords(int number)
        {
            StringBuilder words = new StringBuilder();

            // Hundreds
            int hundreds = number / 100;
            if (hundreds > 0)
            {
                words.Append(ones[hundreds]);
                words.Append(" Hundred");
                if (number % 100 > 0)
                    words.Append(" ");
            }

            // Tens and ones
            int remainder = number % 100;
            if (remainder >= 10 && remainder < 20)
            {
                words.Append(teens[remainder - 10]);
            }
            else
            {
                int tensDigit = remainder / 10;
                int onesDigit = remainder % 10;

                if (tensDigit > 0)
                {
                    words.Append(tens[tensDigit]);
                    if (onesDigit > 0)
                        words.Append("-");
                }

                if (onesDigit > 0)
                    words.Append(ones[onesDigit]);
            }

            return words.ToString();
        }
    }
}
