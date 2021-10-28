using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Balonlar : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private Rigidbody ok;
    [SerializeField] private float zamanLimit = 30;
    [SerializeField] private Transform oyunParent;
    [SerializeField] private List<Transform> allBalonSistemleri = new List<Transform>();
    [SerializeField] private List<GameObject> allBalonlar = new List<GameObject>();
    private bool basladi = false;
    private Vector3 okOrjPos;
    private void Start()
    {
        okOrjPos = ok.position;
        // BalonSistem oluştur
        int resimSirasi = Random.Range(0, allBalonSistemleri.Count);
        Transform tr = Instantiate(allBalonSistemleri[resimSirasi], oyunParent);
        for (int e = 0; e < tr.childCount; e++)
        {
            allBalonlar.Add(tr.GetChild(e).gameObject);
        }
    }
    private void Update()
    {
        if (basladi)
        {
            zamanLimit -= Time.deltaTime;
            if (zamanLimit == 0)
            {
                SetWarning("You Lost.");
                StartCoroutine(Tebrikler());
            }
        }
    }
    IEnumerator ThrowOk()
    {
        yield return new WaitForSeconds(3.0f);
        ok.transform.eulerAngles = new Vector3(0, 0, 15);
        ok.transform.position = okOrjPos;
        ok.useGravity = false;
        ok.velocity = Vector3.zero;
        ok.gameObject.SetActive(true);
    }
    public void Throw()
    {
        StartCoroutine(ThrowOk());
        ok.useGravity = true;
        ok.AddForce(new Vector3(10, 7, 0), ForceMode.Impulse);
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
    public void BalonPatladi(GameObject balon)
    {
        allBalonlar.Remove(balon);
        if (allBalonlar.Count == 0)
        {
            SetWarning("Tebrikler.");
            StartCoroutine(Tebrikler());
        }
    }
    IEnumerator Tebrikler()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}