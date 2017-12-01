using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace tim_dodge
{
	/// <summary>
	/// Represents Tim, the character controlled by the user.
	/// </summary>
	public class Player : PhysicalObject
	{
		public Heart Life { get; protected set; }
		public Stat Score { get; protected set; }
		public FuelBar Fuel { get; protected set; }

		public bool IsDead()
		{
			return (Life.value == 0);
		}

		public Player(Vector2 pos, Vector2 scorePosition, Vector2 fuelPosition, GameInstance gi)
			: base(Load.TimTexture, new Sprite("Content.character.TimXml.xml"), pos)
		{
			JumpImpulsion = new Vector2(0f, -180f);//-250f);//-180f);
			JumpMore = new Vector2(0, -150);
			DashForceLeft = new Vector2(-1500f, 0f);
			DashForceRight = -DashForceLeft;
			Mass = 50;
			gameInst = gi;
			Sprite.ChangeDirection(Controller.Direction.RIGHT);

			this.Life = new Heart(scorePosition, Color.Black);
			this.Score = new Stat(Load.FontScore, Color.Black, "Score : ", 0);
			this.Score.Position = scorePosition;
			this.Fuel = new FuelBar(fuelPosition, gameInst.gm, Color.Black);
			//this.Score = Score;
			min_time_between_squat = Sprite.GetFrameTimeOfState((int)State.Squat) * 8;
		}

		public enum State
		{
			Stay = 0,
			Walk = 1,
			Jump = 2,
			Squat = 3,
			JumpH = 4,
			Tie = 5,
			Dead = 6
		}

		protected SoundEffect jump;
		protected Vector2 JumpImpulsion;
		protected Vector2 JumpMore;

		protected Vector2 DashForceLeft;
		protected Vector2 DashForceRight;

		protected GameInstance gameInst;

		protected float elapsed_since_last_jump = 0;
		const float min_time_between_jump = 0.25f;
		float min_time_between_squat;
 		protected float elapsed_since_last_squat = 0;
 		protected bool squatMode = false;

		public bool CanMove()
		{
			return !IsDead();
		}

		public bool CanJump()
		{
			return gameInst.Level.map.pMap.nearTheGround(this) && elapsed_since_last_jump >= min_time_between_jump;
		}

		public bool CanSquat()
 		{
 			return elapsed_since_last_squat >= min_time_between_squat;
 		}

		public void Move(List<Controller.Direction> directions, float elapsed)
		{
			elapsed_since_last_jump += elapsed;
			elapsed_since_last_squat += elapsed;

			if (gameInst.Level.Current.Beginning)
			{
				if (Sprite.NowState() != (int)State.Tie)
				{
					ChangeSpriteState((int)State.Tie);
				}
			}

			else
			{
				if (CanMove())
				{
					if (CanSquat())
						squatMode = false;

					if (directions.Exists(el => el == Controller.Direction.TOP))
					{
						if (CanJump())
						{
							Load.sounds.playSound(Sound.SoundName.jump);
							elapsed_since_last_jump = 0;
							gameInst.Level.map.pMap.magnetizeToGround(this);
							ApplyNewImpulsion(JumpImpulsion);
						}
						else
						{
							if (Velocity.Y < 0) // if we are in the first state of jumping 
								ApplyNewForce(JumpMore);
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
						if (!gameInst.Level.map.pMap.nearTheGround(this))
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
			}
		}

		const double time_invicibility = 0.5f;
		const double time_bonus = 0.5f;
		const double time_poison = 2f;

		protected double last_damage_time = 0f;
		protected double last_bonus_time = 0f;
		protected double poison_remaining_time = 0f;

		protected override void ApplyCollision(Vector2 imp, PhysicalObject obj, float elapsed)
		{
			base.ApplyCollision(imp, obj, elapsed);

			if (obj is NonPlayerObject)
			{
				NonPlayerObject e = (NonPlayerObject)obj;
				e.TouchPlayer();
				// Bonus
				if (e.Bonus > 0 || e.Life > 0)
				{
					Life.incr(e.Life);
					Score.incr(e.Bonus);
					color = Color.Yellow;
					last_bonus_time = 0f;
				}

				switch (e.poisonState)
				{
					case NonPlayerObject.PoisonState.Horizontal:
						if (!gameInst.flipH)
						{
							poison_remaining_time += time_poison;
							gameInst.flipH = true;
							color = Color.DarkViolet;
							last_damage_time = 0f; // It is for the color + immunity
						}
						break;
					case NonPlayerObject.PoisonState.Vertical:
						if (!gameInst.flipV)
						{
							poison_remaining_time += time_poison;
							gameInst.flipV = true;
							color = Color.DarkViolet;
							last_damage_time = 0f; // It is for the color + immunity
						}
						break;
					case NonPlayerObject.PoisonState.Rotation:
						if (!gameInst.rotation)
						{
							poison_remaining_time += time_poison;
							gameInst.rotation = true;
							color = Color.DarkViolet;
							last_damage_time = 0f; // It is for the color + immunity
						}
						break;
					case NonPlayerObject.PoisonState.Nothing:
						break;
				}

				// Damage
				int damage = 0;
				if (e is Monstar)
				{
					if (!e.Damaged) // If e is damaged, it is because the collision has already been treated
					{
						float y_player = position.Y + Size.Y;
						float y_e = e.Position.Y + e.Size.Y / 2;
						if (y_player < y_e)
						{
							velocity.Y = 0; // To give an impression of bouncing
							e.SufferDamage();
						}
						else
						{
							damage = e.Damage;
							e.SufferDamage();
						}
					}
				}
				else
					damage = e.Damage;
				// If we are not invincible ..
				if (last_damage_time >= time_invicibility)
				{
					if (damage > 0)
					{
						Life.decr(damage);
						color = Color.IndianRed;
						last_damage_time = 0f;
					}
				}
			}
		}

		public override void UpdatePosition(List<PhysicalObject> objects, Map map, float elapsed)
		{
			last_bonus_time += elapsed;
			last_damage_time += elapsed;
			poison_remaining_time = Math.Max(0, poison_remaining_time - elapsed);

			if (poison_remaining_time < Math.Pow(10, -3))
			{
				gameInst.rotation = false;
				gameInst.flipH = false;
				gameInst.flipV = false;
			}

			base.UpdatePosition(objects, map, elapsed);
			if (last_damage_time >= time_invicibility && last_bonus_time >= time_bonus)
				color = Color.White;
		}
	}
}
