using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ResimData
{
    public Vector3 cameraPozisyon;
    public List<Sprite> resimSprites = new List<Sprite>();
}
public class ParcalanmisResimSurukleme : MonoBehaviour
{ 
    [Header("Script Atamaları")]
    [SerializeField] private Camera textureCamera;
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private ParcalanmisResim_Surukleme resimParcasi;
    public ParcalanmisResim_Surukleme emptyParca;
    [SerializeField] private int resimParcaSayisiX;
    [SerializeField] private int resimParcaSayisiY;
    [SerializeField] private List<Texture> resimTextures = new List<Texture>();
    [SerializeField] private List<ParcalanmisResim_Surukleme> resimParcalari = new List<ParcalanmisResim_Surukleme>();
    private void Start()
    {
        int resim = Random.Range(0, resimTextures.Count);
        for (int x = 0; x < resimParcaSayisiX; x++)
        {
            for (int y = 0; y < resimParcaSayisiY; y++)
            {
                ParcalanmisResim_Surukleme parcalanmisResim = Instantiate(resimParcasi, textureCamera.transform);
                parcalanmisResim.transform.localPosition = -new Vector3(1, 1, 0) * (resimParcaSayisiY - 1) * 0.5f + new Vector3(x, y, 5);

                Vector2 scale = new Vector2(1.0f / resimParcaSayisiX, 1.0f / resimParcaSayisiY);
                Vector2 offset = new Vector2(x * (1.0f / resimParcaSayisiX), y * (1.0f / resimParcaSayisiY));
                parcalanmisResim.ResimParcaAyarla(resimTextures[resim], new Vector2Int(x, y), offset, scale);
                resimParcalari.Add(parcalanmisResim);
                textureCamera.orthographicSize = resimParcaSayisiY * 0.55f;
            }
        }
        emptyParca = resimParcalari[(resimParcaSayisiX - 1) * resimParcaSayisiY];
        emptyParca.SetColor();
        // Parcaların yerlerini değiştir
        StartCoroutine(ResimParcasiKaristir());
    }
    IEnumerator ResimParcasiKaristir()
    {
        Vector2Int[] komsular = { new Vector2Int(1, 0), new Vector2Int(-1, 0), new Vector2Int(0, 1), new Vector2Int(0, -1) };
        Vector2Int lastKener = emptyParca.yerim;
        int s = 0;
        while (s < 200)
        {
            s++;
            bool kaydirdik = false;
            while (!kaydirdik)
            {
                int random = Random.Range(0, 4);
                Vector2Int kenar = komsular[random];
                Vector2Int kenarResim = emptyParca.yerim + kenar;
                if (lastKener != kenarResim)
                {
                    if (kenarResim.x >= 0 && kenarResim.y >= 0 && kenarResim.x < resimParcaSayisiX && kenarResim.y < resimParcaSayisiY)
                    {
                        for (int e = 0; e < resimParcalari.Count && !kaydirdik; e++)
                        {
                            if (resimParcalari[e].DogruParca(kenarResim))
                            {
                                kaydirdik = true;
                                lastKener = emptyParca.yerim;

                                // Pozisyonları ayarla
                                Vector3 emptyPozisyon = emptyParca.transform.localPosition;
                                emptyParca.transform.localPosition = resimParcalari[e].transform.localPosition;
                                resimParcalari[e].transform.localPosition = emptyPozisyon;
                                // Yerim ayarla
                                Vector2Int emptyYerim = emptyParca.yerim;
                                emptyParca.yerim = resimParcalari[e].yerim;
                                resimParcalari[e].yerim = emptyYerim;
                            }
                        }
                        kaydirdik = true;
                    }
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetWarning("Fix Resim");
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
    public void ResimKontrol()
    {
        bool resimYanlis = true;
        for (int e = 0; e < resimParcalari.Count && resimYanlis; e++)
        {
            resimYanlis = resimParcalari[e].YerimDogrumu();
        }
        if (resimYanlis)
        {
            // Odul ver.
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