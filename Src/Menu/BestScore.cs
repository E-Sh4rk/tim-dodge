using System;
namespace tim_dodge
{
	public class BestScore
	{
		public String name;
		public int score;

		public static int Compare(BestScore b1, BestScore b2)
		{
			if (b1.score <= b2.score)
				return 1;
			else
				return -1;
		}
	}
}
