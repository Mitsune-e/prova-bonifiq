using ProvaPub.Models;

namespace ProvaPub.Services
{
	public class OrderService
	{
		public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
		{
			switch (paymentMethod)
			{
				case "pix":
					new pix().FazPagamento(); 
					break;
				case "creditcard":
					new creditcard().FazPagamento();
					break;
				case "paypal":
					new paypal().FazPagamento();
					break;
			}

			return await Task.FromResult( new Order()
			{
				Value = paymentValue
			});
		}
	}
}
