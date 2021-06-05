using UnityEngine;

public abstract class Entity : MonoBehaviour
{
	protected enum MovingState
	{
		MOVING,
		STAYING
	}

	[SerializeField] protected float speed;
	[SerializeField] protected float maxHp;
	[SerializeField] [ReadOnly] protected float hp;
	protected MovingState walkingState = MovingState.STAYING;

	public float Speed
	{
		get { return speed; }
	}
	public float MaxHp
	{
		get { return maxHp; }
	}
	public float Hp
	{
		get { return hp; }
	}
	protected void Awake()
	{
		hp = maxHp;
	}

	public bool TakeDamage(DamageReport damageReport)
	{
		hp -= damageReport.damage;
		if (hp <= 0)
		{
			Death(damageReport.attacker);
			return true;
		}
		return false;
	}

	/// <summary>
	/// Execute on Entity death
	/// </summary>
	/// <param name="killer">Reference to killer</param>
	protected abstract void Death(Entity killer);
}
