using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SektirenRaket : MonoBehaviour
{
    private void Update()
    {
        // Raketin sağa sola dönmesi
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0, Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, -Time.deltaTime);
        }
        // Raketin yukarı aşağı dönmesi
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(-Time.deltaTime, 0, 0);
        }
    }
}