using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Obje_Sprite
{
    public GameObject gizliObje;
    public Sprite gizliSprite;
}
public class GizliObjeBulma : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private int gizliObjectSayisi = 1;
    [SerializeField] private List<Obje_Sprite> allObjectler = new List<Obje_Sprite>();
    [SerializeField] private List<GameObject> gizliObjectler = new List<GameObject>();
    [SerializeField] private List<Sprite> gizliObjectSpriteler = new List<Sprite>();
    [SerializeField] private int gizliObjectImageSayisi = 1;
    [SerializeField] private Image gizliObjeListElement;
    [SerializeField] private Transform gizliObjeListElementParent;
    [SerializeField] private List<Image> gizliObjectImageler = new List<Image>();
    private void Start()
    {
        for (int e = 0; e < allObjectler.Count; e++)
        {
            allObjectler[e].gizliObje.GetComponent<GizliObje_3D>().mySprite = allObjectler[e].gizliSprite;
        }
        for (int e = 0; e < gizliObjectSayisi; e++)
        {
            int rnd = Random.Range(0, allObjectler.Count);
            allObjectler[rnd].gizliObje.GetComponent<GizliObje_3D>().mySprite = allObjectler[rnd].gizliSprite;
            gizliObjectler.Add(allObjectler[rnd].gizliObje);
            gizliObjectSpriteler.Add(allObjectler[rnd].gizliSprite);
            allObjectler.RemoveAt(rnd);
        }
        gizliObjectImageler.Add(gizliObjeListElement);
        for (int e = 1; e < gizliObjectImageSayisi; e++)
        {
            Image resim = Instantiate(gizliObjeListElement, gizliObjeListElementParent);
            gizliObjectImageler.Add(resim);
            resim.sprite = gizliObjectSpriteler[e];
        }
        gizliObjeListElement.sprite = gizliObjectSpriteler[0];
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
    public bool GizliObjeBulundumu(Sprite mySprite, GameObject obj)
    {
        bool objeBulundu = false;
        int sira = 0;
        for (int e = 0; e < gizliObjectImageler.Count && !objeBulundu; e++)
        {
            if (mySprite == gizliObjectImageler[e].sprite)
            {
                objeBulundu = true;
                sira = e;
            }
        }
        if (objeBulundu)
        {
            bool spriteBulundu = false;
            for (int h = 0; h < gizliObjectSpriteler.Count && !spriteBulundu; h++)
            {
                if (mySprite == gizliObjectSpriteler[h])
                {
                    gizliObjectler.RemoveAt(h);
                    gizliObjectSpriteler.RemoveAt(h);
                    spriteBulundu = true;
                }
            }
            if (gizliObjectSpriteler.Count >= gizliObjectImageler.Count)
            {
                gizliObjectImageler[sira].sprite = gizliObjectSpriteler[gizliObjectImageler.Count - 1];
            }
            else
            {
                gizliObjectImageler[sira].gameObject.SetActive(false);
            }
            OyunKontrol();
        }

        return objeBulundu;
    }
    public void OyunKontrol()
    {
        if (gizliObjectler.Count == 0)
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