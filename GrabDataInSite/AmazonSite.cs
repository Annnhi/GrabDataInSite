using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GrabDataInSite
{
    public class AmazonSite
    {
        List<Product> listAmazon = new List<Product>();
        Common cm;
        string _key = "iPhone 11";
        xPath xp = new xPath();

        public AmazonSite(Common common)
        {
            cm = common;
        }

        public void SelectCurrency(string currency)
        {
            cm.WaitForElementClickable(xp.amz_xp_btnChangeCurrency);
            cm.element(xp.amz_xp_btnChangeCurrency).Click();
            cm.WaitForPageLoad(cm.driver, cm.defaultTimeout);

            cm.WaitForElementPresent(xp.amz_xp_currencydropdown);
            cm.element(xp.amz_xp_currencydropdown).Click();

            cm.WaitForElementPresent(xp.amz_xp_currencylist);
            cm.element(xp.amz_xp_currency.Replace("{value}",currency)).Click();

            cm.element(xp.amz_xp_savechange).Click();

            cm.WaitForPageLoad(cm.driver,cm.defaultTimeout);
            cm.WaitForElementPresent(xp.amz_xp_mainpage);

        }

        public List<Product> StartGrabAmazon(string mode)
        {
            Console.WriteLine("StartGrabAmazon");
            SearchWithKey(_key);
            if (mode == "all")
            {
                NavigatePages();
            }
            else
            {
                GetListItemsInAPage();
            }
            return listAmazon;
        }

        public void SearchWithKey(string key)
        {
            Console.WriteLine("SearchWithKey");
            cm.WaitForElementPresent(xp.amz_xp_txtSearch);
            cm.element(xp.amz_xp_txtSearch).SendKeys(key);
            cm.element(xp.amz_xp_btnSearch).Click();
            cm.WaitForElementPresent(xp.amz_xp_listResult);
        }


        public void NavigatePages()
        {
            bool isLastPage = false;
            int countPage = 0;
            while (!isLastPage)
            {
                countPage++;
                Console.WriteLine("Page " + countPage);

                Thread.Sleep(0); //Sleep few seconds per page to prevent block from website
                GetListItemsInAPage();
                isLastPage = IsLastPage();
                if (!isLastPage)
                {
                    cm.element(xp.amz_xp_btnPageNext).Click();
                    cm.WaitForPageLoad(cm.driver, 30);
                    cm.WaitForElementPresent(xp.amz_xp_listResult);
                }
            }
        }


        public void GetListItemsInAPage()
        {
            var listItems = cm.driver.FindElements(By.XPath(xp.amz_xp_listItems));
            for (int i = 0; i < listItems.Count; i++)
            {
                string productName = listItems[i].FindElement(By.XPath(xp.amz_xp_item_productname)).Text;
                string price = "";

                try
                {
                    price = listItems[i].FindElement(By.XPath(xp.amz_xp_item_price)).Text;
                }
                catch (Exception)
                {
                }

                string link = listItems[i].FindElement(By.XPath(xp.amz_xp_item_link)).GetAttribute("href");

                string formatPrice = price.Replace(" ", "").Replace("VND", "").Replace(",", "").ToLower();
                double _price = -1;
                try
                {
                    _price = Double.Parse(formatPrice);
                }
                catch (Exception)
                {
                }

                if ((productName.ToLower().Contains(_key.ToLower()) || productName.ToLower().Contains(_key.ToLower().Replace(" ",""))) 
                    && !productName.ToLower().Contains(" case") && price != "")
                {
                    listAmazon.Add(new Product
                    {
                        Site = "Amazon",
                        Name = productName,
                        Price = _price,
                        Link = link
                    });
                }
            }
        }


        public bool IsLastPage()
        {
            var elNext = cm.element(xp.amz_xp_btnPageNext);
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
