using System;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	public class PhysicalObjectSnapshot : ObjectSnapshot
	{
		public PhysicalObjectSnapshot(GameObject model) : base(model) { }

		// Additional properties for kinetic objects
		public Vector2 velocity { get; protected set; }

		public override void RestoreModelState()
		{
			base.RestoreModelState();
		}
		public override void CaptureModelState()
		{
			base.CaptureModelState();
		}
	}
}
