using System;
using System.Collections.Generic;
using System.Linq;

namespace TheMinx
{
    public class ParserUtils
    {
        public static short String_GetShortValue(string raw, short defaultValue)
        {
            short response = defaultValue;

            if (!short.TryParse(raw, out response))
            {
                response = defaultValue;
            }

            return response;
        }

        public static int String_GetIntValue(string raw, int defaultValue)
        {
            int response = defaultValue;

            if (!int.TryParse(raw, out response))
            {
                response = defaultValue;
            }

            return response;
        }

        public static long String_GetLongValue(string raw, long defaultValue)
        {
            long response = defaultValue;

            if (!long.TryParse(raw, out response))
            {
                response = defaultValue;
            }

            return response;
        }

        public static decimal String_GetDecimalValue(string raw, decimal defaultValue)
        {
            decimal response = defaultValue;

            if (!decimal.TryParse(raw.Replace("$", ""), out response))
            {
                response = defaultValue;
            }

            return response;
        }
    }
}
