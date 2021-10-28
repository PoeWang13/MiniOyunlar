using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ToplananToplar : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private Transform oyunParent;
    [SerializeField] private float zamanLimit = 30;
    [SerializeField] private List<Transform> allToplananTopSistemleri = new List<Transform>();
    [SerializeField] private List<ToplananToplar_Top> toplananToplar = new List<ToplananToplar_Top>();
    private bool basladi = false;
    [SerializeField] private Transform duvarlarParent;
    private void Start()
    {
        // Kart Sistem oluştur
        int resimSirasi = Random.Range(0, allToplananTopSistemleri.Count);
        duvarlarParent = Instantiate(allToplananTopSistemleri[resimSirasi], oyunParent);
        for (int e = 0; e < duvarlarParent.GetChild(0).childCount; e++)
        {
            toplananToplar.Add(duvarlarParent.GetChild(0).GetChild(e).GetComponent<ToplananToplar_Top>());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            basladi = true;
            SetWarning("Fix Resim");
        }
    }
    IEnumerator Warning()
    {
        warningPanel.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        warningPanel.SetActive(false);
    }
    public void SetWarning(string s)
    {
        warningText.text = s;
        StartCoroutine(Warning());
    }
    IEnumerator Tebrikler()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
    private void Update()
    {
        if (basladi)
        {
            zamanLimit -= Time.deltaTime;
            if (zamanLimit == 0)
            {
                GameFinish("You Lost.");
            }
        }
    }
    public void GameFinish(string finish)
    {
        SetWarning(finish);
        StartCoroutine(Tebrikler());
    }
    public void KontrolToplar()
    {
        bool hepsiYerinde = true;
        for (int e = 0; e < toplananToplar.Count && hepsiYerinde; e++)
        {
            if (toplananToplar[e].MyParti() == 0)
            {
                if (toplananToplar[e].transform.position.x > duvarlarParent.position.x)
                {
                    hepsiYerinde = false;
                }
            }
        }
        if (hepsiYerinde)
        {
            GameFinish("Tebrikler");
        }
    }
}