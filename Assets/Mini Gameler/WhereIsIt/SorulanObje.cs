using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class SorulanObje : MonoBehaviour, IPointerClickHandler
{
    [Header("Script Atamaları")]
    [SerializeField] private WhereIsIt whereIsIt;
    public List<string> sorulanObjeSorular = new List<string>();

    private void Start()
    {
        whereIsIt = FindObjectOfType<WhereIsIt>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            whereIsIt.ObjeKontrol(this);
        }
    }
}