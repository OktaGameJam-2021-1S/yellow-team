using System.Collections.Generic;
using UnityEngine;
using ThirteenPixels.Soda;

public class BlockMap : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] List<GameObject> gateEntry;
    [SerializeField] List<GameObject> gateExit;
    [SerializeField] Transform endMapPoint;
    [SerializeField] Transform truckTarget;

    System.Action onPlayerEntry;

    public List<Transform> SpawnPoints { get => spawnPoints; }
    public Transform EndMapPoint { get => endMapPoint; }
    public Transform TruckTarget { get => truckTarget; }

    /// <summary>
    /// Opens the gate
    /// </summary>
    public void OpenExitGate()
    {
        foreach (var block in gateExit)
        {
            block.SetActive(false);
        }
    }

    /// <summary>
    /// Opens the gate
    /// </summary>
    public void OpenEntryGate()
    {
        foreach (var block in gateEntry)
        {
            block.SetActive(false);
        }
    }

    /// <summary>
    /// Opens the gate
    /// </summary>
    public void CloseEntryGate()
    {
        foreach (var block in gateEntry)
        {
            block.SetActive(true);
        }
    }

    public void SetActionPlayerEntry(System.Action action)
    {
        onPlayerEntry = action;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            onPlayerEntry?.Invoke();
            onPlayerEntry = null;
        }
    }
}
