using System;

namespace tim_tests
{
	class MainClass
	{
		public static int Main(string[] args)
		{
			int failed = 0;

			// Test 1, oui ca compte comme un test
			int test1 = 1 + 1;
			if (test1 == 2)
				Console.WriteLine("Test 1 passed!");
			else
			{
				Console.WriteLine("Test 1 failed!");
				failed++;
			}

			// TODO: tests collisions

			if (failed == 0)
				Console.WriteLine("All tests passed!");
			else
				Console.WriteLine("Some tests failed!");
			return failed;
		}
	}
}
