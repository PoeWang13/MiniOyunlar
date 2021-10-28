using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HalüsinasyonluAlan : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private int zorluk;
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private Material textureMaterial;
    [SerializeField] private Material normalMaterial;
    [SerializeField] private GameObject newCamera;
    [SerializeField] private GameObject textureCanvas;
    [SerializeField] private GameObject textureMaterialObje;
    [SerializeField] private GameObject rendersizObje;
    [SerializeField] private List<bool> zorlukCesidi = new List<bool>()
                            { false, false, false, false, false, false};

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetWarning("Welcome to Luck Wheel");
            AlanOlustur(zorluk);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AlanIptal();
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
    private void AlanOlustur(int zor)
    {
        int zorlukVerildi = 0;
        while (zorlukVerildi != zor)
        {
            int random = Random.Range(0, 6);
            if (random == 0 && !zorlukCesidi[0])
            {
                zorlukVerildi++;
                zorlukCesidi[0] = true;
                // Kamera Frame ayarı
                Camera.main.clearFlags = CameraClearFlags.Nothing;
            }
            else if (random == 1 && !zorlukCesidi[1])
            {
                zorlukVerildi++;
                zorlukCesidi[1] = true;
                // Kamera görüş uzaklık ayarı
                Camera.main.fieldOfView = 160;
            }
            else if (random == 2 && !zorlukCesidi[2])
            {
                zorlukVerildi++;
                zorlukCesidi[2] = true;
                // 2. Kamera ayarı
                newCamera.SetActive(true);
            }
            else if (random == 3 && !zorlukCesidi[3])
            {
                zorlukVerildi++;
                zorlukCesidi[3] = true;
                // RawImage ayarı
                textureCanvas.SetActive(true);
            }
            else if (random == 4 && !zorlukCesidi[4])
            {
                zorlukVerildi++;
                zorlukCesidi[4] = true;
                // Gorunmez obje ayarı
                rendersizObje.GetComponent<Renderer>().enabled = false;
            }
            else if (random == 5 && !zorlukCesidi[5])
            {
                zorlukVerildi++;
                zorlukCesidi[5] = true;
                // Sahte gorüşlü obje ayarı
                textureMaterialObje.SetActive(true);
                textureMaterialObje.GetComponent<Renderer>().material = normalMaterial;
            }
        }
    }
    private void AlanIptal()
    {
        zorlukCesidi = new List<bool>() { false, false, false, false, false, false };
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.fieldOfView = 60;
        newCamera.SetActive(false);
        textureCanvas.SetActive(false);
        rendersizObje.GetComponent<Renderer>().enabled = true;
        textureMaterialObje.SetActive(false);
        textureMaterialObje.GetComponent<Renderer>().material = textureMaterial;

    }
}