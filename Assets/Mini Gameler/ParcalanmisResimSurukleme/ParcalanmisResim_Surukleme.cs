using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ParcalanmisResim_Surukleme : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private RawImage rawImage;
    [SerializeField] private ParcalanmisResimSurukleme parcalanmisResimSurukleme;
    private Vector2Int orjinayYerim;
    public Vector2Int yerim;
    private Vector2 uvScaleYer;
    public bool canGo;
    public bool isEmpty;
    Vector3 direction;
    Vector3 targetPozisyon;
    public void SetColor()
    {
        isEmpty = true;
        Color c = rawImage.color;
        c.a = 0;
        rawImage.color = c;
        gameObject.name = "Empty";
    }
    public void ResimParcaAyarla(Texture resim, Vector2Int resimParcaYerim, Vector2 resimParcaPozisyon, Vector2 uvScale)
    {
        uvScaleYer = uvScale;
        orjinayYerim = resimParcaYerim;
        yerim = resimParcaYerim;
        rawImage.texture = resim;
        rawImage.uvRect = new Rect(resimParcaPozisyon.x, resimParcaPozisyon.y, uvScaleYer.x, uvScaleYer.y);
    }
    private void Update()
    {
        if (canGo)
        {
            //transform.position = Vector3.MoveTowards(transform.localPosition, targetPozisyon, Time.deltaTime);
            transform.Translate(direction * Time.deltaTime * 5);
            if (Vector2.Distance(transform.localPosition, targetPozisyon) < 0.1f)
            {
                transform.localPosition = targetPozisyon;
                canGo = false;
                parcalanmisResimSurukleme.ResimKontrol();
            }
        }
    }
    private void OnMouseDown()
    {
        if (!isEmpty)
        {
            if ((yerim - parcalanmisResimSurukleme.emptyParca.yerim).sqrMagnitude == 1)
            {
                Move();

            }
        }
    }
    public void Move()
    {
        direction = ((Vector2)(parcalanmisResimSurukleme.emptyParca.yerim - yerim)).normalized;
        Vector2Int yer = parcalanmisResimSurukleme.emptyParca.yerim;
        parcalanmisResimSurukleme.emptyParca.Move(-direction, yerim, transform.localPosition);
        Move(direction, yer, parcalanmisResimSurukleme.emptyParca.transform.localPosition);
    }
    public void Move(Vector2 direc, Vector2Int yer, Vector3 poz)
    {
        targetPozisyon = poz;
        yerim = yer;
        direction = direc;
        canGo = true;
    }
    public bool YerimDogrumu()
    {
        return orjinayYerim == yerim;
    }
    public bool DogruParca(Vector2Int yer)
    {
        return yer == yerim;
    }
}