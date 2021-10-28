using TMPro;
using UnityEngine;

public class RiskRoom : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private Risk risk;
    [SerializeField] private int myRisk;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            risk.RoomSectim(myRisk);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            risk.RoomIptal();
        }
    }
}