using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Daralan : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private DaralanAlan daralanAlan;
    [SerializeField] private DaralanParent daralanParent;
    [SerializeField] private bool canMove;
    [SerializeField] private bool isX;
    [SerializeField] private float ilerleme;
    private void Start()
    {
        daralanAlan = FindObjectOfType<DaralanAlan>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("YapBoz"))
        {
            if (canMove)
            {
                canMove = false;
                daralanAlan.NoktaGonder(isX, collision.contacts[0].point);
                daralanParent.DuvaraDeydiler(this);
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }
    }
    private void Update()
    {
        if (canMove)
        {
            ilerleme += Time.deltaTime * 1.5f;
            if (isX)
            {
                transform.localScale = new Vector3(ilerleme, 0.25f, 1);
            }
            else
            {
                transform.localScale = new Vector3(0.25f, ilerleme, 1);
            }
        }
    }
    public void CanMove()
    {
        canMove = true;
        GetComponent<BoxCollider2D>().enabled = true;
    }
    public bool IsCanMove()
    {
        return canMove = true;
    }
}