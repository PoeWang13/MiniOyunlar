using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ForwardBallResimParcasi : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private RawImage rawImage;
    [SerializeField] private ForwardBall forwardBall;
    public Vector2Int orjinayYerim;
    public Vector2Int yerim;
    private Vector2 uvScaleYer;
    public bool canMoving;
    private Vector3 startPos;
    private bool isMoving;
    public void SetColor(Color c)
    {
        rawImage.color = c;
    }
    public void ResimParcaAyarla(Texture resim, Vector2Int resimParcaYerim, Vector2 resimParcaPozisyon, Vector2 uvScale)
    {
        uvScaleYer = uvScale;
        orjinayYerim = resimParcaYerim;
        rawImage.texture = resim;
        rawImage.uvRect = new Rect(resimParcaPozisyon.x, resimParcaPozisyon.y, uvScaleYer.x, uvScaleYer.y);
    }
    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0) && canMoving && forwardBall.sectigimYapBoz == null)
        {
            isMoving = true;
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPos = mousePos - transform.localPosition;
            forwardBall.sectigimYapBoz = this;
        }
    }
    private void OnMouseUp()
    {
        if (forwardBall.sectigimYapBoz == this)
        {
            isMoving = false;
            forwardBall.sectigimYapBoz = null;
            if (forwardBall.eslestigimYapBoz != null)
            {
                yerim = forwardBall.eslestigimYapBoz.yerim;
                Vector3 yerPos = forwardBall.eslestigimYapBoz.transform.localPosition;
                yerPos.z = 5.0f;
                transform.localPosition = yerPos;
                forwardBall.eslestigimYapBoz = null;
                forwardBall.OyunKontrol();
            }
        }
    }
    private void Update()
    {
        if (isMoving && canMoving)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.localPosition = new Vector3(mousePos.x - startPos.x, mousePos.y - startPos.y, 5);
        }
    }
    public bool YerimDogrumu()
    {
        return yerim == orjinayYerim;
    }
}