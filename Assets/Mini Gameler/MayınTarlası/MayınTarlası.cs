using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MayınTarlası : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private GameObject mayin;
    [SerializeField] private Vector2Int alanEn;
    [SerializeField] private Vector2Int alanBoy;
    [SerializeField] private List<GameObject> mayins = new List<GameObject>();
    [SerializeField] private List<int> baslangics = new List<int>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetWarning("Welcome to Mine Land");
            MayınTarlasiKur();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int e = 0; e < mayins.Count; e++)
            {
                Destroy(mayins[e]);
            }
        }
    }
    private void MayınTarlasiKur()
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
                    Vector3 mayinPozisyon = new Vector3(h, 0, e);
                    mayins.Add(Instantiate(mayin, mayinPozisyon, Quaternion.identity));
                }
            }
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