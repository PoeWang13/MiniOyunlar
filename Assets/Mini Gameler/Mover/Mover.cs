using TMPro;
using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
    // Wall tagına sahip duvar oluşturmayı unutma
    [Header("Script Atamaları")]
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private Transform movingBox;
    [SerializeField] private Vector3 finishPoint;
    [SerializeField] private MovingBox mover;
    private bool justGo;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetWarning("Just Go");
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
    private void Update()
    {
        if (justGo)
        {
            if (Vector3.Distance(finishPoint, movingBox.position) < 0.1f)
            {
                // Odul ver düzgün gitti.
            }
        }
    }
    public void SendMovingBox()
    {
        justGo = true;
        mover.justGo = true;
    }
    public void Lost()
    {
        SetWarning("You Lost");
        StartCoroutine(Losted());
    }
    IEnumerator Losted()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}