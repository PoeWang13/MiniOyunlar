using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Lottery : MonoBehaviour
{
    public bool onValid;
    private void OnValidate()
    {
        if (onValid)
        {
            onValid = false;
            allNumbers.Clear();
            allNumberButtons.Clear();
            for (int e = 0; e < 49; e++)
            {
                allNumbers.Add(e);
                allNumberButtons.Add(buttonsParent.GetChild(e).GetComponent<Button>());
                buttonsParent.GetChild(e).GetChild(0).GetComponentInChildren<TextMeshProUGUI>().text = (e + 1).ToString();
            }
        }
    }
    [Header("Script Atamaları")]
    [SerializeField] private int totalExp = 0;
    [SerializeField] private Transform buttonsParent;
    [SerializeField] private TextMeshProUGUI rewardText;
    private int totalChoose = 0;
    private List<Button> allNumberButtons = new List<Button>();
    private List<int> allNumbers = new List<int>();
    private List<int> allChoosedNumbers = new List<int>();
    private List<int> allRewardNumbers = new List<int>();
    private void Start()
    {
        ResetButtons();
        for (int e = 0; e < allNumberButtons.Count; e++)
        {
            int sayi = e;
            allNumberButtons[e].onClick.AddListener(()=> 
            {
                allNumberButtons[e].interactable = false;
                Choosed(sayi);
            });
        }
    }
    public void TryReward()
    {
        rewardText.text = "Lucky Numbers  ";
        for (int e = 0; e < 6; e++)
        {
            int sayi = Random.Range(0, 49);
            rewardText.text += " - " + sayi.ToString();
            allNumbers.Remove(sayi);
            allRewardNumbers.Add(sayi);
        }
        for (int e = 0; e < 6; e++)
        {
            allNumbers.Add(allRewardNumbers[e]);
        }
        allRewardNumbers.Sort();
        bool kazandim = true;
        for (int e = 0; e < 6 && kazandim; e++)
        {
            if (allRewardNumbers[e] != allNumbers[e])
            {
                kazandim = false;
            }
        }
        if (kazandim)
        {
            totalExp = 0;
            // Ödülü kazandın
        }
        else
        {
            // Ödülü kazanamadin
            ResetButtons();
        }
    }
    private void Choosed(int rakam)
    {
        if (totalChoose == 6)
        {
            // Yeteri kadar çekiliş yaptınız
            return;
        }
        totalExp += 50;
        totalChoose++;
        allChoosedNumbers.Add(rakam);
        allChoosedNumbers.Sort();
    }
    private void ResetButtons()
    {
        allChoosedNumbers.Clear();
        allRewardNumbers.Clear();
        totalChoose = 0;
        for (int e = 0; e < allNumberButtons.Count; e++)
        {
            allNumberButtons[e].interactable = true;
        }
    }
}