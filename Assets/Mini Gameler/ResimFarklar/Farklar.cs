using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Farklar : MonoBehaviour, IPointerClickHandler
{
    [Header("Script Atamaları")]
    [SerializeField] private ResimFarklar resimFarklar;
    [SerializeField] private Image image;

    private void Start()
    {
        resimFarklar = FindObjectOfType<ResimFarklar>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (resimFarklar.HataFinded(image.sprite))
            {
                Destroy(gameObject);
            }
        }
    }
}