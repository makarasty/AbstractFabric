public class Program()
{
	public static void Main(string[] args)
	{
		var logger = new CombinedLogger
		{
			Format = "Hello {0}",
			Color = ConsoleColor.Red
		};
		logger.Log("world ♥");
	}
}