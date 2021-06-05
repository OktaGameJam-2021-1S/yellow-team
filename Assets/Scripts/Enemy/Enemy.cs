using UnityEngine;
using ThirteenPixels.Soda;
public class Enemy : Entity
{
    [SerializeField] protected float damage = 10;
    [SerializeField] protected GlobalTransform player;
    [SerializeField] protected GlobalEnemyHandler enemyHandler;
    [SerializeField] protected GameEvent onPlayerDeath;
    [SerializeField] protected float movingTime;  // Enemy movement time
    [SerializeField] protected float waitingTime; // Enemy idle time
    [SerializeField] protected float randomTime;  // A random time from 0 to the value will be added to the movement time
    [SerializeField] protected float touchDamageMultiplier = 1; // Base damage modifier on contact
    [SerializeField] protected int coinsToDrop = 100; // How many coins will fall for a kill
    [Tooltip("Hoy much percertto drop a craft item")]
    [Range(0,100)] [SerializeField] protected int droprateCraft;
    [SerializeField] protected GameObject craftPickupPrefab;
    //protected Player touchingPlayer; // Needed to determine if the player is still in the affected area after the touch attack cooldown
    protected Entity touchingPlayer; // Needed to determine if the player is still in the affected area after the touch attack cooldown

    [SerializeField] protected Transform target;

    protected void OnEnable()
    {
        onPlayerDeath.onRaise.AddResponse(ResetTouchingPlayer);
    }
    protected void OnDisable()
    {
        onPlayerDeath.onRaise.RemoveResponse(ResetTouchingPlayer);
    }

    public void SetTarget(bool pIsPlayer, Transform pTarget= null)
    {
        if (pIsPlayer)
        {
            target = player.componentCache;
        }
        else
        {
            target = pTarget;
        }
    }

    private void ResetTouchingPlayer()
    {
        touchingPlayer = null;
    }

    protected override void Death(Entity killer)
    {
        Player player = killer as Player;
        if(player!=null)
            player.AddCoins(coinsToDrop);
        enemyHandler.componentCache.RemoveEnemy(this);

        if (Random.Range(1, 101) <= droprateCraft)
        {
            Instantiate(craftPickupPrefab, gameObject.transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    protected void OnTriggerEnter(Collider other)
    {
        //if (player != null)
        if (other.tag == "Player" || other.tag == "Castle")
        {
            var player = other.GetComponent<Entity>();
            touchingPlayer = player;
            player.TakeDamage(new DamageReport(damage * touchDamageMultiplier, this));
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.tag == Tags.playerTag)
            touchingPlayer = null;
    }
}
