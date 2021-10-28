using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FarkliObje : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private ObjeFarklar objeFarklar;
    public bool farkliyim = false;

    private void Start()
    {
        objeFarklar = FindObjectOfType<ObjeFarklar>();
    }
    private void OnMouseUpAsButton()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (farkliyim)
            {
                objeFarklar.HataFinded();
                Destroy(gameObject);
            }
        }
    }
}