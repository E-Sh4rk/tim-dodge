using System;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	public class ObjectSnapshot
	{
		// Properties that must be captured/restored. TYPES USED MUST NOT BE MUTABLE.
		public Vector2 pos { get; set; }
		public Color color { get; set; }
		public int sprite_state { get; set; }
		public int sprite_frame { get; set; }
		public Controller.Direction sprite_direction { get; set; }

		public virtual void RestoreModelState(GameObject model_ptr)
		{
			model_ptr.Position = pos;
			model_ptr.color = color;
			model_ptr.Sprite.ChangeState(sprite_state);
			model_ptr.Sprite.ChangeFrame(sprite_frame);
			model_ptr.Sprite.ChangeDirection(sprite_direction);
		}
		public virtual void CaptureModelState(GameObject model_ptr)
		{
			pos = model_ptr.Position;
			color = model_ptr.color;
			sprite_state = model_ptr.Sprite.NowState();
			sprite_frame = model_ptr.Sprite.NowFrame();
			sprite_direction = model_ptr.Sprite.Direction;
		}
	}
	public class PhysicalObjectSnapshot : ObjectSnapshot
	{
		// Additional properties for kinetic objects
		public Vector2 velocity { get; set; }
		public bool ghost { get; set; }

		public override void RestoreModelState(GameObject model_ptr)
		{
			base.RestoreModelState(model_ptr);
			((PhysicalObject)model_ptr).Velocity = velocity;
			((PhysicalObject)model_ptr).Ghost = ghost;
		}
		public override void CaptureModelState(GameObject model_ptr)
		{
			base.CaptureModelState(model_ptr);
			velocity = ((PhysicalObject)model_ptr).Velocity;
			ghost = ((PhysicalObject)model_ptr).Ghost;
		}
	}
	public class PlayerObjectSnapshot : PhysicalObjectSnapshot
	{
		// Additional properties for players
		public int life { get; set; }

		public override void RestoreModelState(GameObject model_ptr)
		{
			base.RestoreModelState(model_ptr);
			int diff = ((Player)model_ptr).Life.value - life;
			if (diff > 0)
				((Player)model_ptr).Life.decr(diff);
			if (diff < 0)
				((Player)model_ptr).Life.incr(-diff);
		}
		public override void CaptureModelState(GameObject model_ptr)
		{
			base.CaptureModelState(model_ptr);
			life = ((Player)model_ptr).Life.value;
		}
	}
	public class NonPlayerObjectSnapshot : PhysicalObjectSnapshot
	{
		// Additional properties for enemies
		public bool damaged { get; set; }
		public bool dead { get; set; }
		// Not really needed since a non-player object is alive iff it is present. So dead should always be true.

		public override void RestoreModelState(GameObject model_ptr)
		{
			base.RestoreModelState(model_ptr);
			((NonPlayerObject)model_ptr).SetState(damaged, dead);
		}
		public override void CaptureModelState(GameObject model_ptr)
		{
			base.CaptureModelState(model_ptr);
			damaged = ((NonPlayerObject)model_ptr).Damaged;
			dead = ((NonPlayerObject)model_ptr).Dead;
		}
	}
}
