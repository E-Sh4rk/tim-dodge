using System;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;

namespace tim_dodge
{
    /// <summary>
    /// Serializable structure that represents a 3D vector (because Vector3 is not serializable).
    /// </summary>
    [Serializable]
	public struct SVector
	{
		public SVector(float x, float y) { this.x = x; this.y = y; }
		public float x;
		public float y;

		public Vector2 ToVector2()
		{
			return new Vector2(x,y);
		}
		static public SVector FromVector2(Vector2 v)
		{
			return new SVector(v.X,v.Y);
		}
	}
    /// <summary>
    /// Serializable structure that represents a color (because Color is not serializable).
    /// </summary>
	[Serializable]
	public struct SColor
	{
		public SColor(int r, int g, int b, int a)
		{
			this.a = a;
			this.r = r;
			this.g = g;
			this.b = b;
		}
		public int a;
		public int r;
		public int g;
		public int b;

		public Color ToColor()
		{
			return new Color(r, g, b, a);
		}
		static public SColor FromColor(Color c)
		{
			return new SColor(c.R, c.G, c.B, c.A);
		}
	}
    /// <summary>
    /// Serializable structure that can capture and restore all information relative to a basic game object.
    /// </summary>
	[Serializable]
	[XmlInclude(typeof(PhysicalObjectSnapshot))]
	public class ObjectSnapshot
	{
		// Properties that must be captured/restored. TYPES USED MUST NOT BE MUTABLE.
		public SVector pos;
		public SColor color;
		public int sprite_state;
		public int sprite_frame;
		public Controller.Direction sprite_direction;

		public virtual void RestoreModelState(GameObject model_ptr)
		{
			model_ptr.Position = pos.ToVector2();
			model_ptr.color = color.ToColor();
			model_ptr.Sprite.ChangeState(sprite_state);
			model_ptr.Sprite.ChangeFrame(sprite_frame);
			model_ptr.Sprite.ChangeDirection(sprite_direction);
		}
		public virtual void CaptureModelState(GameObject model_ptr)
		{
			pos = SVector.FromVector2(model_ptr.Position);
			color = SColor.FromColor(model_ptr.color);
			sprite_state = model_ptr.Sprite.NowState();
			sprite_frame = model_ptr.Sprite.NowFrame();
			sprite_direction = model_ptr.Sprite.Direction;
		}
	}
    /// <summary>
    /// Serializable structure that can capture and restore all information relative to a physical game object.
    /// </summary>
	[Serializable]
	[XmlInclude(typeof(PlayerObjectSnapshot))]
	[XmlInclude(typeof(NonPlayerObjectSnapshot))]
	public class PhysicalObjectSnapshot : ObjectSnapshot
	{
		// Additional properties for kinetic objects
		public SVector velocity;
		public bool ghost;
		public SVector last_velocity;

		public override void RestoreModelState(GameObject model_ptr)
		{
			base.RestoreModelState(model_ptr);
			((PhysicalObject)model_ptr).Velocity = velocity.ToVector2();
			((PhysicalObject)model_ptr).Ghost = ghost;
			((PhysicalObject)model_ptr).last_computed_velocity = last_velocity.ToVector2();
		}
		public override void CaptureModelState(GameObject model_ptr)
		{
			base.CaptureModelState(model_ptr);
			velocity = SVector.FromVector2(((PhysicalObject)model_ptr).Velocity);
			ghost = ((PhysicalObject)model_ptr).Ghost;
			last_velocity = SVector.FromVector2(((PhysicalObject)model_ptr).last_computed_velocity);
		}
	}
    /// <summary>
    /// Serializable structure that can capture and restore all information relative to a player object.
    /// </summary>
	[Serializable]
	public class PlayerObjectSnapshot : PhysicalObjectSnapshot
	{
		// Additional properties for players
		public int life;
		public int score;

		public override void RestoreModelState(GameObject model_ptr)
		{
			base.RestoreModelState(model_ptr);
			int diff = ((Player)model_ptr).Life.value - life;
			if (diff > 0)
				((Player)model_ptr).Life.decr(diff);
			if (diff < 0)
				((Player)model_ptr).Life.incr(-diff);
			((Player)model_ptr).Score.set(score);
		}
		public override void CaptureModelState(GameObject model_ptr)
		{
			base.CaptureModelState(model_ptr);
			life = ((Player)model_ptr).Life.value;
			score = ((Player)model_ptr).Score.value;
		}
	}
    /// <summary>
    /// Serializable structure that can capture and restore all information relative to a non-player object.
    /// </summary>
	[Serializable]
	public class NonPlayerObjectSnapshot : PhysicalObjectSnapshot
	{
		// Additional properties for enemies
		public bool damaged;
		public bool dead;
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
