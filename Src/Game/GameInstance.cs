using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tim_dodge
{
	/// <summary>
	/// Defines a game play.
	/// </summary>
	public class GameInstance
	{
		public List<Player> players;
		public int InitialNbPlayers;

		public LevelManager Level { get; protected set; }

		public FuelBar Fuel { get; protected set; }	

		public float time_multiplicator = 1f;

		public bool focus;

		const int max_snapshots = 10000;
		Snapshot[] snapshots = new Snapshot[max_snapshots];
		int current_snapshot_index = 0;
		int oldest_snapshot_index = 0;
		int newest_snapshot_index = -1;
		bool mode_rewind = false;
		bool mode_replay = false;
		// TODO: modify falling&walking objects to not use alea if the future is known

		public int GetGlobalScore()
		{
			int max = 0;
			foreach (Player p in players)
			{
				if (p.Score.value > max)
					max = p.Score.value;
			}
			return max;
		}
		public void AddToScores(int v)
		{
			foreach (Player p in players)
				p.Score.incr(v);
		}
		public void SetTextColor(Color c)
		{
			foreach (Player p in players)
			{
				p.Score.Color = c;
				p.Life.Color = c;
				if (Fuel != null)
					Fuel.ColorText = c;
			}
		}

		public GameManager gm;

		public GameInstance(ChooseMap.Maps MapLoad, int nbPlayer, GameManager gm)
		{
			this.gm = gm;
			InitialNbPlayers = nbPlayer;

			Debug.Assert(nbPlayer >= 1 && nbPlayer <=2);
			players = new List<Player>();

			if (nbPlayer == 1 && MapLoad == ChooseMap.Maps.StairMap)
			{
				players.Add(new Player(new Vector2(TimGame.GAME_WIDTH / 4, 300),
										   GetNewScorePosition(0), this));
			}

			else
			{
				for (int i = nbPlayer - 1; i > -1; i--)
				{
					players.Add(new Player(new Vector2((TimGame.GAME_WIDTH / nbPlayer) * i + (TimGame.GAME_WIDTH / nbPlayer / 2), 300),
										   GetNewScorePosition(i), this));
				}

				if (nbPlayer > 1)
				{
					Player p1 = players[0];
					Player p2 = players[1];
					p1.ColorPlayer = Color.PaleTurquoise;
					p2.ColorPlayer = Color.PaleVioletRed;
					p1.Score.Title = "Right Score : ";
					p2.Score.Title = "Left Score : ";
					p1.Life.sfuel = "Right Player : ";
					p2.Life.sfuel = "Left Player : ";
				}
			}

			Level = new LevelManager(this, MapLoad);
			focus = true;
			if (nbPlayer == 1)
				Fuel = new FuelBar(GetNewFuelPosition(0), gm, Color.Black);
		}

		public Vector2 GetNewScorePosition(int nb)
		{
			return new Vector2(30, 20+50*nb);
		}

		public Vector2 GetNewFuelPosition(int nb)
		{
			return new Vector2(500, 20+50*nb);
		}

		public void SaveReplay(string file)
		{
			List<Snapshot> lst = new List<Snapshot>();
			int i = oldest_snapshot_index;
			while (i != current_snapshot_index)
			{
				lst.Add(snapshots[i]);
				i = mod(i + 1,max_snapshots);
			}
			try { Replay.FromSnapshotList(lst).ExportToFile(file); } catch { }
		}
		public void LoadReplay(string file)
		{
			List<Snapshot> lst = null;
			try { lst = Replay.ImportFromFile(file).ToSnapshotList(this); }
			catch { }

			if (lst != null)
			{
				oldest_snapshot_index = 0;
				current_snapshot_index = 0;
				newest_snapshot_index = -1;
				foreach (Snapshot s in lst)
				{
					if (newest_snapshot_index + 1 >= max_snapshots)
						break;
					newest_snapshot_index++;
					snapshots[newest_snapshot_index] = s;
				}
				mode_replay = true;
			}
		}

		int mod(int x, int m)
		{
			int r = x % m;
			return r < 0 ? r + m : r;
		}
		public void Update(GameTime gameTime)
		{
			KeyboardState state = Keyboard.GetState();

			if (players.Count == 1)
			{
				if (Controller.RewindKeyDown(state))
				{
					if (Fuel.isFull && !mode_rewind)
					{
						mode_rewind = true;
						Load.sounds.playRewind();
						Fuel.decr(50); // When shift is pushed, the fuel bar decrease of 50%
					}

					if (Fuel.isEmpty)
					{
						mode_rewind = false;
						Load.sounds.stopRewind();
					}

					if (mode_rewind)
					{
						Fuel.decr(0.5f); // In each update the fuel bar decrease of 0.5%
					}
				}
				else
				{
					if (mode_rewind)
					{
						mode_rewind = false;
						Load.sounds.stopRewind();
					}

				}
			}
			if (mode_rewind)
			{
				if (current_snapshot_index != oldest_snapshot_index)
					current_snapshot_index = mod(current_snapshot_index - 1, max_snapshots);
				snapshots[current_snapshot_index].RestoreGameState(this);
			}
			else if (mode_replay)
			{
				if (current_snapshot_index != newest_snapshot_index)
					current_snapshot_index = mod(current_snapshot_index + 1, max_snapshots);
				snapshots[current_snapshot_index].RestoreGameState(this);
			}
			else
			{
				Snapshot s = new Snapshot();
				s.CaptureGameState(this);
				snapshots[current_snapshot_index%max_snapshots] = s;
				newest_snapshot_index = current_snapshot_index;
				current_snapshot_index = mod(current_snapshot_index + 1, max_snapshots);
				if (current_snapshot_index == oldest_snapshot_index)
					oldest_snapshot_index = mod(oldest_snapshot_index + 1, max_snapshots);

				// Update!
				float insensible_elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
				float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds * time_multiplicator;

				// First, the map
				Level.Current.map.Update(elapsed);

				// Move players
				if (players.Count >= 1)
					players[0].Move(Controller.GetDirectionsPlayer1(state), insensible_elapsed); // Insensible to time speed
				if (players.Count >= 2)
					players[1].Move(Controller.GetDirectionsPlayer2(state), insensible_elapsed); // Insensible to time speed

				// All physical objects
				List<PhysicalObject> phys_obj = new List<PhysicalObject>();
				phys_obj.AddRange(players);
				phys_obj.AddRange(Level.Current.falling.EnemiesList);
				phys_obj.AddRange(Level.Current.walking.EnemiesList);

				foreach (PhysicalObject po in phys_obj)
					po.UpdateSprite(po is Player ? insensible_elapsed : elapsed);
				foreach (PhysicalObject po in phys_obj)
					po.ApplyForces(phys_obj, Level.Current.map, po is Player ? insensible_elapsed : elapsed);
				foreach (PhysicalObject po in phys_obj)
					po.ApplyCollisions(phys_obj, Level.Current.map, po is Player ? insensible_elapsed : elapsed);
				foreach (PhysicalObject po in phys_obj)
					po.UpdatePosition(phys_obj, Level.Current.map, po is Player ? insensible_elapsed : elapsed);

				// Additional operations on players
				foreach (Player p in players)
				{
					if (p.IsOutOfBounds())
						p.Life.decr(p.Life.value);
					if (p.IsDead())
					{
						p.ChangeSpriteState((int)Player.State.Dead);
						if (players.Count > 1)
						{
							players.Remove(p);
							if (players.Count == 1)
								Fuel = new FuelBar(GetNewFuelPosition(0), gm, Color.Black);
							break;
						}
						
					}
					if (players.Count == 1)
						Fuel.Update();
				}

				// Logic of levels / ennemies
				Level.Update(elapsed);
				Level.Current.falling.Update(elapsed);
				Level.Current.walking.Update(elapsed);

			}
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			Level.Draw(spriteBatch);

			foreach (Player p in players)
				p.Draw(spriteBatch);
			foreach (NonPlayerObject en in Level.Current.falling.EnemiesList)
				en.Draw(spriteBatch);
			foreach (NonPlayerObject en in Level.Current.walking.EnemiesList)
				en.Draw(spriteBatch);
			foreach (Player p in players)
			{
				p.Score.Draw(spriteBatch);
				p.Life.Draw(spriteBatch);
				if (players.Count == 1)
					Fuel.Draw(spriteBatch);
			}
		}

	}
}
