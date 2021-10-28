using UnityEngine;

public class RacePoint : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private Vector3 direction;
    [SerializeField] private int kullanimMiktari = 1;
    [SerializeField] private Racer racer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            kullanimMiktari--;
            other.transform.position = transform.position;
            racer.SetDirection(direction);
            if (kullanimMiktari == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}