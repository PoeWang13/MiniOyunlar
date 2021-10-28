using UnityEngine;

public class Mayın : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private int bombaPower = 100;
    [SerializeField] private int bombaRadius = 3;
    [SerializeField] private int bombaModifier = 1;
    [SerializeField] private ForceMode bombaForceMode = ForceMode.Impulse;
    
    private Rigidbody playerRigidbody;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playerRigidbody == null)
            {
                playerRigidbody = other.GetComponent<Rigidbody>();
            }
            playerRigidbody.AddExplosionForce(bombaPower, transform.position, bombaRadius, bombaModifier, bombaForceMode);
        }
    }
}