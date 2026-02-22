using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] objekti = GameObject.FindGameObjectsWithTag("music");

        if (objekti.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
