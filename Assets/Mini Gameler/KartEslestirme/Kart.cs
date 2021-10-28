using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Kart : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private KartEslestirme kartEslestirme;
    [SerializeField] private SpriteRenderer önResim;
    private void Start()
    {
        kartEslestirme = FindObjectOfType<KartEslestirme>();
    }
    private void OnMouseUpAsButton()
    {
        if (kartEslestirme.kart == null)
        {
            kartEslestirme.kart = this;
            transform.Rotate(0, 180, 0);
        }
        else
        {
            transform.Rotate(0, 180, 0);
            if (!kartEslestirme.KartKontrol(this))
            {
                transform.Rotate(0, 180, 0);
            }
        }
    }
    public Sprite MyResim()
    {
        return önResim.sprite;
    }
}