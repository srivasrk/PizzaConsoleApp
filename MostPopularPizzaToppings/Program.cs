using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MostPopularPizzaToppings
{

    class PizzaToppings
    {
        //There could be one or more toppings on a pizza
        public string[] toppings { get; set; }
    }

    class Program
    {

        static void Main(string[] args)
        {
            //Get JSON from url
            var json = new WebClient().DownloadString("http://files.olo.com/pizzas.json");
            
            //Deserialize
            PizzaToppings[] products = JsonConvert.DeserializeObject<PizzaToppings[]>(json);

            //This will store the count of each topping
            Dictionary<string, int> ToppingsFrequencyMap= new Dictionary<string, int>();            

            //Temporary vars
            PizzaToppings ListOfCurrentToppings;
            string CurrentTopping;
            //var result = products.SelectMany(i => i.toppings).ToArray();

            //Populate the dictionary with fequency of each topping
            for (int index = 0; index < products.Length; index++)
            {
                ListOfCurrentToppings = products[index];

                for (int index2 = 0; index2 < ListOfCurrentToppings.toppings.Length; index2++)
                {
                    CurrentTopping = ListOfCurrentToppings.toppings[index2];
                    if (ToppingsFrequencyMap.ContainsKey(CurrentTopping) )
                    {
                        ToppingsFrequencyMap[CurrentTopping]++;
                    }
                    else
                    {
                        ToppingsFrequencyMap.Add(CurrentTopping, 1);
                    }
                }                

            }

            //Get the top 20 most frequent items from the dictionary
            var result = ToppingsFrequencyMap.OrderByDescending(item => item.Value).Take(20);
            int rank = 1;

            //Display the items with rank and frequency
            foreach (var item in result)
            {
                Console.WriteLine(rank++ + "." + item.Key + ": " + item.Value);
            }

            Console.ReadLine();
        }
    }
}
