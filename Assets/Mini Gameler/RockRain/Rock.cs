using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Rock : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Yüzde can götür.
            Destroy(gameObject);
        }
    }
}