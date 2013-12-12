using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Xml;

namespace GcmCmnTools
{    
	/// <summary>
	/// Summary description for CmnTools.
	/// </summary>
    public static class CmnTools
    {
        static public bool TryConvertToDouble(object ob, out double d, out string sign)
        {
            string sNumber = string.Empty;
            sign = string.Empty;
            if (ob != null && ob != DBNull.Value)
            {
                sNumber = ob.ToString();
                if (sNumber.StartsWith("<") || sNumber.StartsWith(">"))
                {
                    sign = sNumber.Substring(0, 1);
                    sNumber = sNumber.Substring(1, sNumber.Length - 1);
                }
            }
            return CmnTools.TryConvertToDouble(sNumber, out d);
        }

        static public bool TryConvertToDecimal(object ob, out decimal d)
        {
            bool res = false;
            string sNumber = "";
            d = 0.0m;

            string dec = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            if (ob != null && ob != DBNull.Value)
            {
                sNumber = ob.ToString();
                if (sNumber.IndexOf(dec) == -1)
                {
                    sNumber = sNumber.Replace(".", dec);
                    sNumber = sNumber.Replace(",", dec);
                }
                res = decimal.TryParse(sNumber, out d);
            }
            return res;
        }

        static public bool TryConvertToDouble(object ob, out double d)
        {
            bool res = false;
            string sNumber = "";
            d = 0.0;

            string dec = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            if (ob != null && ob != DBNull.Value)
            {
                sNumber = ob.ToString();
                if (sNumber.IndexOf(dec) == -1)
                {
                    sNumber = sNumber.Replace(".", dec);
                    sNumber = sNumber.Replace(",", dec);
                }
                res = double.TryParse(sNumber, out d);
            }
            return res;
        }

        public static string GetShortFormatTimeString(object datetime)
        {
            string retVal = "";
            if (datetime != null)
            {
                DateTime dateTime = DateTime.MinValue;
                string datePattern = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern;
                string dateTimeValue = datetime.ToString();
                try
                {
                    if (DateTime.TryParse(dateTimeValue, out dateTime))
                    {
                        retVal = dateTime.ToString(datePattern);
                    }
                }
                catch (ArgumentException) { }
            }
            return retVal;
        }

        public static DateTime ConvertStringToDateTime(string dt)
        {
            return XmlConvert.ToDateTime(dt, WebServiceDatePattern).ToLocalTime();
        }

        public static string WebServiceDatePattern
        {
            get { return "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffzzzzz"; }
        }

        public static string ConvertDateTimeToString(DateTime dt)
        {
            if (dt.Kind == DateTimeKind.Unspecified) dt = new DateTime(dt.Ticks, DateTimeKind.Local);
            else if (dt.Kind == DateTimeKind.Utc)
                dt = dt.ToLocalTime();
            return XmlConvert.ToString(dt, WebServiceDatePattern);
        }

    }
}
