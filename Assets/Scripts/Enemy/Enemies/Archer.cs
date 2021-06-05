using UnityEngine;

public class Archer : WalkingEnemy
{
	[SerializeField] EnemyAimer aimer;
	[SerializeField] Weapon[] weapons;

	int currentWeaponIndex = 0;
	Weapon currentWeapon;
	float lastShootTime;

	protected override void Death(Entity killer)
	{
		currentWeapon.Dispose();
		base.Death(killer);
	}

	protected new void Awake()
	{
		currentWeapon = weapons[currentWeaponIndex];

		base.Awake();
		if (currentWeapon == null)
			currentWeapon = GetComponentInChildren<Weapon>();
		if (aimer == null)
			aimer = GetComponentInChildren<EnemyAimer>();
	}

	protected void Update()
	{
		if (aimer.Target != null)
		{
			walkingState = MovingState.STAYING;
			aimer.FollowTarget();
			if (Time.time - lastShootTime >= currentWeapon.FireRate)
			{
				lastShootTime = Time.time;
				currentWeapon.Shoot(new DamageReport(currentWeapon.Damage, this));
			}
		}
	}
	protected new void FixedUpdate()
	{
		base.FixedUpdate();
		if(walkingState == MovingState.STAYING)
		{
			if (aimer.Target == null)
				aimer.Aim();
			else if (!aimer.IsVisible())
				aimer.ResetTarget();
		}
		
	}
}
