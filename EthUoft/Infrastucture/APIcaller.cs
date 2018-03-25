using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Collections;

namespace EthUoft.Infrastucture
{
    public class APIcaller
    {
        private static string strCon = "Server=tcp:ethuoft.database.windows.net,1433;Initial Catalog=EthUoft;Persist Security Info=False;User ID=database_admin;Password=Cleaner123@@;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
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

        public static ArrayList GetTimeStamps()
        {
            ArrayList Timestamp = new ArrayList();
            String sql = "SELECT [Timestamp] FROM [dbo].[graphmaker]";
            SqlConnection conn = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader nwReader = comm.ExecuteReader();
            while (nwReader.Read())
            {
                Timestamp.Add((string)nwReader["Timestamp"] + " hrs");
                //Timestamp = "\""+(string)nwReader["Timestamp"] + "hrs\" ,";
                
            }
            nwReader.Close();
            conn.Close();
            return Timestamp;
        }

        public static string GetValues()
        {
            string Value = "";
            String sql = "SELECT [Value] FROM [dbo].[graphmaker]";
            SqlConnection conn = new SqlConnection(strCon);
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader nwReader = comm.ExecuteReader();
            while (nwReader.Read())
            {
                Value += ((Convert.ToDouble((string)nwReader["Value"])) * 100 + 100).ToString() + ",";
            }
            nwReader.Close();
            conn.Close();
            return Value;
        }

        public static bool InsertValueEveryHour()
        {
            var date = DateTime.Now;
            using (SqlConnection connection =new SqlConnection(strCon))
            {
                Currency eth = new Currency();
                foreach (var temp in Specific("Ethereum"))
                {
                    eth = temp;
                    break;
                }
                
                string queryString = "IF NOT EXISTS (SELECT Timestamp FROM [dbo].[graphmaker] WHERE Timestamp = '" + date.Hour + "') BEGIN"+
                    " INSERT INTO [dbo].[graphmaker] (Timestamp, Value) VALUES('" + date.Hour+"', '"+ eth.percent_change_1h + "'); END ELSE BEGIN "+
                    "UPDATE [dbo].[graphmaker] SET Value = '"+ eth.percent_change_1h + "' WHERE Timestamp = '" +date.Hour +"' END";
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }

            }
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
