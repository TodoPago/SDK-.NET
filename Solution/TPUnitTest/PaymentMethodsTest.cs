using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TodoPagoConnector;
using TodoPagoConnector.Model;
using TPUnitTest.Mock;
using TPUnitTest.Mock.Data;

namespace TPUnitTest
{
    [TestClass]
    public class PaymentMethodsTest
    {
        [TestMethod]
        public void DiscoverPaymentMethodsOKTest()
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);

            TodoPagoMockConnector restConnector = new TodoPagoMockConnector("https://developers.todopago.com.ar/t/1.1/api/", headers);
            restConnector.SetRequestResponse(PaymentMethodsDataProvider.GetDiscoverOkResponse());
            TPConnectorMock connector = new TPConnectorMock(TPConnector.developerEndpoint, headers, restConnector);

            Dictionary<string, object> response = connector.DiscoverPaymentMethods();

            Assert.AreNotEqual(null, response);

            Assert.AreEqual(true, response.Count > 0);

            Assert.AreEqual(true, response.ContainsKey("PaymentMethodsCollection"));

            Dictionary<string, object> paymentCollection = (Dictionary<string, object>)response["PaymentMethodsCollection"];

            Assert.AreEqual(true, paymentCollection.Count > 0);

            Assert.AreEqual(true, paymentCollection.ContainsKey("PaymentMethod - 1"));

            Assert.AreEqual(true, ((Dictionary<string, object>)paymentCollection["PaymentMethod - 1"]).ContainsKey("ID"));
        }

        [TestMethod]
        public void DiscoverPaymentMethodsFailTest()
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);

            TodoPagoMockConnector restConnector = new TodoPagoMockConnector("https://developers.todopago.com.ar/t/1.1/api/", headers);
            restConnector.SetRequestResponse(PaymentMethodsDataProvider.GetDiscoverFailResponse());
            TPConnectorMock connector = new TPConnectorMock(TPConnector.developerEndpoint, headers, restConnector);

            Dictionary<string, object> response = connector.DiscoverPaymentMethods();

            Assert.AreNotEqual(null, response);

            Assert.AreEqual(true, response.Count > 0);

            Assert.AreEqual(true, response.ContainsKey("PaymentMethodsCollection"));

            Dictionary<string, object> paymentCollection = (Dictionary<string, object>)response["PaymentMethodsCollection"];

            Assert.AreEqual(true, paymentCollection.Count == 0);
        }

        [TestMethod]
        public void GetAllPaymentMethodsOkTest()
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);

            TodoPagoMockConnector restConnector = new TodoPagoMockConnector("https://developers.todopago.com.ar/t/1.1/api/", headers);
            restConnector.SetRequestResponse(PaymentMethodsDataProvider.GetAllPaymentMethodsOkResponse());
            TPConnectorMock connector = new TPConnectorMock(TPConnector.developerEndpoint, headers, restConnector);

            Dictionary<string, object> response = connector.GetAllPaymentMethods("35");

            Assert.AreNotEqual(null, response);

            Assert.AreEqual(true, response.Count > 0);

            Assert.AreEqual(true, response.ContainsKey("Result"));

            Dictionary<string, object> paymentMethods = (Dictionary<string, object>)response["Result"];

            Assert.AreNotEqual(null, paymentMethods);

            Assert.AreEqual(true, paymentMethods.Count > 0);

            Assert.AreEqual(true, paymentMethods.ContainsKey("PaymentMethodsCollection"));
            Assert.AreEqual(true, paymentMethods.ContainsKey("PaymentMethodBanksCollection"));
            Assert.AreEqual(true, paymentMethods.ContainsKey("BanksCollection"));
        }

        public void GetAllPaymentMethodsJSONOkTest()
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);

            TodoPagoMockConnector restConnector = new TodoPagoMockConnector("https://developers.todopago.com.ar/t/1.1/api/", headers);
            restConnector.SetRequestResponse(PaymentMethodsDataProvider.GetAllPaymentMethodsOkResponse());
            TPConnectorMock connector = new TPConnectorMock(TPConnector.developerEndpoint, headers, restConnector);

            var response = connector.GetAllPaymentMethodsAsJSON("35");
            response = response.Replace("\"@xmlns\":\"http://api.todopago.com.ar\",", "");

            var results = JsonConvert.DeserializeObject<TodoPagoResult>(response);

            Assert.AreEqual(true, results.Result.PaymentMethodsCollection.PaymentMethod.Count > 0);
        }

        [TestMethod]
        public void GetAllPaymentMethodsFailTest()
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);

            TodoPagoMockConnector restConnector = new TodoPagoMockConnector("https://developers.todopago.com.ar/t/1.1/api/", headers);
            restConnector.SetRequestResponse(null);
            TPConnectorMock connector = new TPConnectorMock(TPConnector.developerEndpoint, headers, restConnector);

            Dictionary<string, object> response = connector.GetAllPaymentMethods("35");

            Assert.AreNotEqual(null, response);

            Assert.AreEqual(true, response.Count == 0);
        }

        [TestMethod]
        public void GetAllPaymentMethodsJSONFailTest()
        {
            var headers = new Dictionary<String, String>();
            string authorization = "TODOPAGO ABCDEF1234567890";
            headers.Add("Authorization", authorization);

            TodoPagoMockConnector restConnector = new TodoPagoMockConnector("https://developers.todopago.com.ar/t/1.1/api/", headers);
            restConnector.SetRequestResponse(null);
            TPConnectorMock connector = new TPConnectorMock(TPConnector.developerEndpoint, headers, restConnector);

            var response = connector.GetAllPaymentMethodsAsJSON("35");
            response = response.Replace("\"@xmlns\":\"http://api.todopago.com.ar\",", "");

            var results = JsonConvert.DeserializeObject<TodoPagoResult>(response);

            Assert.AreNotEqual(null, response);

            Assert.AreEqual(null, results.Result);
        }
    }
}