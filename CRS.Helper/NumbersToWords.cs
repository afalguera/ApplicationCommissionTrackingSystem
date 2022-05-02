using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRS.Helper
{
    public class NumbersToWords
    {
        public static string DecimalToWords(decimal number)
        {
            long temp = (long)number; //from int to long 
            int decPart = (int)((number - temp) * 100);

            string firstPart = string.Empty;
            string secondpart = string.Empty;

            //Steve 07/11/2012
            //fix negative word in cent
            if (temp < 0 && decPart < 0)
            {
                decPart = Math.Abs(decPart);
            }


            firstPart = ConvertNumberToWord(temp) + " Pesos";
            if (decPart != 0)
                secondpart = " and " + ConvertNumberToWord(decPart) + "Cents";

            return firstPart + secondpart;
        }

        //update param from int to long to handle large number
        public static string ConvertNumberToWord(long numberVal)
        {
            string[] powers = new string[] { "Thousand ", "Million ", 
              "Billion " , "Trillion"};

            string[] ones = new string[] {"One", "Two", "Three", "Four", 
              "Five", "Six", "Seven", "Eight", "Nine", "Ten",
              "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen",
              "Sixteen", "Seventeen", "Eighteen", "Nineteen"};

            string[] tens = new string[] {"Twenty", "Thirty", "Forty", 
             "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"};

            string wordValue = "";

            if (numberVal == 0) return "Zero";
            if (numberVal < 0)
            {
                wordValue = "Negative ";
                numberVal = -numberVal;
            }

            long[] partStack = new long[] { 0, 0, 0, 0 };
            int partNdx = 0;

            while (numberVal > 0)
            {
                partStack[partNdx++] = numberVal % 1000;
                numberVal /= 1000;
            }

            for (int i = 3; i >= 0; i--)
            {
                long part = partStack[i];

                if (part >= 100)
                {
                    wordValue += ones[part / 100 - 1] + " Hundred ";
                    part %= 100;

                    //Steve 01/05/2011 - Fix x00,000 x00,000,000
                    //(ex 800,000 returns Eight hundred instead of Eight hundred thousand)
                    //SOAS Wrong amount in words in Others module CMDM printout
                    if (!(part >= 20) && !(part > 0) && i != 0)
                    {
                        wordValue += powers[i - 1];
                    }

                }

                if (part >= 20)
                {
                    if ((part % 10) != 0) wordValue += tens[part / 10 - 2] +
                       " " + ones[part % 10 - 1] + " ";
                    else wordValue += tens[part / 10 - 2] + " ";
                }
                else if (part > 0) wordValue += ones[part - 1] + " ";

                if (part != 0 && i > 0) wordValue += powers[i - 1];
            }

            return wordValue;
        }

    }
}