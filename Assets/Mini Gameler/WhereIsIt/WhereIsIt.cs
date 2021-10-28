using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WhereIsIt : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private float zamanLimit = 30;
    private bool basladi = false;
    [SerializeField] private Transform resimParentContainer;
    [SerializeField] private List<Transform> resimParentler = new List<Transform>();
    [SerializeField] private List<string> sorulanObjeSorular = new List<string>();
    [SerializeField] private SorulanObje sorulanObje;
    [SerializeField] private int yanlisSayisi;
    [SerializeField] private int soruNumarasi;
    [SerializeField] private TextMeshProUGUI soruText;
    private void Start()
    {
        Transform resimGenel = Instantiate(resimParentler[Random.Range(0, resimParentler.Count)], resimParentContainer);
        sorulanObje = resimGenel.GetChild(Random.Range(0, resimGenel.childCount)).GetComponent<SorulanObje>();
        sorulanObjeSorular = sorulanObje.sorulanObjeSorular;
        SoruSor(soruNumarasi);
        yanlisSayisi =Random.Range(3, sorulanObjeSorular.Count);
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
    private void GameFinish(string finish)
    {
        SetWarning(finish);
        StartCoroutine(Tebrikler());
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
    public void ObjeKontrol(SorulanObje sorulan)
    {
        if (sorulan == sorulanObje)
        {
            GameFinish("Tebrikler.");
            return;
        }
        yanlisSayisi--;
        if (yanlisSayisi == 0)
        {
            GameFinish("You Lost.");
        }
    }
    private void SoruSor(int soru)
    {
        if (soru >= sorulanObjeSorular.Count)
        {
            soruText.text = "You see all tips.";
            return;
        }
        soruText.text = sorulanObjeSorular[soru];
    }
    public void SoruSor()
    {
        soruNumarasi++;
        SoruSor(soruNumarasi);
    }
}