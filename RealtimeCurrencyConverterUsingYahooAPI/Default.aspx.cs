using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Services;

namespace RealtimeCurrencyConverterUsingYahooAPI
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static Decimal ConvertCurrency(decimal amount, string fromCurrency, string toCurrency)
        {
            WebClient client = new WebClient();
            Stream response = client.OpenRead(string.Format("http://finance.yahoo.com/d/quotes.csv?e=.csv&f=sl1d1t1&s={0}{1}=X", fromCurrency.ToUpper(), toCurrency.ToUpper()));
            StreamReader reader = new StreamReader(response);
            string yahooResponse = reader.ReadLine();
            response.Close();
            if (!string.IsNullOrWhiteSpace(yahooResponse))
            {
                string[] values = Regex.Split(yahooResponse, ",");
                if (values.Length > 0)
                {
                    decimal rate = System.Convert.ToDecimal(values[1]);
                    return rate * amount;
                }
            }
            return 0;
        }
    }
}