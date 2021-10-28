using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Yol : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private YolTakip yolTakip;

    private void Start()
    {
        yolTakip = FindObjectOfType<YolTakip>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("YapBoz"))
        {
            yolTakip.YolaDeymisler(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("YapBoz"))
        {
            yolTakip.YolaDeymisler(false);
        }
    }
}