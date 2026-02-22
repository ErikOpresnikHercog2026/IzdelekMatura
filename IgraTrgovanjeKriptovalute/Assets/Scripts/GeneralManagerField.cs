using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GeneralManagerField : MonoBehaviour
{
    public bool IsInHouseTrigger;

    private LevelLoader LevelLoaderLink;
    private DataManager DataManagerLink;

    public TMP_Text MoneyCountText;

    private void Awake()
    {
        LevelLoaderLink = FindObjectOfType<LevelLoader>();
        DataManagerLink = FindObjectOfType<DataManager>();
    }

    public void AddMoney(int Amount)
    {
        DataManagerLink.StanjeEvriGlobal += Amount;
        MoneyCountText.text = "Stanje v evrih: " + DataManagerLink.StanjeEvriGlobal + "€";
    }
    
    public void HwasPressed()
    {
        if (IsInHouseTrigger)
        {
            LevelLoaderLink.LoadNextLevel(1);
        }
    }
}
