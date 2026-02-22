using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTrigger : MonoBehaviour
{
    private GeneralManagerField GeneralManagerFieldLink;

    private void Awake()
    {
        GeneralManagerFieldLink = FindObjectOfType<GeneralManagerField>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "coin")
        {
            GeneralManagerFieldLink.AddMoney(500);
            Destroy(collision.gameObject);
        }
    }
}
