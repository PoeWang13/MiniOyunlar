using UnityEngine;

public class Civi_Kale : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private Civi_Manager manager;
    [SerializeField] private bool isSol;
    public bool canUseTop;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (isSol)
            {
                manager.GolOldu(isSol);
            }
        }
    }
}