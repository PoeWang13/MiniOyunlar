using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HazineAlanBalon : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private int balondaTutmaSüresi = 5;
    //[SerializeField] private int playerinSpeedChancePercent = 5;
    [SerializeField] private int playerYakalanmiskenTıklama;
    [SerializeField] private int balonSpeed = 5;
    [SerializeField] private Animator animator;
    [SerializeField] private KeyCode balondaTıklamaKeyCode = KeyCode.V;

    private Transform player;
    private HazineAlani hazineAlani;
    private float balondaTutmaSüresiNext;
    private bool yakaladim;
    private Vector3 direction;
    private int playerYakalanmiskenKaçmaTıklamasi = 10;

    public void SetBalon(Transform player, HazineAlani hazineAlani)
    {
        this.player = player;
        this.hazineAlani = hazineAlani;
    }
    private void Update()
    {
        if (yakaladim)
        {
            if (Input.GetKeyUp(balondaTıklamaKeyCode))
            {
                playerYakalanmiskenTıklama++;
                if (playerYakalanmiskenTıklama >= playerYakalanmiskenKaçmaTıklamasi)
                {
                    yakaladim = false;
                    // Balonu patlat.
                    animator.SetTrigger("Patla");
                    // Playerin hızını düzelt.
                    PlayerSpeedFix();
                }
            }
            balondaTutmaSüresiNext += Time.deltaTime;
            if (balondaTutmaSüresiNext >= balondaTutmaSüresi)
            {
                PlayerSpeedFix();
                animator.SetTrigger("Patla");
            }
        }
        if (Vector3.Distance(player.position, transform.position) > 0.1f)
        {
            direction = (player.position - transform.position).normalized;
            transform.Translate(direction * Time.deltaTime * balonSpeed);
        }
    }
    private void PlayerSpeedFix()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            yakaladim = true;
            hazineAlani.SetWarning("Click to " + balondaTıklamaKeyCode.ToString());
            // Playerin hızını düşür.
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            yakaladim = false;
            // Playerin hızını düzelt.
            PlayerSpeedFix();
        }
    }
}