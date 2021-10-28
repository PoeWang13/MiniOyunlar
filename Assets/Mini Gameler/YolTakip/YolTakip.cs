using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class YolTakip : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private int deyilenYol = 1;
    [SerializeField] private float zamanLimit = 30;
    [SerializeField] private Vector3 odaYeri;
    [SerializeField] private Transform odaParent;
    [SerializeField] private List<Transform> allHataliOdalar = new List<Transform>();
    private bool basladi = false;
    private void Start()
    {
        // Resim oluştur
        int resimSirasi = Random.Range(0, allHataliOdalar.Count);
        Instantiate(allHataliOdalar[resimSirasi], odaYeri, Quaternion.identity, odaParent);
    }
    private void Update()
    {
        if (basladi)
        {
            zamanLimit -= Time.deltaTime * deyilenYol;
            if (zamanLimit == 0)
            {
                SetWarning("You Lost.");
                StartCoroutine(Tebrikler());
            }
        }
    }
    public void YolaDeymisler(bool deydilermi)
    {
        if (deydilermi)
        {
            deyilenYol++;
        }
        else
        {
            deyilenYol--;
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
    IEnumerator Tebrikler()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}