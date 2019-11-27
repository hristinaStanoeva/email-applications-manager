using System;

namespace EMS.GmailAPI.Parsers
{
    internal static class DataParser
    {
        internal static DateTime ParseDate(long internalDate)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(internalDate).DateTime;
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

            if (first <= 1)
            {
                return senderAsString;
            }
            else
            {
            var name = senderAsString.Substring(0, first - 1);
            return name;
            }
        }
    }
}
