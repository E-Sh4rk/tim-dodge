using System;
namespace tim_dodge
{
	public class PlayerObjectSnapshot : PhysicalObjectSnapshot
	{
		public PlayerObjectSnapshot(GameObject model) : base(model)
		{
		}

		// Additional properties for players
		public bool dead { get; protected set; }
		// Not really needed since a non-player object is alive iff it is present. So dead should always be true.

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
