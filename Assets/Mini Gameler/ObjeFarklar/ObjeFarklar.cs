using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ObjeFarklar : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private Transform odaParent;
    [SerializeField] private Vector3 odaYeri;
    [SerializeField] private float zamanLimit = 30;
    [SerializeField] private int hataSayisi;
    [SerializeField] private List<Transform> allHataliOdalar = new List<Transform>();
    private bool basladi = false;
    private void Start()
    {
        // Resim oluştur
        int resimSirasi = Random.Range(0, allHataliOdalar.Count);
        Transform tr = Instantiate(allHataliOdalar[resimSirasi], odaYeri, Quaternion.identity, odaParent);
        tr.name = "Hatalı Oda";
        // Hata sayısını belirle
        // 0.Obje Normal Odadır - 1.Obje Hatalı Odadır - 2 Obje hataları tutar
        hataSayisi = Random.Range(3, tr.GetChild(2).childCount + 1);
        // Fazlalıkları sil
        StartCoroutine(FazlaliklariSil());
    }
    IEnumerator FazlaliklariSil()
    {
        Transform tr = odaParent.GetChild(0).GetChild(2);
        bool silindiler = tr.childCount == hataSayisi;
        while (!silindiler)
        {
            yield return null;
            int resimSirasi2 = Random.Range(0, tr.childCount);
            Destroy(tr.GetChild(resimSirasi2).gameObject);
            if (tr.childCount - 1 == hataSayisi)
            {
                silindiler = true;
            }
        }
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
    public void HataFinded()
    {
        hataSayisi--;
        if (hataSayisi == 0)
        {
            SetWarning("Tebrikler");
            StartCoroutine(Tebrikler());
        }
    }
}