using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class GizliObje : MonoBehaviour, IPointerClickHandler
{
    [Header("Script Atamaları")]
    [SerializeField] private Image image;
    [SerializeField] private ResimdeGizliObjeBulma resimdeGizliObjeBulma;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (resimdeGizliObjeBulma.ResimBulundumu(image.sprite))
            {
                gameObject.SetActive(false);
            }
        }
    }
}