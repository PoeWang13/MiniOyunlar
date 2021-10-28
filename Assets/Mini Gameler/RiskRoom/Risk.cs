using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Risk : MonoBehaviour
{
    private void OnValidate()
    {
        if (odaGameObject.Count != odaPozisyon.Count)
        {
            odaPozisyon.Clear();
            for (int e = 0; e < odaGameObject.Count; e++)
            {
                odaPozisyon.Add(Vector3.zero);
            }
        }
    }
    [Header("Script Atamaları")]
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private Button chooseRewardButton;
    [SerializeField] private List<Transform> odaGameObject = new List<Transform>();
    [SerializeField] private List<Vector3> odaPozisyon = new List<Vector3>();

    private void Start()
    {
        List<int> odaSira = new List<int>();
        for (int e = 0; e < odaGameObject.Count; e++)
        {
            odaSira.Add(e);
        }
        for (int e = 0; e < 50; e++)
        {
            int random1 = Random.Range(0, odaSira.Count);
            int random2 = Random.Range(0, odaSira.Count);

            int sayi = odaSira[random1];
            odaSira[random1] = odaSira[random2];
            odaSira[random2] = sayi;
        }
        for (int e = 0; e < odaGameObject.Count; e++)
        {
            odaGameObject[e].position = odaPozisyon[odaSira[e]];
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetWarning("Your choice your decision.");
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
    public void RoomIptal()
    {
        chooseRewardButton.gameObject.SetActive(false);
    }
    public void RoomSectim(int room)
    {
        chooseRewardButton.gameObject.SetActive(true);
        chooseRewardButton.onClick.RemoveAllListeners();
        if (room == 0) // Reward
        {
            chooseRewardButton.onClick.AddListener(() => RewardVer());
        }
        else if (room == 1) // Punish
        {
            chooseRewardButton.onClick.AddListener(() => PunishVer());
        }
        else if (room == 2) // Empty
        {
            chooseRewardButton.onClick.AddListener(() => EmptyVer());
        }
    }
    private void RewardVer()
    {

    }
    private void PunishVer()
    {

    }
    private void EmptyVer()
    {

    }
}