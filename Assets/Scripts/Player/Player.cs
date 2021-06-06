using UnityEngine;
using ThirteenPixels.Soda;
using System.Collections;

public class Player : Entity
{
    [SerializeField] GameEvent onPlayerDeath;
    [SerializeField] GlobalVector2 input;
    [SerializeField] PlayerAimer aimer;
    [SerializeField] Animator animator;
    [SerializeField] [ReadOnly] long coins = 0;

    [Header("Weapon Configs")]
    [SerializeField] float meleeDamage;
    [SerializeField] float meleeFireRate;
    [SerializeField] float meleeDistance;

    [SerializeField] Weapon[] weapons;
    [SerializeField] Transform[] weaponsPositions;
    
    [SerializeField] GameObject meleeTriggerBox;

    int currentWeaponIndex = 0;
    Weapon currentWeapon;
    float lastShootTime;
    float lastMeleeTime;

    private void OnEnable()
    {
        input.onChange.AddResponse(CheckMovementState);
    }

    private void OnDisable()
    {
        input.onChange.RemoveResponse(CheckMovementState);
    }

    protected new void Awake()
    {
        currentWeapon = weapons[currentWeaponIndex];
        currentWeapon.gameObject.SetActive(true);

        base.Awake();
        if (currentWeapon == null)
            currentWeapon = GetComponentInChildren<Weapon>();
        if (aimer == null)
            aimer = GetComponentInChildren<PlayerAimer>();
    }

    protected override void Death(Entity killer)
    {
        onPlayerDeath.Raise();
        Debug.Log("Player is DEAD");
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Adds coins on Enemy death
    /// </summary>
    /// <param name="amount"></param>
    public void AddCoins(int amount)
    {
        coins += amount;
    }

    /// <summary>
    /// Called when Input Vector2 is changed
    /// </summary>
    /// <param name="direction"></param>
    private void CheckMovementState(Vector2 direction)
    {
        if (walkingState == MovingState.STAYING && direction != Vector2.zero)
        {
            walkingState = MovingState.MOVING;
            animator.SetBool("Walking", true);
        }
        else
        {
            if (walkingState == MovingState.MOVING && direction == Vector2.zero)
            {
                animator.SetBool("Walking", false);
                walkingState = MovingState.STAYING;
            }
        }
    }

    private void Update()
    {
        if (walkingState == MovingState.STAYING)
        {
            if (aimer.Target != null)
            {
                aimer.FollowTarget();
                if (Time.time - lastShootTime >= currentWeapon.FireRate)
                {
                    lastShootTime = Time.time;
                    animator.SetTrigger("Attack");
                    currentWeapon.Shoot(new DamageReport(currentWeapon.Damage, this));
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && aimer.Target != null)
        {
            if (Time.time - lastMeleeTime >= meleeFireRate)
            {
                lastMeleeTime = Time.time;
                animator.SetTrigger("Melee");

                if (Vector3.Distance(transform.position, aimer.Target.position) <= meleeDistance)
                {
                    aimer.FollowTarget();
                    aimer.Target.gameObject.SendMessage("TakeDamage", new DamageReport { damage = meleeDamage, attacker = this });
                }

                meleeTriggerBox.SetActive(true);
                StartCoroutine(MeleeEndAnimation());
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            ChangeWeapon();
        }

        if (currentWeapon != null)
        {
            currentWeapon.UpdateWeaponPosition(weaponsPositions[(int)currentWeapon.Position].transform.position);
        }
    }

    IEnumerator MeleeEndAnimation()
    {
        yield return new WaitForSeconds(0.458f);
        meleeTriggerBox.SetActive(false);
    }

    public void ChangeWeapon()
    {
        currentWeaponIndex++;
        if (currentWeaponIndex >= weapons.Length)
        {
            currentWeaponIndex = 0;
        }

        animator.SetTrigger("Change");

        currentWeapon.gameObject.SetActive(false);
        currentWeapon = weapons[currentWeaponIndex];
        currentWeapon.gameObject.SetActive(true);
    }

    private void FixedUpdate()
    {
        //if (aimer.Target == null)
        //    aimer.Aim();
        //else if (!aimer.IsVisible())
        //    aimer.ResetTarget();

        aimer.Aim();
    }
}
