using System;
using System.IO;
using System.Net;
using NUnit.Framework;

namespace CheckoutKataApi.Specs
{
    public class Browser
    {
        private HttpWebResponse _webResponse;

        public void Post(Uri uri)
        {
            var webRequest = WebRequest.Create(uri);
            webRequest.Method = "POST";
            webRequest.ContentLength = 0;
            _webResponse = (HttpWebResponse) webRequest.GetResponse();
        }

        public void Get(Uri basketUri)
        {
            var webRequest = WebRequest.Create(basketUri);
            _webResponse = (HttpWebResponse) webRequest.GetResponse();
        }

        public Uri GetLocationUri()
        {
            return new Uri(_webResponse.GetResponseHeader("Location"));
        }

        public void AssertResponseCodeIs(HttpStatusCode httpStatusCode)
        {
            Assert.That(_webResponse.StatusCode, Is.EqualTo(httpStatusCode));
        }

        public string GetResponseBody()
        {
            using (var responseStream = _webResponse.GetResponseStream())
            {
                Assert.IsNotNull(responseStream, "responseStream");
                using (var streamReader = new StreamReader(responseStream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
    }
}