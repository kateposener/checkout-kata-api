using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;
using CheckoutKataApi.Web;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace CheckoutKataApi.Specs
{
    [Binding]
    public class BasketSteps
    {
        private Uri _basketUri;
        private HttpWebResponse _webResponse;

        [Given(@"I have an empty basket")]
        public void GivenIHaveAnEmptyBasket()
        {
            _basketUri = CreateBasket();
        }

        private Uri CreateBasket()
        {
            var webRequest = WebRequest.Create("http://checkout-kata.local/baskets");
            webRequest.Method = "POST";
            webRequest.ContentLength = 0;
            _webResponse = (HttpWebResponse) webRequest.GetResponse();

            Assert.That(_webResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            return new Uri(_webResponse.GetResponseHeader("Location"));
        }

        [When(@"I check my basket")]
        public void WhenICheckMyBasket()
        {
            var webRequest = WebRequest.Create(_basketUri);
            _webResponse = (HttpWebResponse)webRequest.GetResponse();

            Assert.That(_webResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
        
        [Then(@"the price should be (.*)")]
        public void ThenThePriceShouldBe(int expectedPrice)
        {
            var responseStream = _webResponse.GetResponseStream();
            Assert.IsNotNull(responseStream, "responseStream");
            var streamReader = new StreamReader(responseStream);

            var body = streamReader.ReadToEnd();

            var serializer = new JavaScriptSerializer();
            var basket = serializer.Deserialize<Basket>(body);

            Assert.That(basket.Price, Is.EqualTo(0));
        }
    }
}
