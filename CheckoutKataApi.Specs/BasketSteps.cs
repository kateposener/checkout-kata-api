﻿using System;
using System.Net;
using TechTalk.SpecFlow;

namespace CheckoutKataApi.Specs
{
    [Binding]
    public class BasketSteps
    {
        private Uri _basketUri;
        private readonly BasketProcessing _basketProcessing = new BasketProcessing();

        [Given(@"I have an empty basket")]
        public void GivenIHaveAnEmptyBasket()
        {
            _basketUri = _basketProcessing.CreateBasket();
        }

        [When(@"I check my basket")]
        public void WhenICheckMyBasket()
        {
            _basketProcessing.GetBasket(_basketUri);
        }

        [Then(@"the price should be (.*)")]
        public void ThenThePriceShouldBe(int expectedPrice)
        {
            _basketProcessing.AssertPriceIsCorrect(expectedPrice);
        }
    }
}
