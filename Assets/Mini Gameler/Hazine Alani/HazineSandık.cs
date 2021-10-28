using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HazineSandık : MonoBehaviour
{
    [Header("Script Atamaları")]
    public Animator animator;
    public GameObject parent;
    [SerializeField] private int sandıkOpeningTime = 5;
    private float sandıkOpeningTimeNext;
    private bool sandikAciliyor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Sandık açılıyor
            sandikAciliyor = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Sandık kapaniyor
            sandikAciliyor = false;
        }
    }
    public void AdamaHazıneVer()
    {
        // Adama Hazıne ver
    }
    public void SandıkAcildi()
    {
        Destroy(parent);
    }
    private void Update()
    {
        if (sandikAciliyor)
        {
            sandıkOpeningTimeNext += Time.deltaTime;
            if (sandıkOpeningTimeNext >= sandıkOpeningTime)
            {
                animator.SetTrigger("Open");
            }
        }
        else
        {
            sandıkOpeningTimeNext -= Time.deltaTime;
            if (sandıkOpeningTimeNext < 0)
            {
                sandıkOpeningTimeNext = 0;
            }
        }
    }
}