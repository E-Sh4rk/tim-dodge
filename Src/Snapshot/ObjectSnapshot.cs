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

		public virtual void RestoreModelState()
		{

		}
		public virtual void CaptureModelState()
		{

		}
	}
}
