
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace ProvaPub.Test.Customer_Tests
{
    public class CustomerServiceTest
    {

        [Fact]
        public void CustomerService_CheckIfResponseIsTrue()
        {
            var url = "https://localhost:7191/Parte4/CanPurchase?customerId=1&purchaseValue=10";
            var resultado = HttpRequestHelper.Get<CustomerStatus>(url);
            bool ResultadoBool = resultado.IsOk;
            string ResultadoString = resultado.Message;
            ResultadoBool.Should().BeTrue("Passed: Greater than 0 customerId and purchaseValue at returns True");
            ResultadoString.Should().Be("Ok");
        }

        [Fact]
        public void CustomerService_CheckIfVariablesIs0OrLess() 
        {

            var url = "https://localhost:7191/Parte4/CanPurchase?customerId=0&purchaseValue=0";
            var resultado = HttpRequestHelper.Get<CustomerStatus>(url);
            bool ResultadoBool = resultado.IsOk;
            string ResultadoString = resultado.Message;
            ResultadoBool.Should().BeFalse("Passed: 0 at customerId or purchaseValue returns False ");
            ResultadoString.Should().Be(new ArgumentOutOfRangeException("customerId").Message);
        }
    }
}
