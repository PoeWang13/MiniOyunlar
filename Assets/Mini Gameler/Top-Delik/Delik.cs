using UnityEngine;

public class Delik : MonoBehaviour
{
    [Header("Script Atamaları")]
    public Top_Delik top_Delik;
    public int delikPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            top_Delik.SetPoint(delikPoint);
        }
    }
}