using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParcalanmisResimYapBoz : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private LayerMask yapBozMask;
    [SerializeField] private Camera textureCamera;
    [SerializeField] private ParcalanmisResim_YapBoz resimParcasi;
    [SerializeField] private int resimParcaSayisiX = 3;
    [SerializeField] private int resimParcaSayisiY = 3;
    public ParcalanmisResim_YapBoz eslestigimYapBoz;
    public ParcalanmisResim_YapBoz sectigimYapBoz;
    [SerializeField] private List<Texture> resimTextures = new List<Texture>();
    [SerializeField] private List<ParcalanmisResim_YapBoz> resimParcalari = new List<ParcalanmisResim_YapBoz>();
    private void Start()
    {
        CreatePuzzle();
    }
    private void CreatePuzzle()
    {
        int resim = Random.Range(0, resimTextures.Count);
        int sira = 0;
        for (int x = 0; x < resimParcaSayisiX; x++)
        {
            for (int y = 0; y < resimParcaSayisiY; y++)
            {
                ParcalanmisResim_YapBoz parcalanmisResim = Instantiate(resimParcasi, textureCamera.transform);
                parcalanmisResim.transform.localPosition = -new Vector3(1, 1, 0) * (resimParcaSayisiY - 1) * 0.5f + new Vector3(x, y, 5);
                parcalanmisResim.canMoving = true;
                parcalanmisResim.gameObject.name = "resimParcasi_" + sira;
                Vector2 scale = new Vector2(1.0f / resimParcaSayisiX, 1.0f / resimParcaSayisiY);
                Vector2 offset = new Vector2(x * (1.0f / resimParcaSayisiX), y * (1.0f / resimParcaSayisiY));
                parcalanmisResim.ResimParcaAyarla(resimTextures[resim], new Vector2Int(x, y), offset, scale);
                resimParcalari.Add(parcalanmisResim);
                textureCamera.orthographicSize = resimParcaSayisiY * 0.55f;
                parcalanmisResim.yerim = new Vector2Int(-1, -1);


                ParcalanmisResim_YapBoz resimParcasiKoyacakPozisyon = Instantiate(resimParcasi, textureCamera.transform);
                resimParcasiKoyacakPozisyon.transform.localPosition = -new Vector3(1, 1, 0) * (resimParcaSayisiY - 1) * 0.5f + new Vector3(x, y, 5.2f);
                resimParcasiKoyacakPozisyon.gameObject.tag = "YapBoz";
                resimParcasiKoyacakPozisyon.gameObject.name = "resimParcasiKoyacakPozisyon_" + sira;
                sira++;
                resimParcasiKoyacakPozisyon.gameObject.layer = 9;
                resimParcasiKoyacakPozisyon.yerim = new Vector2Int(x, y);
            }
        }
        // Parcaların yerlerini değiştir
        StartCoroutine(ResimParcasiKaristir());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetWarning("Fix Resim");
            CreatePuzzle();
        }
    }
    IEnumerator Warning()
    {
        warningPanel.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        warningPanel.SetActive(false);
    }
    public void SetWarning(string s)
    {
        warningText.text = s;
        StartCoroutine(Warning());
    }
    IEnumerator ResimParcasiKaristir()
    {
        Vector3 ilkYer = resimParcalari[0].transform.localPosition + new Vector3(-1, -1, 0);
        Vector3 sonYer = resimParcalari[resimParcalari.Count - 1].transform.localPosition + new Vector3(1, 1, 0);
        float xYerim = 0;
        float yYerim = 0;
        float scale = resimParcalari[0].transform.localScale.x;
        for (int e = 0; e < resimParcalari.Count; e++)
        {
            yield return new WaitForSeconds(0.1f);
            int xx = Random.Range(0, 2);
            xYerim = (xx == 0) ? ilkYer.x : sonYer.x;
            yYerim = Random.Range(ilkYer.y + scale, sonYer.y - scale);

            resimParcalari[e].transform.localPosition = new Vector3(xYerim, yYerim, 5);
        }
    }
    private void Update()
    {
        if (sectigimYapBoz != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, 50, yapBozMask))
            {
                if (eslestigimYapBoz != null)
                {
                    eslestigimYapBoz.SetColor(Color.white);
                }
                eslestigimYapBoz = hit.transform.GetComponent<ParcalanmisResim_YapBoz>();
                eslestigimYapBoz.SetColor(Color.red);
            }
            else if (eslestigimYapBoz != null)
            {
                eslestigimYapBoz.SetColor(Color.white);
                eslestigimYapBoz = null;
            }
        }
    }
    public void OyunKontrol()
    {
        bool oyunBitti = true;
        for (int e = 0; e < resimParcalari.Count && oyunBitti; e++)
        {
            oyunBitti = resimParcalari[e].YerimDogrumu();
        }
        if (oyunBitti)
        {
            SetWarning("Tebrikler");
            StartCoroutine(Tebrikler());
        }
    }
    IEnumerator Tebrikler()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}