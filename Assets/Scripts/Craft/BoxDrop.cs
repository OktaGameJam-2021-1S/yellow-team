using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDrop : MonoBehaviour
{
    [Range(0, 100)] [SerializeField] protected int droprateCraft;
    [SerializeField] private GameObject craftPickupPrefab;


    // how to target only on melee? 
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);

        // Destroy only on trigger by melee box collider
        if(other.tag == Tags.playerTag && other.GetComponent<PlayerMeleeData>() != null)
        {
            other.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Random.Range(1, 101) <= droprateCraft)
        {
            Instantiate(craftPickupPrefab, gameObject.transform.position, Quaternion.identity);
        }
    }

}
