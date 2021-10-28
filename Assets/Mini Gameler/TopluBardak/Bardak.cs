using UnityEngine;

public class Bardak : MonoBehaviour
{
    [Header("Script Atamaları")]
    public bool canMove;
    [SerializeField] private Vector3 posizyon;
    [SerializeField] private TopluBardak topluBardak;
    [SerializeField] private bool canChoose;
    [HideInInspector] public bool canStop;
    [HideInInspector] public bool hasTop;
    public void SetPozisyon(Vector3 pos)
    {
        posizyon = pos;
        transform.position = pos;
    }
    private void Update()
    {
        if (canMove)
        {
            if (Vector3.Distance(posizyon, transform.position) < 0.1f)
            {
                if (canStop)
                {
                    canMove = false;
                    canChoose = true;
                }
                Vector3 posizyon2 = posizyon;
                posizyon = topluBardak.WantPozisyon();
                topluBardak.AddPozisyon(posizyon2);
            }
        }
    }
    private void OnMouseUpAsButton()
    {
        if (hasTop && canChoose)
        {
            topluBardak.OdulVer();
        }
    }
}