using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ForwardBall_Top : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private int speed = 3;
    [SerializeField] private Vector3 direction;
    private void Start()
    {
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
    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}