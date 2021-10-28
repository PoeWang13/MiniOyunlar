using UnityEngine;

public class MoverPoint : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private Vector3 direction;
    [SerializeField] private int kullanimMiktari = 1;
    [SerializeField] private MovingBox mover;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            kullanimMiktari--;
            other.transform.position = transform.position;
            mover.SetDirection(direction);
            if (kullanimMiktari == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}