using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class LuckCardItems
{
    public string items;
    public int itemFounded;
    public int itemRewardLimit;
    public List<string> luckCardItems = new List<string>();
}
public class LuckCard : MonoBehaviour
{
    public bool onValid;
    private void OnValidate()
    {
        if (onValid)
        {
            onValid = false;
            allItemsAmount.Clear();
            allNumberButtons.Clear();
            for (int e = 0; e < 25; e++)
            {
                allItemsAmount.Add(e);
                allNumberButtons.Add(buttonsParent.GetChild(e).GetComponent<Button>());
            }
        }
    }
    /// Şans Kartı	  : Kartın üzerinde 25 bölüm vardır. 5 tane seçersin ve ne çıktıysa artık. Çıkacak itemler
    ///                 5-4-4-3-3-3-3 adet şeklinde parçalanmıştır ve grubun tamamı bulunmalıdır.
    [Header("Script Atamaları")]
    [SerializeField] private Transform buttonsParent;
    [SerializeField] private List<LuckCardItems> allItems = new List<LuckCardItems>();

    private int totalChoose = 0;
    private List<Button> allNumberButtons = new List<Button>();
    private List<int> allItemsAmount = new List<int>();
    private List<string> allItemName = new List<string>();
    
    private void Start()
    {
        SetLotter();
    }
    private void Choosed(string itemName, int itemNo)
    {
        if (totalChoose == 5)
        {
            // Yeteri kadar çekiliş yaptınız
            return;
        }
        allNumberButtons[itemNo].interactable = false;
        //allNumberButtons[itemNo].transform.GetChild(1).gameObject.SetActive(false);
        totalChoose++;
        bool founded = false;
        for (int e = 0; e < allItems.Count && !founded; e++)
        {
            if (itemName == allItems[e].items)
            {
                founded = true;
                allItems[e].itemFounded++;
                if (allItems[e].itemFounded == allItems[e].itemRewardLimit)
                {
                    // Odulu kazandınız.
                    totalChoose = 5;
                }
            }
        }
        if (totalChoose == 5)
        {
            totalChoose = 0;
            SetLotter();
        }
    }
    private void SetLotter()
    {
        // İtemlerin rewardLimitlerini yedekliyoruz ve hangileri olduklarını belirliyoruz.
        for (int e = 0; e < allItems.Count; e++)
        {
            allItems[e].itemFounded = allItems[e].itemRewardLimit;
            allItems[e].items = allItems[e].luckCardItems[Random.Range(0, allItems[e].luckCardItems.Count)];
        }
        // Cıkacak Item Listesini ve numaraları düzenle
        allItemsAmount.Clear();
        for (int e = 0; e < 49; e++)
        {
            allItemsAmount.Add(e);
            allItemName[e] = "";

            allNumberButtons[e].interactable = true;
            allNumberButtons[e].transform.GetChild(1).gameObject.SetActive(true);
        }
        // İtemlerin rewardLimitlerini kullanarak lotoyu ödüllerle dolduruyoruz.
        int itemSirasi = 0;
        for (int e = 0; e < 25 && itemSirasi < allItems.Count; e++)
        {
            if (allItems[itemSirasi].itemRewardLimit != 0)
            {
                int rnd = allItemsAmount[Random.Range(0, allItemsAmount.Count)];
                allItemName[rnd] = allItems[itemSirasi].items;
                allItemsAmount.Remove(rnd);
                allItems[itemSirasi].itemRewardLimit--;
                string isim = allItemName[rnd];
                allNumberButtons[rnd].onClick.RemoveAllListeners();
                allNumberButtons[rnd].onClick.AddListener
                    (() => Choosed(isim, rnd));
                allNumberButtons[rnd].GetComponentInChildren<TextMeshProUGUI>().text = isim;
            }
            else
            {
                itemSirasi++;
                allItems[e].itemRewardLimit = allItems[e].itemFounded;
                allItems[e].itemFounded = 0;
            }
        }
    }
}