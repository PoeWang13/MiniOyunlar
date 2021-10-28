using TMPro;
using UnityEngine;
using System.Collections;

public class MissionStarter : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject warningPanel;

    private string starting;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetWarning("Be careful. Don't show yourself.");
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