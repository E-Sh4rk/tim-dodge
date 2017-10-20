using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	public class Player : PhysicalObject
	{

		public Heart Life;
		public Stat Score;

		public bool IsDead()
		{
			return (Life.value == 0);
		}

		public Player(Vector2 pos, Heart Life, Stat Score, GameInstance gi)
			: base(gi.LoadTexture("character/Tim"), new Sprite("Content.character.TimXml.xml"), pos)
		{
			JumpImpulsion = new Vector2(0f, -250f);
			DashForceLeft = new Vector2(-1500f, 0f);
			DashForceRight = -DashForceLeft;
			this.map = gi.map;
			Mass = 50;
			gameInst = gi;
			Sprite.ChangeDirection(Controller.Direction.RIGHT);

			//int InitialLife = 10;

			this.Life = Life;

			this.Score = Score;
			min_time_between_squat = Sprite.GetFrameTimeOfState((int)State.Squat) * 8;
		}

		enum State
		{
			Stay = 0,
			Walk = 1,
			Jump = 2,
 			Squat = 3,
			JumpH = 4
		}

		protected SoundEffect jump;
		protected Map map;

		protected Vector2 JumpImpulsion;
		protected Vector2 DashForceLeft;
		protected Vector2 DashForceRight;

		protected GameInstance gameInst;

		protected float elapsed_since_last_jump = 0;
		const float min_time_between_jump = 0.25f;
		float min_time_between_squat;
 		protected float elapsed_since_last_squat = 0;
 		protected bool squatMode = false;

		public bool CanJump()
		{
			return map.nearTheGround(this) && elapsed_since_last_jump >= min_time_between_jump;
		}

		public bool CanSquat()
 		{
 			return elapsed_since_last_squat >= min_time_between_squat;
 		}

		public void Move(KeyboardState state, GameTime gameTime)
		{
			List<Controller.Direction> directions = Controller.GetDirections(state);

			elapsed_since_last_jump += (float)gameTime.ElapsedGameTime.TotalSeconds;
			elapsed_since_last_squat += (float)gameTime.ElapsedGameTime.TotalSeconds;

			if (CanSquat())
				squatMode = false;

			if (directions.Exists(el => el == Controller.Direction.TOP))
			{
				if (CanJump())
				{
					GameManager.sounds.playSound(Sound.SoundName.jump);
					elapsed_since_last_jump = 0;
					map.magnetizeToGround(this);
					ApplyNewImpulsion(JumpImpulsion);
				}
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
				if (CanSquat())
				{
					ChangeSpriteState((int)State.Squat);
					elapsed_since_last_squat = 0;
					squatMode = true;
				}
			}

			if (!squatMode)
			{
				if (!map.nearTheGround(this))
				{
					if (Math.Abs(Velocity.X) > 2)
						ChangeSpriteState((int)State.Jump);
					else
						ChangeSpriteState((int)State.JumpH);
					
				}
					
				else
				{
					if (Math.Abs(Velocity.X) > 0.7)
						ChangeSpriteState((int)State.Walk);
					else
						ChangeSpriteState((int)State.Stay);
				}
			}
		}

		const double time_invicibility = 0.5f;
		protected double last_damage_time = 0f;
		protected override void ApplyCollision(Vector2 imp, int id, GameTime gt)
		{
			base.ApplyCollision(imp, id, gt);
			// Apply damage if necessary

			List<Enemy> es = gameInst.enemies.ListEnemies.FindAll(en => en.ID == id);

			if (es.Count > 0 && gt.TotalGameTime.TotalSeconds - last_damage_time >= time_invicibility)
			{
				foreach (Enemy e in es)
				{
					Life.decr(e.Damage);
					e.TouchPlayer();
				}
				color = Color.Red;
				last_damage_time = gt.TotalGameTime.TotalSeconds;
			}

		}
		public override void UpdatePosition(List<PhysicalObject> objects, Map map, GameTime gameTime)
		{
			base.UpdatePosition(objects, map, gameTime);
			if (gameTime.TotalGameTime.TotalSeconds - last_damage_time >= time_invicibility)
				color = Color.White;
		}
	}
}
