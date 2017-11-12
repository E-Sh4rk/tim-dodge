using System;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
	public class ObjectSnapshot
	{
		public ObjectSnapshot(GameObject model)
		{
			model_ptr = model;
		}

		public GameObject model_ptr { get; } // Pointer to the involved game object

		// Properties that must be captured/restored. TYPES USED MUST NOT BE MUTABLE.
		public Vector2 pos { get; protected set; }
		public Color color { get; protected set; }
		public int sprite_state { get; protected set; }
		public int sprite_frame { get; protected set; }
		public Controller.Direction sprite_direction { get; protected set; }

		public virtual void RestoreModelState()
		{
			model_ptr.Position = pos;
			model_ptr.color = color;
			model_ptr.Sprite.ChangeState(sprite_state);
			model_ptr.Sprite.ChangeFrame(sprite_frame);
			model_ptr.Sprite.ChangeDirection(sprite_direction);
		}
		public virtual void CaptureModelState()
		{
			pos = model_ptr.Position;
			color = model_ptr.color;
			sprite_state = model_ptr.Sprite.NowState();
			sprite_frame = model_ptr.Sprite.NowFrame();
			sprite_direction = model_ptr.Sprite.Direction;
		}
	}
}
