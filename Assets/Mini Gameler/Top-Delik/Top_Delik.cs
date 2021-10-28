using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Top_Delik : MonoBehaviour
{
    [Header("Script Atamalarý")]
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private GameObject topFirlatmaButton;
    [SerializeField] private Transform topPoint;
    [SerializeField] private Slider ileriSlider;
    [SerializeField] private Slider yukariSlider;
    [SerializeField] private KeyCode topIleriFirlatmaKeyCode = KeyCode.V;
    [SerializeField] private KeyCode topYukariFirlatmaKeyCode = KeyCode.B;
    [SerializeField] private float topIleriFirlamaPower = 50;
    [SerializeField] private float topYukariFirlamaPower = 50;
    [SerializeField] private int topSayisi;
    [SerializeField] private int topSayisiNext;
    [SerializeField] private Rigidbody myTop;
    [SerializeField] private bool canFirlat;
    [SerializeField] private ForceMode forceMode = ForceMode.Impulse;
    private int myPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            topSayisiNext = 0;
            myPoint = 0;
            ileriSlider.value = 0;
            yukariSlider.value = 0;
            SetWarning("Welcome to Top's Hole");
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
    public void SetPoint(int point)
    {
        canFirlat = true;
        myPoint += point;
        topFirlatmaButton.SetActive(true);
        SetWarning(myPoint.ToString());
        if (topSayisiNext == topSayisi)
        {
            OdulVer();
        }
    }
    private void Update()
    {
        if (!canFirlat)
        {
            return;
        }
        if (Input.GetKeyDown(topIleriFirlatmaKeyCode))
        {
            topIleriFirlamaPower = 50;
        }
        if (Input.GetKey(topIleriFirlatmaKeyCode))
        {
            topIleriFirlamaPower += Time.deltaTime;
            ileriSlider.value = topIleriFirlamaPower;
        }
        if (Input.GetKeyDown(topYukariFirlatmaKeyCode))
        {
            topYukariFirlamaPower = 50;
        }
        if (Input.GetKey(topYukariFirlatmaKeyCode))
        {
            topYukariFirlamaPower += Time.deltaTime;
            yukariSlider.value = topYukariFirlamaPower;
        }
    }
    private void OdulVer()
    {
        // Adama önce ödülü ver
        Destroy(gameObject);
    }
    public void Firlat()
    {
        topFirlatmaButton.SetActive(false);
        canFirlat = false;
        topSayisiNext++;
        Vector3 topForce = new Vector3(topIleriFirlamaPower, topYukariFirlamaPower, 0);
        myTop.AddForceAtPosition(topForce, myTop.position, forceMode);
    }
}