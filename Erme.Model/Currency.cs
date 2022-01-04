using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenExchangeRatesSharp;

namespace Erme.Model
{

    public class Currency
    {
        public decimal eurHuf { get; set; }
        public decimal usdHuf { get; set; }

        public Currency()
        {
            var client = new RateClient("bd6f8f17770647d68a7bde47f07dfdd4");
            var currencyList = client.GetLatest();
            var rates = currencyList.Rates; // A dictionary of the rates

            decimal usdEur = 0;
            foreach (var item in rates)
            {
                if (item.Key == "EUR")
                {
                    usdEur = item.Value;
                }
                else if (item.Key == "HUF")
                {
                    this.usdHuf = item.Value;
                }
            }
            if (usdHuf != 0 && usdEur != 0)
            {
                this.eurHuf = this.usdHuf * (1 / usdEur);
            }

        }

        public string cc()
        {
            var client = new RateClient("bd6f8f17770647d68a7bde47f07dfdd4");
            var currencyList = client.GetLatest();
            var rates = currencyList.Rates; // A dictionary of the rates

            
            decimal usdEur = 0;
            decimal usdHuf = 0;
            decimal eurHuf = 0;

            foreach (var item in rates)
            {
                if (item.Key == "EUR")
                {
                    usdEur = item.Value;
                }
                else if (item.Key == "HUF")
                {
                    usdHuf = item.Value;
                }

            }

            if(usdHuf != 0 && usdEur != 0)
            {
                eurHuf = usdHuf * (1 / usdEur);
            }

            return "1€ = " + eurHuf.ToString("C0") + " - 1$ = " + usdHuf.ToString("C0");
        }


    }



}
