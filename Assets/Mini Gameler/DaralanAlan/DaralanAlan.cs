using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DaralanAlan : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private Transform oyunParent;

    [SerializeField] private float zamanLimit = 30;
    [SerializeField] private List<Transform> allDaralanAlanSistemleri = new List<Transform>();
    private bool basladi = false;
    [SerializeField] private Transform duvarlarParent;
    private void Start()
    {
        // Kart Sistem oluştur
        int resimSirasi = Random.Range(0, allDaralanAlanSistemleri.Count);
        duvarlarParent = Instantiate(allDaralanAlanSistemleri[resimSirasi], oyunParent);
        //0 alt duvar - 3 sag duvar
        float yanKenar = duvarlarParent.GetChild(3).localScale.y;
        float altKenar = duvarlarParent.GetChild(0).localScale.x;
        yukseklikKucuk = duvarlarParent.GetChild(3).localPosition.y - yanKenar * 0.5f;
        yukseklikBuyuk = duvarlarParent.GetChild(3).localPosition.y + yanKenar * 0.5f;
        genislikKucuk = duvarlarParent.GetChild(0).localPosition.x - altKenar * 0.5f;
        genislikBuyuk = duvarlarParent.GetChild(0).localPosition.x + altKenar * 0.5f;
        orjAlan = yanKenar * altKenar;
        extraDuvarPoint = duvarlarParent.GetChild(4).position;
        YeniDuvarGoster();
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
    [SerializeField] private float yukseklikKucuk;
    [SerializeField] private float yukseklikBuyuk;
    [SerializeField] private float genislikKucuk;
    [SerializeField] private float genislikBuyuk;
    [SerializeField] private Vector3 extraDuvarPoint;
    [SerializeField] private Transform top;
    [SerializeField] private Transform daralan1;
    [SerializeField] private Transform daralan2;
    [SerializeField] private float orjAlan;
    public void NoktaGonder(bool isX, Vector3 nokta)
    {
        // Duvar yatayda hareket etti
        if (isX)
        {
            if (top.position.y > nokta.y)
            {
                // Top Yukarida
                yukseklikKucuk = nokta.y;
            }
            else
            {
                yukseklikBuyuk = nokta.y;
            }
        }
        else
        {
            if (top.position.x > nokta.x)
            {
                // Top Sagda
                genislikKucuk = nokta.x;
            }
            else
            {
                genislikBuyuk = nokta.x;
            }
        }
        AlanHesapla();
    }
    private void AlanHesapla()
    {
        float yanKenar = yukseklikBuyuk - yukseklikKucuk;
        float altKenar = genislikBuyuk - genislikKucuk;
        float alan = yanKenar * altKenar;
        if (orjAlan / alan > 4)
        {
            GameFinish("Tebrikler.");
        }
    }
    public void YeniDuvarGoster()
    {
        int rnd = Random.Range(0, 2);
        Transform duvar = Instantiate((rnd == 0) ? daralan1 : daralan2, extraDuvarPoint, Quaternion.identity);
        DuvarlaraEtkisizlestir(duvar.GetComponent<BoxCollider2D>());
        duvar.SetParent(oyunParent);
    }
    public void DuvarlaraEtkisizlestir(BoxCollider2D box)
    {
        Physics2D.IgnoreCollision(box, duvarlarParent.GetChild(0).GetComponent<BoxCollider2D>());
        Physics2D.IgnoreCollision(box, duvarlarParent.GetChild(1).GetComponent<BoxCollider2D>());
        Physics2D.IgnoreCollision(box, duvarlarParent.GetChild(2).GetComponent<BoxCollider2D>());
        Physics2D.IgnoreCollision(box, duvarlarParent.GetChild(3).GetComponent<BoxCollider2D>());
        Physics2D.IgnoreCollision(box, top.GetComponent<CircleCollider2D>());
    }
    public bool DuvarBirakilabilirmi(Vector3 pos)
    {
        if (pos.x > genislikKucuk)
        {
            if (genislikBuyuk > pos.x)
            {
                if (pos.y > yukseklikKucuk)
                {
                    if (yukseklikBuyuk > pos.y)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }
}