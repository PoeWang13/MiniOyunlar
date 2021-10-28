using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DaralanParent : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private DaralanAlan daralanAlan;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Daralan daralan1;
    [SerializeField] private Daralan daralan2;
    [SerializeField] private bool daraldilar1;
    [SerializeField] private bool daraldilar2;
    private Vector3 startPos;
    private bool isMoving;
    private bool canMoving = true;
    private void Start()
    {
        daralanAlan = FindObjectOfType<DaralanAlan>();
        BoxCollider2D box1 = daralan1.GetComponent<BoxCollider2D>();
        BoxCollider2D box2 = daralan2.GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(box1, box2);
        Physics2D.IgnoreCollision(box1, boxCollider);
        Physics2D.IgnoreCollision(box2, boxCollider);
    }
    public void DuvaraDeydiler(Daralan dar)
    {
        if (dar == daralan1)
        {
            daraldilar1 = true;
        }
        else
        {
            daraldilar2 = true;
        }
        if (daraldilar2 && daraldilar1)
        {
            daralanAlan.YeniDuvarGoster();
        }
    }
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
        if (daralanAlan.DuvarBirakilabilirmi(transform.position))
        {
            isMoving = false;
            canMoving = false;
            boxCollider.enabled = false;
            daralan1.CanMove();
            daralan2.CanMove();
        }
        else
        {
            isMoving = false;
        }
    }
    private void Update()
    {
        if (isMoving && canMoving)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.localPosition = new Vector3(mousePos.x - startPos.x, mousePos.y - startPos.y, 0);
        }
    }
}