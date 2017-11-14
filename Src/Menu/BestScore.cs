using System;
namespace tim_dodge
{
	/// <summary>
	/// Some utilities to compare scores.
	/// </summary>
	public class BestScore
	{
		public string name;
		public int score;
		public string replay_filename;

		public static int Compare(BestScore b1, BestScore b2)
		{
			if (b1.score <= b2.score)
				return 1;
			else
				return -1;
		}
	}
}
