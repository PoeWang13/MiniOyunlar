using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ParcalanmisResim_Kaydırma : MonoBehaviour, IPointerClickHandler
{
    [Header("Script Atamaları")]
    [SerializeField] private RawImage rawImage;
    [SerializeField] private ParcalanmisResimKaydırma parcalanmisResimKaydırma;
    private Vector2 orjinayYerim;
    private Vector2 yerim;
    private Vector2 uvScaleYer;
    public Vector2 MyYerim()
    {
        return yerim;
    }
    public void MyColor(Color color)
    {
        rawImage.color = color;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (parcalanmisResimKaydırma.GetResimParcasi() == null)
            {
                MyColor(Color.gray);
                parcalanmisResimKaydırma.SetResimParcasi(this);
            }
            else
            {
                parcalanmisResimKaydırma.ResimYerDegistir(this);
            }
        }
    }
    public void ResimParcaAyarla(Texture resim, Vector2 resimParcaPozisyon, Vector2 uvScale)
    {
        uvScaleYer = uvScale;
        orjinayYerim = resimParcaPozisyon;
        yerim = resimParcaPozisyon;
        rawImage.texture = resim;
        rawImage.uvRect = new Rect(resimParcaPozisyon.x, resimParcaPozisyon.y, uvScaleYer.x, uvScaleYer.y);
    }
    public void ResimParcaDegis(Vector2 resimParcaPozisyon)
    {
        yerim = resimParcaPozisyon;
        rawImage.uvRect = new Rect(resimParcaPozisyon.x, resimParcaPozisyon.y, uvScaleYer.x, uvScaleYer.y);
    }
    public bool YerimDogrumu()
    {
        return orjinayYerim == yerim;
    }
}