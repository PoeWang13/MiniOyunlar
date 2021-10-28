using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ResimFarklar : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private float zamanLimit = 30;
    [SerializeField] private int hataSayisi;
    [SerializeField] private Transform resimlerParent;
    [SerializeField] private List<Transform> allHataliResimler = new List<Transform>();
    [SerializeField] private List<Sprite> allHatalar = new List<Sprite>();
    private bool basladi = false;
    private void Start()
    {
        // Resim oluştur
        int resimSirasi = Random.Range(0, allHataliResimler.Count);
        Transform tr = Instantiate(allHataliResimler[resimSirasi], resimlerParent);
        tr.name = "Hatali Resim";
        // Hata sayısı belirle
        hataSayisi = Random.Range(3, tr.GetChild(1).childCount + 1);
        // Fazlalıkları sil
        StartCoroutine(FazlaliklariSil());
    }
    IEnumerator FazlaliklariSil()
    {
        Transform tr = resimlerParent.GetChild(0).GetChild(1);
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
        int sira = 0;
        while (allHatalar.Count != hataSayisi)
        {
            yield return null;
            allHatalar.Add(tr.GetChild(sira).GetComponent<Image>().sprite);
            sira++;
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
    public bool HataFinded(Sprite resim)
    {
        bool hataBulundu = false;
        for (int e = 0; e < allHatalar.Count && !hataBulundu; e++)
        {
            if (resim == allHatalar[e])
            {
                allHatalar.RemoveAt(e);
                hataBulundu = true;
                if (allHatalar.Count == 0)
                {
                    SetWarning("Tebrikler");
                    StartCoroutine(Tebrikler());
                }
            }
        }
        return hataBulundu;
    }
    IEnumerator Tebrikler()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}