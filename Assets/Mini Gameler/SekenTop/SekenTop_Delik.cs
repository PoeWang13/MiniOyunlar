using UnityEngine;

public class SekenTop_Delik : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private SekenTop sekenTop;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sekenTop.Gool();
        }
    }
}