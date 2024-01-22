namespace ProvaPub.Services
{
	public class RandomService
	{
		int seed;
		Random random;
		public RandomService()
		{
			seed = Guid.NewGuid().GetHashCode();
			random = new Random(seed);
		}
		public int GetRandom()
		{
			return random.Next(100);
		}

	}
}
