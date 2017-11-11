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
		public int sprite_state { get; protected set; }
		public int sprite_frame { get; protected set; }

		public virtual void RestoreModelState()
		{
			model_ptr.Position = pos;
			model_ptr.Sprite.ChangeState(sprite_state);
			model_ptr.Sprite.ChangeFrame(sprite_frame);
		}
		public virtual void CaptureModelState()
		{
			pos = model_ptr.Position;
			sprite_state = model_ptr.Sprite.NowState();
			sprite_frame = model_ptr.Sprite.NowFrame();
		}
	}
}
