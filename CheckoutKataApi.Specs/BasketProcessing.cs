using System;
using System.Net;
using System.Web.Script.Serialization;
using CheckoutKataApi.Web;
using NUnit.Framework;

namespace CheckoutKataApi.Specs
{
    public class BasketProcessing
    {
        private readonly Browser _browser = new Browser();

        public Uri CreateBasket()
        {
            _browser.Post(new Uri("http://checkout-kata.local/baskets"));

            _browser.AssertResponseCodeIs(HttpStatusCode.Created);

            return _browser.GetLocationUri();
        }

        public void GetBasket(Uri basketUri)
        {
            _browser.Get(basketUri);

            _browser.AssertResponseCodeIs(HttpStatusCode.OK);
        }

        public void AssertPriceIsCorrect(int expectedPrice)
        {
            var body = _browser.GetResponseBody();

            var serializer = new JavaScriptSerializer();
            var basket = serializer.Deserialize<Basket>(body);

            Assert.That(basket.Price, Is.EqualTo(expectedPrice));
        }
    }
}