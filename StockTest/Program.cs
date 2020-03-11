using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace StockTest
{ 

class Stock
{
    public Double Price { get; set; }
    public string Name { get; set; }
}

    class Program
    {
        static void Main()
        {
            RunAsync().Wait();
            Console.WriteLine("AA");
        }

        static async Task RunAsync()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://www.alphavantage.co/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string companyName;
                Console.WriteLine("Enter a company name: ");
                companyName = Console.ReadLine().ToString();
                Console.WriteLine("\n");
                try
                {
                    HttpResponseMessage response = await client.GetAsync($"query?function=GLOBAL_QUOTE&symbol={companyName}&interval=5min&apikey=W6FPZ0L66XE6HLI4");
                    if (response.IsSuccessStatusCode)
                    {

                        JObject jsonResponse = await response.Content.ReadAsAsync<JObject>();


                        Stock currStock = new Stock
                        {
                            Name = jsonResponse["Global Quote"]["01. symbol"].ToString(),
                            Price = Double.Parse(jsonResponse["Global Quote"]["05. price"].ToString())
                        };
                        Console.WriteLine("Stock name " + currStock.Name + "\n" + "Stock price " + currStock.Price);

                    }
                }
                catch
                {
                    Console.WriteLine("Invalid input");
                }

            }
        }
    }
}