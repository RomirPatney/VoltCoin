using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EthUoft.Infrastucture
{
    public class APIcaller
    {

        private static readonly HttpClient client = new HttpClient();
        private static string allCurrencies;
        private static string specificCurrency;

        public static List<Currency> All()
        {
            GetAllcurrencies();
            //return ReadValue(allCurrencies);
            while (allCurrencies == null || allCurrencies == "")
            {
                // wait till you get the values
                // The process is in async because
            }
            var jToken = JToken.Parse(allCurrencies);
            var users = jToken.ToObject<List<Currency>>();
            return users;
        }

        public static List<Currency> Specific(string currency)
        {
            GetSpecifcCurrency(currency);
            //return ReadValue(specificCurrency);
            while (specificCurrency == null || specificCurrency == "")
            {
                // wait till you get the values
                // The process is in async because
            }
            var jToken = JToken.Parse(specificCurrency);
            var users = jToken.ToObject<List<Currency>>();
            return users;

        }

        public async static void GetAllcurrencies()
        {
            //var responseString = await client.GetStringAsync("http://www.example.com/recepticle.aspx");
            allCurrencies = await client.GetStringAsync("https://api.coinmarketcap.com/v1/ticker/");
        }

        public async static void GetSpecifcCurrency(string currency)
        {
            specificCurrency = await client.GetStringAsync("https://api.coinmarketcap.com/v1/ticker/"+ currency);
        }

        public static List<Currency> ReadValue(string call)
        {
            while (call == null || call == "")
            {
                // wait till you get the values
                // The process is in async because
            }
            var jToken = JToken.Parse(call);
            var users = jToken.ToObject<List<Currency>>();
            return users;
        }
        

    }
   
    public class Currency
    {
        public string id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public string rank { get; set; }
        public string price_usd { get; set; }
        public string price_btc { get; set; }
        public string __invalid_name__24h_volume_usd { get; set; }
        public string market_cap_usd { get; set; }
        public string available_supply { get; set; }
        public string total_supply { get; set; }
        public string max_supply { get; set; }
        public string percent_change_1h { get; set; }
        public string percent_change_24h { get; set; }
        public string percent_change_7d { get; set; }
        public string last_updated { get; set; }
    }

}
