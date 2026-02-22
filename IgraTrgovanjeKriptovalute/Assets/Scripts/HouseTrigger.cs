using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseTrigger : MonoBehaviour
{
    private GeneralManagerField GeneralManagerFieldLink;

    private void Awake()
    {
        GeneralManagerFieldLink = FindObjectOfType<GeneralManagerField>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "car")
        {
            GeneralManagerFieldLink.IsInHouseTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "car")
        {
            GeneralManagerFieldLink.IsInHouseTrigger = false;
        }
    }
}
