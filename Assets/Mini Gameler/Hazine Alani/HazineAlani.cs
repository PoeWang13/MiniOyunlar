using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HazineAlani : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private GameObject warningPanel;
    public HazineAlanBalon balon;
    public HazineSandık hazine;

    [SerializeField] private int balonCreatingTime = 3;
    [SerializeField] private int hazineAlanAktifTime;
    [SerializeField] private int balonCreateLimitZ;
    [SerializeField] private Vector2 balonCreateLimitX;
    [SerializeField] private Vector2 balonCreateLimitY;

    private Transform player;
    private bool createBalon;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SetWarning("Welcome to Treasure Area");
            player = other.transform;
            createBalon = true;
            StartCoroutine(CreateBalon());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (createBalon)
            {
                createBalon = false;
                hazine.animator.SetTrigger("Patla");
            }
        }
    }
    IEnumerator CreateBalon()
    {
        yield return new WaitForSeconds(balonCreatingTime);
        if (createBalon)
        {
            HazineAlanBalon hazineAlanBalon = Instantiate(balon, 
                new Vector3(Random.Range(balonCreateLimitX.x, balonCreateLimitX.y),
                            Random.Range(balonCreateLimitX.x, balonCreateLimitX.y), 
                            balonCreateLimitZ), Quaternion.identity);
            hazineAlanBalon.SetBalon(player, this);
            StartCoroutine(HazineBalonDeAktifTime());
        }
    }
    IEnumerator HazineBalonDeAktifTime()
    {
        yield return new WaitForSeconds(hazineAlanAktifTime);
        createBalon = false;
        hazine.animator.SetTrigger("Patla");
    }
    public void SetWarning(string warning)
    {
        warningText.text = warning;
        StartCoroutine(Warning());
    }
    IEnumerator Warning()
    {
        warningPanel.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        warningPanel.SetActive(false);
    }
}