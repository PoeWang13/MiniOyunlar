using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class KartEslestirme : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private Transform oyunParent;

    [SerializeField] private float zamanLimit = 30;
    [SerializeField] private List<Transform> allKartSistemleri = new List<Transform>();
    private bool basladi = false;
    private void Start()
    {
        // Kart Sistem oluştur
        int resimSirasi = Random.Range(0, allKartSistemleri.Count);
        Transform tr = Instantiate(allKartSistemleri[resimSirasi], oyunParent);
        for (int e = 0; e < tr.childCount; e++)
        {
            allKartlar.Add(tr.GetChild(e).GetComponent<Kart>());
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
                SetWarning("You Lost.");
                StartCoroutine(Tebrikler());
            }
        }
    }

    [SerializeField] private List<Kart> allKartlar = new List<Kart>();
    public Kart kart;
    public bool KartKontrol(Kart resimKart)
    {
        if (resimKart.MyResim() == kart.MyResim())
        {
            allKartlar.Remove(kart);
            allKartlar.Remove(resimKart);
            // Kart doğru eşleşti
            if (allKartlar.Count == 0)
            {
                SetWarning("Tebrikler.");
                StartCoroutine(Tebrikler());
            }
            return true;
        }
        else
        {
            // Kart yanlış eşleşti
            kart.transform.Rotate(0, 180, 0);
            return false;
        }
    }
}