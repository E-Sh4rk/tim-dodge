using System;
using Microsoft.Xna.Framework;
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
			Console.WriteLine("Simulated game initialized!");

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

			// Test 2, collisions
			int test2 = 0;
			Texture2D t1 = loadTexture("Collisions.1.png");
			tim_dodge.PhysicalObject o1 = new tim_dodge.PhysicalObject(new tim_dodge.Texture(t1), null, new Vector2(0f, 0f));
			Texture2D t2 = loadTexture("Collisions.2.png");
			tim_dodge.PhysicalObject o2 = new tim_dodge.PhysicalObject(new tim_dodge.Texture(t2), null, new Vector2(0f, 0f));
			if (tim_dodge.Collision.object_collision(o1, o2) != null)
				test2++;
			o2.Position = new Vector2(1.0f, 0.0f);
			if (tim_dodge.Collision.object_collision(o1, o2) == null)
				test2++;
			failed += test2;
			if (test2 == 0)
				Console.WriteLine("Test 2 passed!");
			else
				Console.WriteLine("Test 2 failed!");

			g.Exit();
			g.Dispose();
			Console.WriteLine("Simulated game disposed!");

			if (failed == 0)
				Console.WriteLine("All tests passed!");
			else
				Console.WriteLine("Some tests failed!");
			return failed;
		}
	}
}
