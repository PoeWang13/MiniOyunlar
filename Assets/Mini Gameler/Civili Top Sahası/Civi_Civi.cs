using UnityEngine;

public class Civi_Civi : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private Civi_Top top;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            top.SetDirection(Vector2.Reflect(top.direction, collision.contacts[0].normal));
        }
    }
}