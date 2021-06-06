using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftConstructor : MonoBehaviour
{
    public CraftInventory craftInventory;

    [Header("Cheats - Start values")]
    [SerializeField] private int mines;
    [SerializeField] private int turrets;


    //public static CraftInventory craftInventory;

    private bool preInstantiate;
    private GameObject preInstantiatedGO;
    [Header("Set in editor")]
    [SerializeField] private GameObject defaultGO;
    [SerializeField] private AutoTurret turretPrefab;
    [SerializeField] private GameObject minePrefab;
    [SerializeField] private Player player;

    private void Start()
    {
        craftInventory = new CraftInventory(turrets, mines);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!craftInventory.HaveItemInInvetory()) return;

            preInstantiate = !preInstantiate;
            preInstantiatedGO = defaultGO;
            preInstantiatedGO.SetActive(preInstantiate);
        }

        if (preInstantiate)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && craftInventory.HaveItem(CraftInventory.ItemType.Turret))
            {
                //preInstantiatedGO = turretPrefab;
                preInstantiate = !preInstantiate;
                preInstantiatedGO.SetActive(preInstantiate);
                AutoTurret go = Instantiate(turretPrefab);
                go.SetEntity(player);
                go.transform.position = preInstantiatedGO.transform.position;
                go.gameObject.SetActive(true);

                craftInventory.UseItem(CraftInventory.ItemType.Turret);

            } else if (Input.GetKeyDown(KeyCode.Alpha2) && craftInventory.HaveItem(CraftInventory.ItemType.Mine)) 
            {
                //preInstantiatedGO = minePrefab;
                preInstantiate = !preInstantiate;
                preInstantiatedGO.SetActive(preInstantiate);
                GameObject go = Instantiate(minePrefab);
                go.transform.position = preInstantiatedGO.transform.position;
                go.SetActive(true);

                craftInventory.UseItem(CraftInventory.ItemType.Mine);
            }

        }
    }
}