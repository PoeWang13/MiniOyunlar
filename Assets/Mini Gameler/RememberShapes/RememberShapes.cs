using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class RememberShapes : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private float zamanLimit = 30;
    private bool basladi = false;
    [SerializeField] private List<GameObject> oyunAlani = new List<GameObject>();
    [SerializeField] private GameObject kontrolculer;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Image resim;
    [SerializeField] private Transform resimlerParent;
    [SerializeField] private int sorulacakResim = 0;
    [SerializeField] private int sorulanResimSayisi = 0;
    private void Start()
    {
        StartCoroutine(ResimOlustur());
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
    IEnumerator ResimOlustur()
    {
        int resimSayisi = 0;
        sorulacakResim = Random.Range(0, oyunAlani.Count);
        while (resimSayisi < 30)
        {
            resimSayisi++;
            yield return new WaitForSeconds(1.0f);
            int hangisi = Random.Range(0, oyunAlani.Count);
            if (hangisi == sorulacakResim)
            {
                sorulanResimSayisi++;
            }
            float posX = Random.Range(-8, 8);
            float posY = Random.Range(-4, 4);
            Instantiate(oyunAlani[hangisi], new Vector3(posX, posY, 0), Quaternion.identity, resimlerParent);
        }
        SetWarning("How many times do you see this shape.");
        resim.sprite = oyunAlani[sorulacakResim].GetComponent<SpriteRenderer>().sprite;
        kontrolculer.SetActive(true);
    }
    public void OyunKontrol()
    {
        bool oyunBitti = true;
        if (int.TryParse(inputField.text, out int deger))
        {
            if (deger != sorulanResimSayisi)
            {
                oyunBitti = false;
            }
        }
        if (oyunBitti)
        {
            GameFinish("Tebrikler.");
        }
        else
        {
            GameFinish("You Lost.");
        }
        StartCoroutine(Tebrikler());
    }
}