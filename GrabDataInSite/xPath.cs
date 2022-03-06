using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrabDataInSite
{
    public class xPath
    {
        public string eb_link = "https://www.ebay.com/";
        public string eb_xp_txtSearch = "//input[@id='gh-ac']";
        public string eb_xp_btnSearch = "//input[@id='gh-btn']";
        public string eb_xp_listResult = "//div[@id='srp-river-main']";
        public string eb_xp_listItems = "//div[@id='srp-river-results']//li[contains(@class,'s-item s-item__pl-on-bottom')]";
        public string eb_xp_btnPageNext = "//a[@type='next']";
        public string eb_xp_item_productname = ".//h3";
        public string eb_xp_item_price = ".//span[contains(@class, 's-item__price')]";
        public string eb_xp_item_link = ".//div[@class='s-item__info clearfix']//a";


        public string amz_link = "https://www.amazon.com/";
        public string amz_xp_btnChangeCurrency = "//a[@id='icp-nav-flyout']";
        public string amz_xp_currencydropdown = "//span[@id='icp-currency-dropdown-selected-item-prompt']";
        public string amz_xp_currencylist = "//div[@id='a-popover-1']";
        public string amz_xp_currency = "//li[@id='{value}']";
        public string amz_xp_savechange= "//span[@id='icp-save-button']//input";
        public string amz_xp_mainpage = "//div[@id='pageContent']";
        public string amz_xp_txtSearch = "//input[@id='twotabsearchtextbox']";
        public string amz_xp_btnSearch = "//input[@id='nav-search-submit-button']";
        public string amz_xp_listResult = "//div[@class='s-main-slot s-result-list s-search-results sg-row']";
        public string amz_xp_listItems = "//div[@data-component-type='s-search-result']";
        public string amz_xp_btnPageNext = "//*[contains(@class,'s-pagination-next')]";
        public string amz_xp_item_productname = ".//h2//span";
        public string amz_xp_item_price = ".//span[@class='a-price-whole']";
        public string amz_xp_item_link = ".//h2/a";

    }
}
