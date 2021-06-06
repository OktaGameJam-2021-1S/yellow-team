using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTurret : Weapon
{

    [SerializeField] private int ammo = 100;
    [SerializeField] private Transform bodyTransform;
    [SerializeField] PlayerAimer aimer;
    [SerializeField] TMPro.TextMeshProUGUI textAmmo;

    Entity player;
    float lastShootTime;

    void Awake()
    {
        pool = new Queue<GameObject>(poolSize);
        ResizePool(poolSize);

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
            if (Time.time - lastShootTime >= FireRate)
            {
                lastShootTime = Time.time;
                Shoot(new DamageReport(Damage, player));
                ammo--;
                textAmmo.text = ammo.ToString();
                if (ammo <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        aimer.Aim();
    }

    public void SetEntity(Entity entity)
    {
        player = entity;
    }
}
