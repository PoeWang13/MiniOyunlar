using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GizliObje_3D : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private GizliObjeBulma gizliObjeBulma;
    public Sprite mySprite;
    private void OnMouseUpAsButton()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (gizliObjeBulma.GizliObjeBulundumu(mySprite, gameObject))
            {
                gameObject.SetActive(false);
            }
        }
    }
}