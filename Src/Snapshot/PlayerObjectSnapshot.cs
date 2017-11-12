using System;
namespace tim_dodge
{
	public class PlayerObjectSnapshot : PhysicalObjectSnapshot
	{
		public PlayerObjectSnapshot(GameObject model) : base(model)
		{
		}

		// Additional properties for living objects
		public int life { get; protected set; }

		public override void RestoreModelState()
		{
			base.RestoreModelState();
			int diff = ((Player)model_ptr).Life.value - life;
			if (diff > 0)
				((Player)model_ptr).Life.decr(diff);
			if (diff < 0)
				((Player)model_ptr).Life.incr(-diff);
		}
		public override void CaptureModelState()
		{
			base.CaptureModelState();
			life = ((Player)model_ptr).Life.value;
		}
	}
}
