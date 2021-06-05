using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftData : MonoBehaviour
{

    [SerializeField] CraftInventory.ItemType ItemType;

    private void Start()
    {
        ItemType = Random.Range(0, 100) % 2 == 0 ? CraftInventory.ItemType.Turret : CraftInventory.ItemType.Mine;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<CraftConstructor>().craftInventory.AddItem(ItemType);
            Destroy(gameObject);
        }
    }

}
