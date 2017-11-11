using System;
namespace tim_dodge
{
	public class NonPlayerObjectSnapshot : PhysicalObjectSnapshot
	{
		public NonPlayerObjectSnapshot(GameObject model) : base(model)
		{
		}

		// Additional properties for living objects
		public int life { get; protected set; }

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
