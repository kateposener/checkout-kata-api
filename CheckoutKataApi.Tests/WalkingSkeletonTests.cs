using System.IO;
using System.Net;
using NUnit.Framework;

namespace CheckoutKataApi.Tests
{
    [TestFixture]
    public class WalkingSkeletonTests
    {
        [Test]
        public void Should_return_something()
        {
            var webRequest = WebRequest.Create("http://checkout-kata.local");
            var webResponse = (HttpWebResponse) webRequest.GetResponse();

            var responseStream = webResponse.GetResponseStream();
            var streamReader = new StreamReader(responseStream);
            var response = streamReader.ReadToEnd();

            Assert.That(webResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response, Is.EqualTo("welcome"));
        }
    }
}
