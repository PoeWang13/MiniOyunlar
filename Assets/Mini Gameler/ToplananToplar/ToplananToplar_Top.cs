using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ToplananToplar_Top : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private ToplananToplar toplananToplar;
    [SerializeField] private int speed;
    [SerializeField] private int parti;
    [SerializeField] private Vector3 direction;
    private void Start()
    {
        parti = (transform.parent.position.x > transform.position.x) ? 0 : 1;
        toplananToplar = FindObjectOfType<ToplananToplar>();
        direction = (Random.Range(0, 2) == 0 ? Vector3.left : Vector3.right) +
                    (Random.Range(0, 2) == 0 ? Vector3.up : Vector3.down);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("YapBoz"))
        {
            direction = Vector3.Reflect(direction, collision.contacts[0].normal);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("YapBoz2"))
        {
            toplananToplar.KontrolToplar();
        }
    }
    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
    public int MyParti()
    {
        return parti;
    }
}