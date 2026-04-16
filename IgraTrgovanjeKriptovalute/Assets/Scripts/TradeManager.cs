using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TradeManager : MonoBehaviour
{
    [SerializeField]
    public static List<int> VrednostiCryptoValute = new List<int>
    {
    17,15,20,35,30,54,56,58,57,59,60,62,61,63,65,64,66,68,70,69,
    71,73,75,74,76,78,80,79,77,76,78,81,83,85,84,86,88,90,89,87,
    85,83,82,80,78,76,74,72,70,68,66,65,63,61,60,58,56,55,53,52,
    50,48,47,45,44,42,40,38,37,35,33,31,30,28,27,25,24,22,21,20,
    22,24,26,28,30,32,35,37,39,41,43,45,47,49,52,54,56,58,60,63,
    65,67,69,72,74,76,78,80,83,85,87,89,91,93,95,97,96,94,92,90,
    88,86,84,82,80,78,76,74,72,70,68,66,64,62,60,58,56,54,52,50,
    48,46,44,42,40,38,36,34,32,30,28,26,24,22,20,18,16,15,14,13,
    15,17,19,22,24,27,29,32,35,37,40,43,45,48,50,53,56,58,61,64,
    67,69,72,75,78,80,83,86,88,91,94,96,98,97,95,93,91,89,87,85,
    83,81,79,77,75,73,71,69,67,65,63,61,59,57,55,53,51,49,47,45,
    43,41,39,37,35,33,31,29,27,25,23,21,20,19,18,17,16,15,14,13,
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
            DenarVBitcoinText.text = "Valuti Bitcoin: " + (float)VrednostCryptoValute / StanjeCrypto;
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
