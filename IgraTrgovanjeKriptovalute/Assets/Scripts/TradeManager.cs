using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TradeManager : MonoBehaviour
{
    public List<int> VrednostiCryptoValute;

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
