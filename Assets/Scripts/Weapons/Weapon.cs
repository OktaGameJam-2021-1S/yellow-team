using UnityEngine;
using System.Collections.Generic;

public class Weapon : MonoBehaviour
{
	public enum WeaponPosition
    {
		none,
		hand,
    }

	public enum WeaponType
	{
		shoot,
		melee,
	}

	[SerializeField] GameObject shellPrefab;
	[SerializeField] WeaponPosition weaponPosition;
	[SerializeField] WeaponType weaponType;
	[SerializeField] Transform modelPrefab;
	[SerializeField] Transform shotParent;
	[SerializeField] Transform bulletSpawn;
	[SerializeField] int poolSize = 20;
	[SerializeField] float fireRate;
	[SerializeField] float damage;
	[SerializeField] float speedBullet = 1;

	Queue<GameObject> pool;

    public float FireRate { get => fireRate; }
    public float Damage { get => damage; }
    public WeaponPosition Position { get => weaponPosition; }
    public WeaponType Type { get => weaponType; }

    private void Awake()
	{
		pool = new Queue<GameObject>(poolSize);
		ResizePool(poolSize);
	}

    #region Pool

	public void ResizePool(int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			var obj = Instantiate(shellPrefab, shotParent);
			obj.SetActive(false);
			pool.Enqueue(obj);
		}
	}

	public void Dispose()
	{
		while (pool.Count > 0)
		{
			Destroy(pool.Dequeue());
		}
	}
    #endregion

	/// <summary>
	/// Calls when shell needs to go back to the pool
	/// </summary>
	/// <param name="shell"></param>
    public void DeactivateShell(GameObject shell)
	{
		shell.SetActive(false);
		shell.GetComponent<Collider>().enabled = true;
		try
		{
			shell.transform.position = transform.position;
			pool.Enqueue(shell);
		}
		catch (System.Exception e)
		{
			//Debug.Log("Shooted of this shell is already dead");
			Destroy(shell);
		}
	}

	/// <summary>
	/// Shoots one prefab Shell with given DamageReport forward
	/// </summary>
	/// <param name="damageReport">damage info</param>
	public void Shoot(DamageReport damageReport)
	{
		var newShell = pool.Dequeue();
		newShell.transform.position = bulletSpawn.position;
		newShell.transform.rotation = transform.rotation;
		newShell.SetActive(true);
		newShell.GetComponent<Shell>().Shoot(this, damageReport, speedBullet);
		if (pool.Count <= 1)
			ResizePool(poolSize);
	}

	public void UpdateWeaponPosition(Vector3 position)
    {
		modelPrefab.transform.position = position;
    }
}
