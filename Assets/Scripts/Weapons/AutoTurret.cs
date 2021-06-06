using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTurret : Entity
{

    [SerializeField] private int ammo = 100;
    [SerializeField] private Transform bodyTransform;
    [SerializeField] private Weapon turret;
    [SerializeField] PlayerAimer aimer;
    [SerializeField] TMPro.TextMeshProUGUI textAmmo;

    float lastShootTime;

    new void Awake()
    {
        base.Awake();

        if (aimer == null)
        {
            aimer = GetComponentInChildren<PlayerAimer>();
        }

        textAmmo.text = ammo.ToString();
        aimer.SetOwner(bodyTransform.gameObject);
    }

    private void Update()
    {
        if (aimer.Target != null)
        {
            aimer.FollowTarget();
            if (Time.time - lastShootTime >= turret.FireRate)
            {
                lastShootTime = Time.time;
                turret.Shoot(new DamageReport(turret.Damage, this));
                ammo--;
                textAmmo.text = ammo.ToString();
                if (ammo <= 0)
                {
                    Death(this);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        aimer.Aim();
    }

    protected override void Death(Entity killer)
    {
        gameObject.SetActive(false);
    }
}
