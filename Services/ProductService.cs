using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
	public class ProductService : ServiceBase
	{


		public ProductService(TestDbContext ctx) : base(ctx)
		{
			
		}

		public ProductList  ListProducts(int page)
		{   
			base.setItemsInfo(page);
            var allItems = new ProductList() { HasNext = false, TotalCount = TotalItems, Products = _ctx.Products.ToList()};

			if (FirstItem >= allItems.Products.Count())
				throw new Exception("Não foi encontrado resultados para essa página.");

			allItems.Products = allItems.Products.GetRange(FirstItem, TotalItems);
			return allItems;
		}

	}
}
