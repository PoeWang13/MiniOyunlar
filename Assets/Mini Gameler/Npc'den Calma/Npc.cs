using UnityEngine;
using System.Collections;

public class Npc : MonoBehaviour
{
    [Header("Script Atamaları")]
    public float npcGorusUzaklığı = 5;
    [Range(0, 360)] public float npcGorusGenislik = 90;

    [HideInInspector] public GameObject player;

    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask objeMask;

    [HideInInspector] public bool canSeePlayer;
    private bool playerCezalandirildi;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Gorus());
    }
    private IEnumerator Gorus()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            GorusGerceklestir();
        }
    }
    private void Cezalandir()
    {
        Debug.Log("Cezalandir");
    }
    private void GorusGerceklestir()
    {
        if (playerCezalandirildi)
        {
            return;
        }
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, npcGorusUzaklığı, targetMask);
        if (rangeChecks.Length > 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionTarget) < npcGorusGenislik / 2)
            {
                float distanceTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionTarget, distanceTarget, objeMask))
                {
                    canSeePlayer = true;
                    playerCezalandirildi = true;
                    Cezalandir();
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }
}