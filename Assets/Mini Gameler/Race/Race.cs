using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Race : MonoBehaviour
{
    // Wall tagına sahip duvar oluşturmayı unutma
    // Girişler hariç üst katman kapalı olacaktır.
    /// 12-İki farklı delik vardır. 1inden birşey bırakırız ve diğerini geçmesini umut ederiz.
    [Header("Script Atamaları")]
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private Vector3 finishPoint;
    [SerializeField] private Racer racer;
    [SerializeField] private List<Vector3> startingPoints = new List<Vector3>();
    private bool justGo;
    private int nextPoint;
    public void NextPozisyon(int next)
    {
        nextPoint += next;
        if (nextPoint == startingPoints.Count)
        {
            nextPoint = 0;
        }
        else if (nextPoint == -1)
        {
            nextPoint = startingPoints.Count - 1;
        }
        racer.transform.position = startingPoints[nextPoint];
    }
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
            if (Vector3.Distance(finishPoint, racer.transform.position) < 0.1f)
            {
                // Odul ver düzgün gitti.
            }
        }
    }
    public void SendMovingBox()
    {
        justGo = true;
        racer.justGo = true;
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