using System;

namespace tim_tests
{
	class MainClass
	{
		public static int Main(string[] args)
		{
			// NOTE: Pas reussi à faire fonctionner NUnit...
			// TODO
			Console.WriteLine("Running default tests...");
			Tests t = new Tests();
			try
			{
				t.Collisions();
				Console.WriteLine("Passed.");
				return 0;
			}
			catch
			{
				Console.WriteLine("Failed.");
			}
			finally { t.Dispose(); }
			return -1;
		}
	}
}
