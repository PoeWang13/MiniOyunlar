using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Balon : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private Balonlar balonlar;
    [SerializeField] private int myLife = 1;
    [SerializeField] private int mySpeed = 3;
    private void Start()
    {
        balonlar = FindObjectOfType<Balonlar>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("YapBoz"))
        {
            myLife--;
            other.gameObject.SetActive(false);
            if (myLife == 0)
            {
                Destroy(gameObject);
                balonlar.BalonPatladi(gameObject);
            }
        }
    }
    private void Update()
    {
        if (transform.position.y > 7)
        {
            transform.position = new Vector3(transform.position.x, -5,0);
        }
        transform.Translate(Vector3.up * Time.deltaTime * mySpeed);
    }
}