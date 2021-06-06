using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image bar;
    [SerializeField] Entity entity;
    [SerializeField] Transform targetPosition;
    Transform target;

    void Start()
    {
        target = Camera.main.transform;
        transform.SetParent(null);
    }
    private void Update()
    {
        bar.fillAmount = Mathf.Lerp(bar.fillAmount, entity.Hp / entity.MaxHp, 0.2f);

        if (entity.Hp <= 0)
        {
            Destroy(gameObject);
        }

        if (targetPosition != null)
        {
            transform.position = targetPosition.position;
        }

        transform.LookAt(target);
    }
}
