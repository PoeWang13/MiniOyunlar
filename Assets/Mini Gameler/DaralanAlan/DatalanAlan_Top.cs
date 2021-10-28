using UnityEngine;

public class DatalanAlan_Top : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private DaralanAlan daralanAlan;
    [SerializeField] private int speed;
    [SerializeField] private Vector3 direction;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("YapBoz"))
        {
            direction = Vector3.Reflect(direction, collision.contacts[0].normal);
            if (collision.gameObject.TryGetComponent<Daralan>(out Daralan dar))
            {
                if (dar.IsCanMove())
                {
                    daralanAlan.GameFinish("You Lost.");
                }
            }
        }
    }
    private void Start()
    {
        direction = (Random.Range(0, 2) == 0 ? Vector3.left: Vector3.right) + 
                    (Random.Range(0, 2) == 0 ? Vector3.up : Vector3.down);
    }
    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
}