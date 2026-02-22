using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GeneralManagerHouse : MonoBehaviour
{
    private DataManager DataManagerLink;
    private TradeManager TradeManagerLink;
    private LevelLoader LevelLoaderLink;

    public TMP_Text MoneyCountTextHouse;
    public TMP_Text MoneyCountTextTrade;

    public GameObject[] GreySquares = new GameObject[4];
    public int[] RoomPrices = new int[4];
    public GameObject[] RoomTilemaps = new GameObject[4];


    public GameObject TradePanel;

    private void Awake()
    {
        LevelLoaderLink = FindObjectOfType<LevelLoader>();
        DataManagerLink = FindObjectOfType<DataManager>();
        TradeManagerLink = FindObjectOfType<TradeManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        TradeManagerLink.StanjeEvri = DataManagerLink.StanjeEvriGlobal;
        TradeManagerLink.StanjeCrypto = DataManagerLink.StanjeCryptoGlobal;

        MoneyCountTextHouse.text = "Stanje v evrih: " + TradeManagerLink.StanjeEvri + "€";

        if (DataManagerLink.IDNapredovanjaSob != 4)
        {
            for (int i = 0; i <= DataManagerLink.IDNapredovanjaSob; i++)
            {
                RoomTilemaps[i].SetActive(true);
                GreySquares[i].SetActive(true);
            }
        }
    }

    public void Gumbven()
    {
        DataManagerLink.StanjeEvriGlobal = TradeManagerLink.StanjeEvri;
        DataManagerLink.StanjeCryptoGlobal = TradeManagerLink.StanjeCrypto;
        LevelLoaderLink.LoadNextLevel(0);
    }

    public void GumbTrade()
    {
        TradePanel.SetActive(true);
    }

    public void GumbKupiSobo(int ID_sobe)
    {
        if (DataManagerLink.IDNapredovanjaSob+1 == ID_sobe)
        {
            if (TradeManagerLink.StanjeEvri >= RoomPrices[ID_sobe])
            {
                RoomTilemaps[ID_sobe].SetActive(true);
                GreySquares[ID_sobe].SetActive(true);
                TradeManagerLink.StanjeEvri -= RoomPrices[ID_sobe];
                MoneyCountTextHouse.text = "Stanje v evrih: " + TradeManagerLink.StanjeEvri + "€";
                DataManagerLink.IDNapredovanjaSob = ID_sobe;
            }
            else
            {
                StartCoroutine(TradeManagerLink.WaitToHideWarning("Nimaš dovolj denarja za novo sobo trguj ali ga naberi"));
            }
        }
    }
}
