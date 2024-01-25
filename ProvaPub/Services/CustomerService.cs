using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
    public class CustomerService : ServiceBase
    {
        
        public CustomerService(TestDbContext ctx) : base(ctx) 
        {

        }

        public CustomerList ListCustomers(int page)
        {
            base.setItemsInfo(page);
            var allItems = new CustomerList() {HasNext = false, TotalCount = TotalItems, Customers = _ctx.Customers.ToList() };

            if (FirstItem >= allItems.Customers.Count)
            {
                throw new Exception("Não foi encontrado resultados para essa pagina");
            }

            allItems.Customers = allItems.Customers.GetRange(FirstItem, TotalItems);
            return allItems;
        }

        public async Task<CustomerStatus> CanPurchase(int customerId, decimal purchaseValue)
        {
            var status = new CustomerStatus()
            {
                IsOk = true,
                Message = "Ok"
            };

            if (customerId <= 0) 
            { 
                status.IsOk = false;
                status.Message = new ArgumentOutOfRangeException(nameof(customerId)).Message;
                return status;
            }

            if (purchaseValue <= 0) 
            {
                status.IsOk = false;
                status.Message = new ArgumentOutOfRangeException(nameof(purchaseValue)).Message;
                return status;
            }

            //Business Rule: Non registered Customers cannot purchase
            var customer = await _ctx.Customers.FindAsync(customerId);
            if (customer == null)
            {
                status.IsOk = false;
                status.Message = new InvalidOperationException($"Customer Id {customerId} does not exists").Message ;
            }

            //Business Rule: A customer can purchase only a single time per month
            var baseDate = DateTime.UtcNow.AddMonths(-1);
            var ordersInThisMonth = await _ctx.Orders.CountAsync(s => s.CustomerId == customerId && s.OrderDate >= baseDate);
            if (ordersInThisMonth > 0)
            {
                status.IsOk = false;
                status.Message = "A customer can purchase only a single time per month";
                return status;
            }

            //Business Rule: A customer that never bought before can make a first purchase of maximum 100,00
            var haveBoughtBefore = await _ctx.Customers.CountAsync(s => s.Id == customerId && s.Orders.Any());
            if (haveBoughtBefore == 0 && purchaseValue > 100)
            {
                status.IsOk = false;
                status.Message = "A customer that never bought before can make a first purchase of maximum 100,00";
            }
                

            return status;
        }

    }
}
