using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class DefinitelyComeOut : MonoBehaviour
{
    /// Mutlak kazanç : Toplanan para oynayanlardan 4 kişiye %10-%20-%20-%30 şeklinde dağıtılır.
    [Header("Script Atamaları")]
    [SerializeField] private int totalExp = 0;
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private float odulAciklanmaZaman = 4;
    private List<string> allPlayers = new List<string>();
    private List<string> kazananPlayers = new List<string>();
    private float odulAciklanmaZamanNext;

    public void EnterGame(string playerName)
    {
        totalExp += 50;
        allPlayers.Add(playerName);
    }
    private void EndedGame()
    {
        kazananPlayers.Clear();
        rewardText.text = "Lucky Players  ";

        for (int e = 0; e < 4; e++)
        {
            int player = Random.Range(0, allPlayers.Count);
            string playerName = allPlayers[player];
            allPlayers.RemoveAt(player);
            kazananPlayers.Add(playerName);
            rewardText.text += " - " + playerName;
        }
    }
    public void TakeMyReward()
    {

    }
    private void TakeReward(string playerName)
    {
        int kazanmaSirasi = -1;
        for (int e = 0; e < kazananPlayers.Count; e++)
        {
            if (kazananPlayers[e] == playerName)
            {
                kazanmaSirasi = e;
            }
        }
        if (kazanmaSirasi == 0)
        {
            // %10 ödül kazandınız.
        }
        else if (kazanmaSirasi == 1 || kazanmaSirasi == 2)
        {
            // %20 ödül kazandınız.
        }
        else if (kazanmaSirasi == 3)
        {
            // %30 ödül kazandınız.
        }
    }
    private void Update()
    {
        odulAciklanmaZamanNext -= Time.deltaTime;
        if (odulAciklanmaZamanNext < 0)
        {
            EndedGame();
            odulAciklanmaZamanNext = odulAciklanmaZaman;
        }
    }
}