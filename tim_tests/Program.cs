using System;
using Microsoft.Xna.Framework.Graphics;

namespace tim_tests
{
	class MainClass
	{
		static SimulatedGame g = null;

		static Texture2D loadTexture(string str)
		{
			var stream = typeof(MainClass).Module.Assembly.GetManifestResourceStream("tim_tests.Resources."+str);
			return g.textureFromStream(stream);
		}

		public static int Main(string[] args)
		{
			g = new SimulatedGame();
			g.RunOneFrame();

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
			Texture2D t1 = loadTexture("Collisions.2.png");
			tim_dodge.GameObject o1 = new tim_dodge.GameObject(new tim_dodge.Texture(t1), null);

			if (failed == 0)
				Console.WriteLine("All tests passed!");
			else
				Console.WriteLine("Some tests failed!");
			return failed;
		}
	}
}
