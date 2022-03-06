using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace GrabDataInSite
{
    public class EbaySite
    {
        List<Product> listEbay = new List<Product>();
        Common cm;
        string _key = "iPhone 11";
        xPath xp = new xPath();

        public EbaySite(Common common)
        {
            cm = common;
        }

        public List<Product> StartGrabEbay(string mode)
        {
            Console.WriteLine("StartGrabEbay");
            SearchWithKey(_key);
            if (mode == "all")
            {
                NavigatePages();
            }
            else
            {
                GetListItemsInAPage();
            }
            return listEbay;
        }

        public void SearchWithKey(string key)
        {
            Console.WriteLine("SearchWithKey");
            cm.WaitForElementPresent(xp.eb_xp_txtSearch);
            cm.element(xp.eb_xp_txtSearch).SendKeys(key);
            cm.element(xp.eb_xp_btnSearch).Click();
            cm.WaitForElementPresent(xp.eb_xp_listResult);
        }


        public void NavigatePages()
        {
            bool isLastPage = false;
            int countPage = 0;
            while (!isLastPage)
            {
                countPage++;
                Console.WriteLine("Page " + countPage);

                Thread.Sleep(1000); //Sleep few seconds per page to prevent block from website
                GetListItemsInAPage();
                isLastPage = IsLastPage();
                if (!isLastPage)
                {
                    cm.element(xp.eb_xp_btnPageNext).Click();
                    cm.WaitForPageLoad(cm.driver, 30);
                    cm.WaitForElementPresent(xp.eb_xp_listResult);
                }
            }
        }

        public void GetListItemsInAPage()
        {
            var listItems = cm.driver.FindElements(By.XPath(xp.eb_xp_listItems));
            for (int i = 0; i < listItems.Count; i++)
            {
                string productName = listItems[i].FindElement(By.XPath(xp.eb_xp_item_productname)).Text;
                string price = listItems[i].FindElement(By.XPath(xp.eb_xp_item_price)).Text;
                string link = listItems[i].FindElement(By.XPath(xp.eb_xp_item_link)).GetAttribute("href");

                string formatPrice = price.Replace(" ", "").Replace("VND", "").Replace(",", "").ToLower();
                if (formatPrice.Contains("to"))
                {
                    formatPrice = formatPrice.Split('t')[0];
                }

                double _price = -1;
                try
                {
                    _price = Double.Parse(formatPrice);
                }
                catch (Exception)
                {
                }

                //Console.WriteLine(i + " - " + productName + " | " + price + " | " + link);
                if ((productName.ToLower().Contains(_key.ToLower()) || productName.ToLower().Contains(_key.ToLower().Replace(" ", "")))
                    && !productName.ToLower().Contains(" case") && price != "")
                {
                    listEbay.Add(new Product
                    {
                        Site = "Ebay",
                        Name = productName,
                        Price = _price,
                        Link = link
                    });
                }
            }
        }

        public bool IsLastPage()
        {
            var elNext = cm.element(xp.eb_xp_btnPageNext);
            string datatrack = "";
            datatrack = elNext.GetAttribute("aria-disabled");
            if (datatrack != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
