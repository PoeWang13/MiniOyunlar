using TMPro;
using UnityEngine;
using System.Collections;

public class SekenTop : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject warningPanel;
    private bool justGo;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetWarning("Jump Jump");
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
    public void Gool()
    {
        SetWarning("Gooool.");
    }
    IEnumerator Gooool()
    {
        // Adama Odul ver.
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
    }
}