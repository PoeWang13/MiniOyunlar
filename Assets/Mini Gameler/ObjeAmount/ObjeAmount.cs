using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ObjeAmount : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private int objAmount;
    [SerializeField] private TMP_InputField objeAmountInput;
    [SerializeField] private Image objImage;
    [SerializeField] private List<Sprite> allTypeSprite = new List<Sprite>();
    [SerializeField] private List<Sprite> allSprite = new List<Sprite>();
    private void Start()
    {
        Sprite resim = allTypeSprite[Random.Range(0, allTypeSprite.Count)];
        for (int e = 0; e < allSprite.Count; e++)
        {
            if (resim == allSprite[e])
            {
                objAmount++;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
    public void AllObjFinded()
    {
        if (int.TryParse(objeAmountInput.text, out int amount))
        {
            Finded(amount);
        }
    }
    private void Finded(int amount)
    {
        if (objAmount == amount)
        {
            SetWarning("Tebrikler");
        }
        else
        {
            SetWarning("You Lost.");
        }
        StartCoroutine(Tebrikler());
    }
    IEnumerator Tebrikler()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}