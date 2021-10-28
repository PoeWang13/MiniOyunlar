using UnityEngine;

public class Racer : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private Race race;
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
            race.Lost();
        }
    }
}