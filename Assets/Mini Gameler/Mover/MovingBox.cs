using UnityEngine;

public class MovingBox : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private Mover mover;
    private Vector3 direction;
    [HideInInspector] public bool justGo;
    private void Update()
    {
        if (justGo)
        {
            transform.Translate(direction * Time.deltaTime * 5);
        }
    }
    public void SetDirection(Vector3 direc)
    {
        direction = direc;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            mover.Lost();
        }
    }
}