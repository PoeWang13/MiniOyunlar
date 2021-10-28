using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Yolcu : MonoBehaviour
{
    [Header("Script Atamaları")]
    private Vector3 startPos;
    private bool isMoving;
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isMoving = true;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPos = mousePos - transform.localPosition;
        }
    }
    private void OnMouseUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isMoving = false;
        }
    }
    private void Update()
    {
        if (isMoving)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.localPosition = new Vector3(mousePos.x - startPos.x, mousePos.y - startPos.y, 0);
        }
    }
}