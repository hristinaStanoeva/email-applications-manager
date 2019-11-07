using System;

namespace EMS.GmailAPI.Parsers
{
    internal static class DataParser
    {
        internal static DateTime ParseDate(string dateAsString)
        {
            var trimedDate = dateAsString.Split(" +")[0];

            return DateTime.Parse(trimedDate);
        }

        internal static string ParseSenderEmail(string senderAsString)
        {
            var first = senderAsString.IndexOf('<');
            var second = senderAsString.IndexOf('>');

            return senderAsString.Substring(first + 1, second - first - 1);
        }

        internal static string ParseSenderName(string senderAsString)
        {
            var first = senderAsString.IndexOf('<');

            var name = senderAsString.Substring(0, first - 1);
            return name;
        }
    }
}
