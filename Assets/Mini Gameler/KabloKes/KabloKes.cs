using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KabloKes : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private Sprite kabloKesilmisResim;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetWarning("Electric Panel");
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
    public void DogruKabloKesildi(Image resim)
    {
        resim.sprite = kabloKesilmisResim;
        // Odul ver
        StartCoroutine(DestroySystem());
    }
    public void WrongKabloKesildi(Image resim)
    {
        resim.sprite = kabloKesilmisResim;
        SetWarning("Wrong Kablo");
        // Ceza ver
        StartCoroutine(DestroySystem());
    }
    IEnumerator DestroySystem()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}