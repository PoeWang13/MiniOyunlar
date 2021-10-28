using UnityEngine;

public class HareketliDuvar : MonoBehaviour
{
    [Header("Script Atamaları")]
    [SerializeField] private int yon = 1;
    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (transform.position.y < 3.5)
            {
                yon = 3;
            }
            else
            {
                yon = 0;
            }
        }
        else if(Input.GetKey(KeyCode.S))
        {
            if (transform.position.y > -3.5)
            {
                yon = -3;
            }
            else
            {
                yon = 0;
            }
        }
        else
        {
            yon = 0;
        }
        transform.Translate(Vector3.up * Time.deltaTime * yon);
    }
}