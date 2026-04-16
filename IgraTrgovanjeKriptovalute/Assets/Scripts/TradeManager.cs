using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TradeManager : MonoBehaviour
{
    [SerializeField]
    public List<int> VrednostiCryptoValute = new List<int>
    {
    10,11,12,13,15,14,16,18,17,19,20,22,21,23,25,24,26,28,30,29,
    31,33,35,34,36,38,40,39,37,36,38,41,43,45,44,46,48,50,49,47,
    45,43,42,40,38,36,34,32,30,28,26,25,23,21,20,18,16,15,13,12,
    11,10,9,8,7,6,5,4,3,2,3,4,5,6,7,8,10,12,14,16,
    18,20,22,24,26,28,30,32,34,36,38,40,42,45,48,50,53,56,58,60,
    63,66,69,72,75,78,80,83,86,88,91,94,96,98,97,95,93,91,89,87,
    85,83,81,79,77,75,73,71,69,67,65,63,61,59,57,55,53,51,49,47,
    45,43,41,39,37,35,33,31,29,27,25,23,21,20,19,18,17,16,15,14,


    15,17,19,22,25,28,32,36,40,44,48,52,56,60,65,70,75,80,85,90,
    95,100,98,96,94,92,90,88,86,84,82,80,78,76,74,72,70,68,66,64,
    62,60,58,56,54,52,50,48,46,44,42,40,38,36,34,32,30,28,26,24,
    22,20,18,16,14,12,10,9,8,7,6,5,4,3,2,3,4,6,8,10,
    13,16,20,25,30,35,40,45,50,55,60,65,70,75,80,85,90,95,100,97,
    94,91,88,85,82,79,76,73,70,67,64,61,58,55,52,49,46,43,40,37,
    34,31,28,25,22,20,18,16,14,12,10,9,8,7,6,5,4,3,2,1,


    2,4,6,9,13,18,24,31,39,48,58,69,81,94,100,96,92,88,84,80,
    76,72,68,64,60,56,52,48,44,40,36,32,28,24,20,17,14,12,10,8,
    7,6,5,4,3,2,1,2,3,5,7,10,14,19,25,32,40,49,59,70,
    82,95,100,97,93,89,85,81,77,73,69,65,61,57,53,49,45,41,37,33,
    29,25,21,18,15,12,10,8,6,5,4,3,2,1,2,3,5,8,12,17,


    18,20,22,25,28,32,36,40,45,50,55,60,65,70,75,80,85,90,95,100,
    98,96,94,92,90,88,86,84,82,80,78,76,74,72,70,68,66,64,62,60,
    58,56,54,52,50,48,46,44,42,40,38,36,34,32,30,28,26,24,22,20,
    18,16,14,12,10,9,8,7,6,5,4,3,2,1
    };

    private bool CanUpdatePrices = true;

    public int StanjeEvri;
    public int StanjeCrypto;
    private int VrednostCryptoValute;
    private int PrejsnaVrednostCryptoValute;

    public GameObject BuyInput;
    public GameObject SellInput;
    public GameObject Graph;

    public TMP_Text DenarVBitcoinText;
    public TMP_Text DenarVEvroText;
    public TMP_Text VrednostCryptoText;
    public TMP_Text WarningText;

    private GeneralManagerHouse GeneralManagerHouseLink;
    private DataManager DataManagerLink;
    private GraphDisplay GraphDisplayLink;

    private void Awake()
    {
        GeneralManagerHouseLink = FindObjectOfType<GeneralManagerHouse>();
        DataManagerLink = FindObjectOfType<DataManager>();
        GraphDisplayLink = FindObjectOfType<GraphDisplay>();
    }
    // Start is called before the first frame update
    void Start()
    {
        GraphDisplayLink.PokaziGraf(VrednostiCryptoValute);
        if (DataManagerLink.VrednostiIndexGlobal != 0)
        {
            Graph.LeanMoveX(Graph.transform.position.x - (50 * DataManagerLink.VrednostiIndexGlobal), 1f);
        }

        VrednostCryptoValute = VrednostiCryptoValute[DataManagerLink.VrednostiIndexGlobal];
        PrejsnaVrednostCryptoValute = VrednostCryptoValute;

    }

    // Update is called once per frame
    void Update()
    {
        if (CanUpdatePrices == true)
        {
            StartCoroutine(WaitToUpdateCrypto());
        }
    }

    IEnumerator WaitToUpdateCrypto()
    {
        CanUpdatePrices = false;
        yield return new WaitForSeconds(5);
        Graph.LeanMoveX((Graph.transform.position.x - 50), 1f);

        DataManagerLink.VrednostiIndexGlobal += 1;
        VrednostCryptoValute = VrednostiCryptoValute[DataManagerLink.VrednostiIndexGlobal];
        StanjeCrypto = Mathf.RoundToInt((VrednostCryptoValute * StanjeCrypto) / PrejsnaVrednostCryptoValute);
        PrejsnaVrednostCryptoValute = VrednostCryptoValute;
        
        UpdateTexts();

        CanUpdatePrices = true;

    }

    public IEnumerator WaitToHideWarning(string Warning)
    {
        WarningText.text = Warning;
        yield return new WaitForSeconds(5);
        WarningText.text = "";
    }

    public void UpdateTexts()
    {
        if (StanjeCrypto > 0)
        {
            if (StanjeCrypto < VrednostCryptoValute)
            {
                DenarVBitcoinText.text = "Valuti Bitcoin: " + (float)VrednostCryptoValute / StanjeCrypto;
            }
            else
            {
                DenarVBitcoinText.text = "Valuti Bitcoin: " + (float)StanjeCrypto / VrednostCryptoValute;
            }
        }

        DenarVEvroText.text = "Valuti Evro: " + StanjeCrypto + "€";

        GeneralManagerHouseLink.MoneyCountTextHouse.text = "Stanje v evrih: " + StanjeEvri + "€";
        GeneralManagerHouseLink.MoneyCountTextTrade.text = "Stanje v evrih: " + StanjeEvri + "€";

        VrednostCryptoText.text = "Vrednost bitcoina: " + VrednostCryptoValute + "€";
    }

    public void GumbNazaj()
    {
        GeneralManagerHouseLink.TradePanel.SetActive(false);
    }

    public void BuyCrypto()
    {
        int Amount = 0;
        Amount = int.Parse(BuyInput.GetComponent<TMP_InputField>().text);

        if (StanjeEvri >= Amount)
        {
            StanjeCrypto += Amount;
            StanjeEvri -= Amount;
            UpdateTexts();
        }
        else
        {
            StartCoroutine(WaitToHideWarning("Premalo evrov"));
        }
    }

    public void SellCrypto()
    {
        int Amount = 0;
        Amount = int.Parse(SellInput.GetComponent<TMP_InputField>().text);

        if (StanjeCrypto >= Amount)
        {
            StanjeCrypto -= Amount;
            StanjeEvri += Amount;
            UpdateTexts();
        }
        else
        {
            StartCoroutine(WaitToHideWarning("Premalo Bitcoinov"));
        }
    }


}
