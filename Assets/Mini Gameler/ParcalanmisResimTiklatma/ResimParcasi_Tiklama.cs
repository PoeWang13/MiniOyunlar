using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ResimParcasi_Tiklama : MonoBehaviour, IPointerClickHandler
{
    [Header("Script Atamaları")]
    [SerializeField] private Transform resim;
    [SerializeField] private RawImage rawImage;
    [SerializeField] private ParcalanmisResimTiklatma parcalanmisResimTiklatma;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            resim.Rotate(0, 0, 90);
            parcalanmisResimTiklatma.ResimKontrol();
        }
    }
    public void ResimParcaAyarla(Texture resim, Vector2 resimParca, Vector2 uvYer)
    {
        rawImage.texture = resim;
        rawImage.uvRect = new Rect(resimParca.x, resimParca.y, uvYer.x, uvYer.y);
    }
}