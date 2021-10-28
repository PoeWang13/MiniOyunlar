using UnityEngine;

public class Civi_Top : MonoBehaviour
{
    [Header("Script Atamaları")]
    public Vector2 direction;
    [SerializeField] private float speed;
    [SerializeField] private Civi_Manager civi_Manager;
    private float speedOrj;
    private bool canMove;
    private void Start()
    {
        speedOrj = speed;
        Physics2D.IgnoreCollision(civi_Manager.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());
    }
    private void Update()
    {
        if (canMove)
        {
            transform.Translate(speed * Time.deltaTime * direction);
            speed -= Time.deltaTime;
            if (speed <= 0)
            {
                canMove = false;
                speed = 0;
                civi_Manager.TopBeklemede();
            }
        }
    }
    public void SetDirection(Vector2 dir, bool isKale = false)
    {
        direction = dir;
        if (isKale)
        {
            canMove = true;
            speed = speedOrj;
            civi_Manager.TopGonderildi();
        }
    }
    public void SetDirection()
    {
        direction = Vector2.zero;
        speed = 0;
    }
    private void OnMouseUpAsButton()
    {
        Debug.Log("222");
        //Vector2 direc = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 topDirec = ((Vector2)top.transform.position - direc).normalized;
        //TopDirection(topDirec);
    }
}