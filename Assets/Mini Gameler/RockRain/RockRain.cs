using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RockRain : MonoBehaviour
{
    public bool onValid;
    private void OnValidate()
    {
        if (onValid)
        {
            onValid = false;
            RockYagmur();
        }
    }
    [Header("Script Atamaları")]
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject rock;
    [SerializeField] private bool rockTime;
    [SerializeField] private List<GameObject> rocks = new List<GameObject>();
    [SerializeField] private List<int> baslangics = new List<int>();
    [SerializeField] private Vector2Int alanEn;
    [SerializeField] private Vector2Int alanBoy;
    [SerializeField] private int alanYukseklik;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Warning());
            rockTime = true;
            StartCoroutine(RockYagmurTime());
        }
    }
    IEnumerator RockYagmurTime()
    {
        while (rockTime)
        {
            // Yağmur oluştur
            RockYagmur();
            yield return new WaitForSeconds(3.0f);
            // Yağmuru sil
            for (int e = 0; e < rocks.Count; e++)
            {
                Destroy(rocks[e]);
            }
            yield return new WaitForSeconds(1.0f);
        }
    }
    private void RockYagmur()
    {
        baslangics.Clear();
        int baslangic = Random.Range(alanEn.x, alanEn.y);
        baslangics.Add(baslangic);
        int limit = Mathf.Abs(alanBoy.x - alanBoy.y);
        for (int h = 0; h < limit; h++)
        {
            int randomYon = Random.Range(0, 3);
            bool ekleyebilirsin = true;
            if (randomYon == 0)
            {
                baslangic--;
                if (baslangic < alanEn.x)
                {
                    h--;
                    baslangic++;
                    ekleyebilirsin = false;
                }
            }
            else if (randomYon == 1)
            {
                baslangic += 0;
            }
            else if (randomYon == 2)
            {
                baslangic++;
                if (baslangic == alanEn.y)
                {
                    h--;
                    baslangic--;
                    ekleyebilirsin = false;
                }
            }
            if (ekleyebilirsin)
            {
                baslangics.Add(baslangic);
            }
        }
        for (int h = alanBoy.x; h < alanBoy.y; h++)
        {
            for (int e = alanEn.x; e < alanEn.y; e++)
            {
                bool mayinKur = true;
                if (e == baslangics[h])
                {
                    mayinKur = false;
                }
                if (mayinKur)
                {
                    Vector3 mayinPozisyon = new Vector3(h, alanYukseklik, e);
                    rocks.Add(Instantiate(rock, mayinPozisyon, Quaternion.identity));
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rockTime = false;
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
}