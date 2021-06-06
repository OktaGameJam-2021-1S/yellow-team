using System.Collections.Generic;
using UnityEngine;
using ThirteenPixels.Soda;
public class EnemyHandler : MonoBehaviour
{
	[SerializeField] GameEvent onLevelPassed;
	[SerializeField] private List<Enemy> enemies;
	public int enemiesKilled;

	public List<Enemy> Enemies
	{
		get { return enemies; }
		private set { enemies = value; }
	}

	public void NewGame()
    {
		enemiesKilled = 0;
		enemies.Clear();

	}

	public void AddEnemy(Enemy enemy)
	{
		enemies.Add(enemy);
	}

	/// <summary>
	/// Removes enemy from enemies list, if list is empty raises onLevelPassed SODA event
	/// </summary>
	/// <param name="enemy"></param>
	public void RemoveEnemy(Enemy enemy)
	{
		enemiesKilled++;
		PlayerHUDUI.ScoreChanged(enemiesKilled);
		enemies.Remove(enemy);
		if (enemies.Count == 0)
			onLevelPassed.Raise();
	}
}
