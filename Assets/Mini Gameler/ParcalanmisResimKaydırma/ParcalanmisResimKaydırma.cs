using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParcalanmisResimKaydırma : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private Transform resimParent;
    [SerializeField] private ParcalanmisResim_Kaydırma resimParcasi;
    [SerializeField] private int resimParcaSayisiX;
    [SerializeField] private int resimParcaSayisiY;
    [SerializeField] private List<Texture> resimTextures = new List<Texture>();
    [SerializeField] private List<ParcalanmisResim_Kaydırma> resimParcalari = new List<ParcalanmisResim_Kaydırma>();
    [SerializeField] private ParcalanmisResim_Kaydırma seciliResimParcasi;

    private void Start()
    {
        for (int e = 0; e < resimParcaSayisiX; e++)
        {
            for (int h = 0; h < resimParcaSayisiY; h++)
            {
                ParcalanmisResim_Kaydırma tr = Instantiate(resimParcasi, resimParent);
                Vector2 scale = new Vector2(1.0f / resimParcaSayisiX, 1.0f / resimParcaSayisiY);
                Vector2 offset = new Vector2(e * (1.0f / resimParcaSayisiX), h * (1.0f / resimParcaSayisiY));
                tr.ResimParcaAyarla(resimTextures[Random.Range(0, resimTextures.Count)], offset, scale);
                resimParcalari.Add(tr);
            }
        }
        // Parcaların yerlerini değiştir
        for (int e = 0; e < 50; e++)
        {
            SetResimParcasi(resimParcalari[Random.Range(0, resimParcalari.Count)]);
            ResimleriDegistir(resimParcalari[Random.Range(0, resimParcalari.Count)]);
        }
        resimParcasi.gameObject.SetActive(false);
    }
    public void SetResimParcasi(ParcalanmisResim_Kaydırma seciliResim)
    {
        seciliResimParcasi = seciliResim;
    }
    public ParcalanmisResim_Kaydırma GetResimParcasi()
    {
        return seciliResimParcasi;
    }
    public void ResimYerDegistir(ParcalanmisResim_Kaydırma seciliResim)
    {
        ResimleriDegistir(seciliResim);
        ResimKontrol();
    }
    private void ResimleriDegistir(ParcalanmisResim_Kaydırma seciliResim)
    {
        // Ikı resmin yerini değiştir
        seciliResimParcasi.MyColor(Color.white);
        Vector2 resimYeri = seciliResimParcasi.MyYerim();
        seciliResimParcasi.ResimParcaDegis(seciliResim.MyYerim());
        seciliResim.ResimParcaDegis(resimYeri);
        SetResimParcasi(null);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int e = 0; e < resimParcaSayisiX; e++)
            {
                for (int h = 0; h < resimParcaSayisiY; h++)
                {
                    ParcalanmisResim_Kaydırma tr = Instantiate(resimParcasi, resimParent);
                    Vector2 scale = new Vector2(1.0f / resimParcaSayisiX, 1.0f / resimParcaSayisiY);
                    Vector2 offset = new Vector2(e * (1.0f / resimParcaSayisiX), h * (1.0f / resimParcaSayisiY));
                    tr.ResimParcaAyarla(resimTextures[Random.Range(0, resimTextures.Count)], offset, scale);
                }
            }
            // Parcaların yerlerini değiştir
            resimParcasi.gameObject.SetActive(false);
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
    public void ResimKontrol()
    {
        bool resimYanlis = true;
        for (int e = 0; e < resimParcalari.Count && resimYanlis; e++)
        {
            resimYanlis = resimParcalari[e].YerimDogrumu();
        }
        if (resimYanlis)
        {
            // Odul ver.
            SetWarning("Tebrikler");
            StartCoroutine(Tebrikler());
        }
    }
    IEnumerator Tebrikler()
    {
        yield return new WaitForSeconds(3.0f);
        //Destroy(gameObject);
    }
}