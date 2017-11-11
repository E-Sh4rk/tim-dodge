using System;
namespace tim_dodge
{
	public class NonPlayerObjectSnapshot : PhysicalObjectSnapshot
	{
		public NonPlayerObjectSnapshot(GameObject model) : base(model)
		{
		}

		// Additional properties for players
		public bool damaged { get; protected set; }
		public bool dead { get; protected set; }
		// Not really needed since a non-player object is alive iff it is present. So dead should always be true.

		public override void RestoreModelState()
		{
			base.RestoreModelState();
			((NonPlayerObject)model_ptr).SetState(damaged, dead);
		}
		public override void CaptureModelState()
		{
			base.CaptureModelState();
			damaged = ((NonPlayerObject)model_ptr).Damaged;
			dead = ((NonPlayerObject)model_ptr).Dead;
		}
	}
}
