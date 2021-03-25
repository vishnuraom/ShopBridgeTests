using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopBridge.Services;
using ShopBridge.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace ShopBridge.Services.Tests
{
    [TestClass()]
    public class InventoryMgmtServiceTests
    {
        string getItemsUrl = "https://localhost:44362/api/Inventory/GetItems";
        string addItemUrl = "https://localhost:44362/api/Inventory/AddItem";

        [TestMethod()]
        public void GetItemsTest()
        {
            HttpClient httpClient = new HttpClient();
            Task<HttpResponseMessage> httpResponse = httpClient.GetAsync(getItemsUrl);
            HttpResponseMessage httpResponseMessage =  httpResponse.Result;
            httpClient.Dispose();
        }

        [TestMethod()]
        public void AddItemTest()
        {
            HttpClient httpClient = new HttpClient();
            
            InventoryDTO inventoryDTO = new InventoryDTO() { Category = Models.Enums.Category.Clothing, Description = "Jeans", Name = "Jeans", Price = 3453 };
            //string json = JsonConvert.SerializeObject(inventoryDTO, Formatting.Indented);
            string json = "{\r\n  \"name\": \"Jeans\",\r\n  \"description\": \"Jeans\",\r\n  \"category\": 1,\r\n  \"price\": 3453.0\r\n}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            Uri uri = new Uri(addItemUrl);
            var httpResponse = httpClient.PostAsync(uri, content).Result;

            // If the response contains content we want to read it!
            if (httpResponse.Content != null)
            {
                var responseContent = httpResponse.Content.ReadAsStringAsync().Result;
                httpClient.Dispose();
            }
        }
    }
}
