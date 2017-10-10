using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace tim_dodge
{
	public class Player : PhysicalObject
	{
		public Player(Texture t, Map map, Vector2 pos)
			: base(t, new Sprite(), pos)
		{
			JumpImpulsion = new Vector2(0f, -250f);
			DashForceLeft = new Vector2(-1000f, 0f);
			DashForceRight = -DashForceLeft;
			this.map = map;
			Mass = 50;
		}

		protected Map map;

		protected Vector2 JumpImpulsion;
		protected Vector2 DashForceLeft;
		protected Vector2 DashForceRight;
	
		public bool InJump()
		{
			return !map.onTheGround(this);
		}

		public void Move(KeyboardState state)
		{
			List<Controller.Direction> directions = Controller.GetDirections(state);

			if (directions.Exists(el => el == Controller.Direction.TOP))
			{
				if (!InJump())
					ApplyNewImpulsion(JumpImpulsion);
			}

			if (directions.Exists(el => el == Controller.Direction.LEFT))
			{
				Sprite.ChangeDirection(Controller.Direction.LEFT);
				if (velocity.X >= -3)
					ApplyNewForce(DashForceLeft);
			}

			if (directions.Exists(el => el == Controller.Direction.RIGHT))
			{
				Sprite.ChangeDirection(Controller.Direction.RIGHT);
				if (velocity.X <= 3)
					ApplyNewForce(DashForceRight);
			}

			if (directions.Exists(el => el == Controller.Direction.BOTTOM))
			{
				// TODO : "S'accroupir
			}

			if (Math.Abs(Velocity.X) > 0.3)
			{
				Sprite.ChangeState(Sprite.State.Walk);
			}
			else
			{
				Sprite.ChangeState(Sprite.State.Stay);
			}
				
		}
	}
}
