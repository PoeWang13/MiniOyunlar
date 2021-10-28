using UnityEngine;

public class Civi_Manager : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private int golLimit;
    [SerializeField] private int solKalePuan;
    [SerializeField] private int SagKalePuan;
    [SerializeField] private Civi_Top top;
    [SerializeField] private Civi_Kale solKale, sagKale;
    private bool solIsNext = true;

    public void TopGonderildi()
    {
        solIsNext = !solIsNext;
        solKale.canUseTop = false;
        sagKale.canUseTop = false;
    }
    public void TopBeklemede()
    {
        solKale.canUseTop = solIsNext;
        sagKale.canUseTop = !solIsNext;
        // Netten oynanırsa burada ilgili tarafa topu yonlendirme izni verilecek.
    }
    private bool CanUseTop()
    {
        return solKale.canUseTop || sagKale.canUseTop;
    }
    public void TopDirection(Vector2 direction)
    {
        top.SetDirection(direction, true);
    }
    public void GolOldu(bool isSol)
    {
        top.SetDirection();
        if (isSol)
        {
            SagKalePuan++;
            if (SagKalePuan >= golLimit)
            {
                MacBitti(false);
            }
            else
            {
                top.transform.position = new Vector3( -7, 0, 0);
            }
        }
        else
        {
            solKalePuan++;
            if (solKalePuan >= golLimit)
            {
                MacBitti(true);
            }
            else
            {
                top.transform.position = new Vector3(7, 0, 0);
            }
        }
    }
    private void MacBitti(bool isSolWin)
    {
        PopUp_Manager.Instance.SetTitle(isSolWin ? "Sol Kazandı" : "Sag Kazandı")
                              .SetMessage("Tebrikler");
    }
    private void OnMouseUpAsButton()
    {
        if (!CanUseTop())
        {
            return;
        }
        Vector2 direc = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 topDirec = ((Vector2)top.transform.position - direc).normalized;
        TopDirection(topDirec);
    }
}