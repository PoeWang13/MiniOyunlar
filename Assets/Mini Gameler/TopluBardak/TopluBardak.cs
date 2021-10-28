using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TopluBardak : MonoBehaviour
{
    public bool onValide;
    private void OnValidate()
    {
        if (onValide)
        {
            onValide = false;
            posizyonlar.Clear();
            for (int e = 0; e < bardaklar.Count; e++)
            {
                posizyonlar.Add(Vector3.zero);
            }
        }
    }
    [Header("Script Atamaları")]
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private GameObject startBardakButton;
    [SerializeField] private Transform top;
    [SerializeField] private List<Bardak> bardaklar = new List<Bardak>();
    [SerializeField] private List<Vector3> posizyonlar = new List<Vector3>();
    public void StartBardak()
    {
        int yer = Random.Range(0, bardaklar.Count);
        for (int e = 0; e < bardaklar.Count; e++)
        {
            bardaklar[e].SetPozisyon(posizyonlar[e]);
        }
        StartCoroutine(ShowTop(yer));
        startBardakButton.SetActive(false);
    }
    IEnumerator ShowTop(int yer)
    {
        top.position = posizyonlar[yer];
        bardaklar[yer].hasTop = true;
        bardaklar[yer].gameObject.SetActive(false);
        yield return new WaitForSeconds(2.0f);
        bardaklar[yer].gameObject.SetActive(true);
        StartMove();
    }
    private void StartMove()
    {
        for (int e = 0; e < bardaklar.Count; e++)
        {
            bardaklar[e].canMove = true;
        }
        StartCoroutine(StopBall());
    }
    IEnumerator StopBall()
    {
        yield return new WaitForSeconds(30.0f);
        for (int e = 0; e < bardaklar.Count; e++)
        {
            bardaklar[e].canStop = true;
        }
    }
    public void AddPozisyon(Vector3 posizyon)
    {
        posizyonlar.Add(posizyon);
    }
    public Vector3 WantPozisyon()
    {
        int yer = Random.Range(0, posizyonlar.Count);
        Vector3 posizyon = posizyonlar[yer];
        posizyonlar.RemoveAt(yer);
        return posizyon;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetWarning("Welcome to Glass");
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
    public void OdulVer()
    {
    }
}