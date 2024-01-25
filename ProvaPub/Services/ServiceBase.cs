using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
    public class ServiceBase
    {
        public TestDbContext _ctx;
        public int TotalItems;
        public int FirstItem;

        public ServiceBase(TestDbContext ctx)
        { 
            _ctx = ctx;
            
        }

        public void setItemsInfo(int Page)
        {
            TotalItems = 10;
            FirstItem = TotalItems * (Page - 1);
        }
    }
}