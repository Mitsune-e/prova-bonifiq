namespace ProvaPub.Models
{
	public class Customer : Base
	{
		public ICollection<Order> Orders { get; set; }
	}
}
