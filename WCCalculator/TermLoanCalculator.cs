using NUnit.Framework;
using RestSharp;
using RestSharp.Serialization.Json;
using System;
using System.Collections.Generic;
using System.Net;
using WCCalculator.Models;

namespace WCCalculator
{
    [TestFixture]
    public class TermLoanCalculator
    {
        RestClient client;
        string uri;

        Dictionary<string, string> head = new Dictionary<string, string>() { { "key", "Ocp-Apim-Subscription-Key" }, { "value", "3cc7e8194f4d462093c4a71282fac62d" } };

        [SetUp]
        public void Initialize()
        {
            client = new RestClient("https://sf-api-gateway.azure-api.net");
            uri = "/credit-limit/staging/workingcapital/loan-calculator";   
        }

        [TestCase(10, 1000000, 3, "monthly", "3", false, TestName = "Check for Pay Interest in 3 Term and Montlhy")]
        [TestCase(10, 1000000, 4, "monthly", "4", false, TestName = "Check for Pay Interest in 4 Term and Montlhy")]
        [TestCase(10, 1000000, 5, "monthly", "5", false, TestName = "Check for Pay Interest in 5 Term and Montlhy")]
        [TestCase(10, 1000000, 6, "monthly", "6", false, TestName = "Check for Pay Interest in 6 Term and Montlhy")]
        [TestCase(10, 1000000, 7, "monthly", "7", false, TestName = "Check for Pay Interest in 7 Term and Montlhy")]
        [TestCase(10, 1000000, 8, "monthly", "8", false, TestName = "Check for Pay Interest in 8 Term and Montlhy")]
        [TestCase(10, 1000000, 9, "monthly", "9", false, TestName = "Check for Pay Interest in 9 Term and Montlhy")]
        [TestCase(10, 1000000, 10, "monthly", "10", false, TestName = "Check for Pay Interest in 10 Term and Montlhy")]
        [TestCase(10, 1000000, 11, "monthly", "11", false, TestName = "Check for Pay Interest in 11 Term and Montlhy")]
        [TestCase(10, 1000000, 12, "monthly", "12", false, TestName = "Check for Pay Interest in 12 Term and Montlhy")]
        public void TermLoanMontlhyAndAllTermsCalculatorTest(int loanType, int value, int totalPayments, string frequency, string paymentTimesExpected, bool flagInterest)
        {
            // arrange
            var request = new RestRequest(uri + $"/{loanType}/{value}/{totalPayments}/{frequency}/{flagInterest}", Method.GET);
            request.AddHeader(head.GetValueOrDefault("key"), head.GetValueOrDefault("value"));

            // act
            IRestResponse response = client.Execute(request);
            CalculatorResults calculatorResponse = new JsonDeserializer().Deserialize<CalculatorResults>(response);

            //assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.IsTrue(calculatorResponse.IsSuccess);
            Assert.IsFalse(calculatorResponse.Error[0].HasError);
            Assert.AreEqual(paymentTimesExpected, calculatorResponse.Result[0].PaymentTimes);
        }

        //[TestCase(10, 1000000, 3, "biweekly", "6", false, TestName = "Check for Pay Interest in 3 Term and Biweekly")]
        //[TestCase(10, 1000000, 4, "biweekly", "8", false, TestName = "Check for Pay Interest in 4 Term and Biweekly")]
        //[TestCase(10, 1000000, 5, "biweekly", "10", false, TestName = "Check for Pay Interest in 5 Term and Biweekly")]
        //[TestCase(10, 1000000, 6, "biweekly", "12", false, TestName = "Check for Pay Interest in 6 Term and Biweekly")]
        //[TestCase(10, 1000000, 7, "biweekly", "14", false, TestName = "Check for Pay Interest in 7 Term and Biweekly")]
        //[TestCase(10, 1000000, 8, "biweekly", "16", false, TestName = "Check for Pay Interest in 8 Term and Biweekly")]
        //[TestCase(10, 1000000, 9, "biweekly", "18", false, TestName = "Check for Pay Interest in 9 Term and Biweekly")]
        //[TestCase(10, 1000000, 10, "biweekly", "20", false, TestName = "Check for Pay Interest in 10 Term and Biweekly")]
        //[TestCase(10, 1000000, 11, "biweekly", "22", false, TestName = "Check for Pay Interest in 11 Term and Biweekly")]
        //[TestCase(10, 1000000, 12, "biweekly", "24", false, TestName = "Check for Pay Interest in 12 Term and Biweekly")]
        //public void TermLoanBiweeklyAndAllTermsCalculatorTest(int loanType, int value, int totalPayments, string frequency, string paymentTimesExpected, bool flagInterest)
        //{
        //    // arrange
        //    var request = new RestRequest(uri + $"/{loanType}/{value}/{totalPayments}/{frequency}/{flagInterest}", Method.GET);
        //    request.AddHeader(headerKey, headerValue);

        //    // act
        //    IRestResponse response = client.Execute(request);
        //    CalculatorResults calculatorResponse = new JsonDeserializer().Deserialize<CalculatorResults>(response);

        //    // assert
        //    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        //    Assert.IsTrue(calculatorResponse.IsSuccess);
        //    Assert.IsFalse(calculatorResponse.Error[0].HasError);
        //    Assert.AreEqual(paymentTimesExpected, calculatorResponse.Result[0].PaymentTimes);
        //}

        //[TestCase(10, 5000, 12, "monthly", false, "18", "1.5", TestName = "Check for Pay Interest in a minimum range for Rate in 18000")]
        //[TestCase(10, 50000, 12, "biweekly", false, "18", "1.5", TestName = "Check for Pay Interest in a maximum range for Rate in 18000")]
        //[TestCase(10, 50001, 12, "monthly", false, "17.5", "1.46", TestName = "Check for Pay Interest in a minimum range for Rate in 17500")]
        //[TestCase(10, 100000, 12, "biweekly", false, "17.5", "1.46", TestName = "Check for Pay Interest in a maximum range for Rate in 17500")]
        //[TestCase(10, 100001, 12, "monthly", false, "17", "1.42", TestName = "Check for Pay Interest in a minimum range for Rate in 17000")]
        //[TestCase(10, 500000, 12, "biweekly", false, "17", "1.42", TestName = "Check for Pay Interest in a maximum range for Rate in 17000")]
        //[TestCase(10, 500001, 12, "monthly", false, "14.5", "1.21", TestName = "Check for Pay Interest in a minimum range for Rate in 14500")]
        //[TestCase(10, 1000000, 12, "biweekly", false, "14.5", "1.21", TestName = "Check for Pay Interest in a maximum range for Rate in 14500")]
        //[TestCase(10, 1000001, 12, "monthly", false, "13.6", "1.13", TestName = "Check for Pay Interest in a minimum range for Rate in 13500")]
        //[TestCase(10, 99999999, 12, "biweekly", false, "13.6", "1.13", TestName = "Check for Pay Interest in a maximum range for Rate in 13600")]
        //public void TermLoanRangeOfXIRRCalculatorTest(int loanType, int value, int totalPayments, string frequency, bool flagInterest, string rateExpected, string rateMonthlyExpected)
        //{
        //    // arrange
        //    var request = new RestRequest(uri + $"/{loanType}/{value}/{totalPayments}/{frequency}/{flagInterest}", Method.GET);
        //    request.AddHeader(headerKey, headerValue);

        //    // act
        //    IRestResponse response = client.Execute(request);
        //    CalculatorResults calculatorResponse = new JsonDeserializer().Deserialize<CalculatorResults>(response);

        //    // assert
        //    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        //    Assert.IsTrue(calculatorResponse.IsSuccess);
        //    Assert.IsFalse(calculatorResponse.Error[0].HasError);
        //    Assert.AreEqual(rateExpected, calculatorResponse.Result[0].Rate);
        //    Assert.AreEqual(rateMonthlyExpected, calculatorResponse.Result[0].RateMonthly);
        //}

        [TestCase(10, 1000000, 4, "biweekly", "9", true, TestName = "Pay Interest witk interesto Only in 12 Terms ")]
        public void TermLoanWithInterestoOnlyCalculatorTest(int loanType, int value, int totalPayments, string frequency, string paymentTimesExpected, bool flagInterest)
        {
            // arrange
            var request = new RestRequest(uri + $"/{loanType}/{value}/{totalPayments}/{frequency}/{flagInterest}", Method.GET);
            request.AddHeader(head.GetValueOrDefault("key"), head.GetValueOrDefault("value"));

            // act
            IRestResponse response = client.Execute(request);
            CalculatorResults calculatorResponse = new JsonDeserializer().Deserialize<CalculatorResults>(response);

            Console.WriteLine(response.Content);
            Console.WriteLine(calculatorResponse.Result[0].PaymentSchedule[7].Payment);
            Console.WriteLine(calculatorResponse.Result[0].PaymentSchedule[7].DueDate);
            Console.WriteLine(calculatorResponse.Result[0].PaymentSchedule[7].Principal);
            Console.WriteLine(calculatorResponse.Result[0].PaymentSchedule[7].Interest);
            Console.WriteLine(calculatorResponse.Result[0].PaymentSchedule[7].Paymentamount);
            Console.WriteLine(calculatorResponse.Result[0].PaymentSchedule[7].Balance);

            //assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.IsTrue(calculatorResponse.IsSuccess);
            Assert.IsFalse(calculatorResponse.Error[0].HasError);
            Assert.AreEqual(paymentTimesExpected, calculatorResponse.Result[0].PaymentTimes);
            }
        }
    }




