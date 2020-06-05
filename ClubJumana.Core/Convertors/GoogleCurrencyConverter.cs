using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

public static class GoogleCurrencyConverter
{
    public static decimal ConvertCurrency(string from, string to, decimal quantity)
    {
        if (from == null)
        {
            throw new ArgumentNullException("from");
        }
        else if (to == null)
        {
            throw new ArgumentNullException("to");
        }
        else if (quantity < 0)
        {
            throw new ArgumentOutOfRangeException("quantity");
        }

        if (from == to)
        {
            return quantity;
        }
        else if (quantity == 0)
        {
            return 0;
        }

        var request = HttpWebRequest.Create(string.Format("http://www.google.com/ig/calculator?hl=en&q=1 {0} to {1}", from, to));
        request.Timeout = 5000;
        using (var response = request.GetResponse())
        {
            using (var stream = new StreamReader(response.GetResponseStream()))
            {

                Match match;
                if ((match = Regex.Match(stream.ReadLine(), @"{lhs: ""(?<From>[0-9.]+)(.*?)"",rhs: ""(?<To>[0-9.]+)(.*?)"",error: ""(?<Error>(.*?))"",icc: (true|false)}", RegexOptions.Singleline | RegexOptions.IgnoreCase)).Success)
                {
                    var result = new
                    {
                        From = match.Groups["From"].Value,
                        To = match.Groups["To"].Value,
                        Error = match.Groups["Error"].Value
                    };

                    if (string.IsNullOrEmpty(result.Error))
                    {
                        return Math.Round(Convert.ToDecimal(result.To, CultureInfo.InvariantCulture) * quantity, 4);
                    }

                    throw new Exception(string.Format("Converter error: {0}.", result.Error));
                }

                throw new Exception("Response format error.");
            }
        }
    }
}