using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrabDataInSite
{
    class Program
    {
        static Common cm;
        static xPath xp = new xPath();
        static EbaySite ebay;
        static AmazonSite amazon;

        static List<Product> listEbay;
        static List<Product> listAmazon;
        static void Main(string[] args)
        {
            string mode = "";
            bool isTrue = false;
            while (!isTrue)
            {
                Console.WriteLine("1. Get data in first page");
                Console.WriteLine("2. Get data in all page");
                Console.Write("Your option: ");
                string choice = Console.ReadLine().Trim();
                if (choice == "1" || choice == "2")
                {
                    isTrue = true;

                    if (choice == "2")
                    {
                        mode = "all";
                    }
                    else
                    {
                        mode = "";
                    }
                }
                else
                {
                    Console.WriteLine("Wrong option");
                    Console.WriteLine();
                }
            }


            Console.WriteLine("STARTING...");
            cm = new Common();
            ebay = new EbaySite(cm);
            amazon = new AmazonSite(cm);

            StartDriver();

            OpenEbaySite();
            GrabItemInEbay(mode);

            OpenAmazonSite();
            SelectCurrency("VND");
            GrabItemInAmazon(mode);


            var allProducts = new List<Product>(listEbay.Count + listAmazon.Count);
            allProducts.AddRange(listEbay);
            allProducts.AddRange(listAmazon);

            List<Product> SortedList = allProducts.OrderBy(o => o.Price).ToList();
            for (int i = 0; i < listEbay.Count; i++)
            {
                Console.WriteLine("{0} - {1} | {2} {3} | {4}", SortedList[i].Site, 
                    SortedList[i].Name, 
                    SortedList[i].Price.ToString("#,##0.##"), "VND", 
                    SortedList[i].Link);
            }
            Console.WriteLine("Total {0} products", SortedList.Count);

            Console.WriteLine("DONE!");
            Console.ReadLine();
        }

        private static void StartDriver()
        {
            cm.StartChrome();
        }

        private static void OpenEbaySite()
        {
            Console.WriteLine("OpenEbaySite");
            cm.OpenSite(xp.eb_link);
        }

        private static void GrabItemInEbay(string mode)
        {
            listEbay = ebay.StartGrabEbay(mode);
        }

        private static void OpenAmazonSite()
        {
            Console.WriteLine("OpenAmazonSite");
            cm.OpenSite(xp.amz_link);
        }

        private static void SelectCurrency(string currency)
        {
            amazon.SelectCurrency(currency);
        }

        private static void GrabItemInAmazon(string mode)
        {
            listAmazon = amazon.StartGrabAmazon(mode);

        }

    }
}
