using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private TeleportTarla teleportTarla;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            teleportTarla.SetWarning("Be careful yor step.");
        }
    }
}