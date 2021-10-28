using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParcalanmisResimTiklatma : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private Transform resimParent;
    [SerializeField] private ResimParcasi_Tiklama resimParcasi;
    [SerializeField] private int resimParcaSayisiX;
    [SerializeField] private int resimParcaSayisiY;
    [SerializeField] private List<Texture> resimTextures = new List<Texture>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int e = 0; e < resimParcaSayisiX; e++)
            {
                for (int h = 0; h < resimParcaSayisiY; h++)
                {
                    ResimParcasi_Tiklama tr = Instantiate(resimParcasi, resimParent);
                    Vector2 scale = new Vector2(1.0f / resimParcaSayisiX, 1.0f / resimParcaSayisiY);
                    Vector2 offset = new Vector2(e * (1.0f / resimParcaSayisiX), h * (1.0f / resimParcaSayisiY));
                    tr.ResimParcaAyarla(resimTextures[Random.Range(0, resimTextures.Count)], offset, scale);
                    int random = Random.Range(0, 10);
                    for (int c = 0; c < random; c++)
                    {
                        tr.transform.Rotate(0, 0, 90);
                    }
                }
            }
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
        for (int e = 0; e < resimParent.childCount && resimYanlis; e++)
        {
            if (resimParent.GetChild(e).rotation.z != 0)
            {
                resimYanlis = false;
            }
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
        Destroy(gameObject);
    }
}