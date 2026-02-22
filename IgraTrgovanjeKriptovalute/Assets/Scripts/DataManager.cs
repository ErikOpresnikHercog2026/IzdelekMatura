using UnityEngine;

public class DataManager : MonoBehaviour
{
    public int StanjeEvriGlobal;
    public int StanjeCryptoGlobal;
    public int VrednostiIndexGlobal;
    public int IDNapredovanjaSob;

    private void Awake()
    {
        GameObject[] objekti = GameObject.FindGameObjectsWithTag("DataManager");

        if (objekti.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

}
