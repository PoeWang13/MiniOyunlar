using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ResimdeGizliObjeBulma : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private Image gizliObjeListElement;
    [SerializeField] private Transform gizliObjeListElementParent;
    [SerializeField] private int gizliResimSayisi = 1;
    [SerializeField] private List<Sprite> allResimler = new List<Sprite>();
    [SerializeField] private List<Sprite> gizliResimler = new List<Sprite>();
    [SerializeField] private int gizliResimImageSayisi = 1;
    [SerializeField] private List<Image> gizliResimImageler = new List<Image>();
    private void Start()
    {
        for (int e = 0; e < gizliResimSayisi; e++)
        {
            int rnd = Random.Range(0, allResimler.Count);
            gizliResimler.Add(allResimler[rnd]);
            allResimler.RemoveAt(rnd);
        }
        gizliResimImageler.Add(gizliObjeListElement);
        for (int e = 1; e < gizliResimImageSayisi; e++)
        {
            Image resim = Instantiate(gizliObjeListElement, gizliObjeListElementParent);
            gizliResimImageler.Add(resim);
            resim.sprite = gizliResimler[e];
        }
        gizliObjeListElement.sprite = gizliResimler[0];
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
    public bool ResimBulundumu(Sprite resim)
    {
        bool resimBulunmadi = false;
        for (int e = 0; e < gizliResimImageler.Count && !resimBulunmadi; e++)
        {
            if (resim == gizliResimImageler[e].sprite)
            {
                gizliResimler.Remove(resim);
                resimBulunmadi = true;
                if (gizliResimler.Count >= gizliResimImageler.Count)
                {
                    gizliResimImageler[e].sprite = gizliResimler[gizliResimImageler.Count - 1];
                }
                else
                {
                    gizliResimImageler[e].gameObject.SetActive(false);
                }
                OyunKontrol();
            }
        }
        return resimBulunmadi;
    }
    public void OyunKontrol()
    {
        if (gizliResimler.Count == 0)
        {
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