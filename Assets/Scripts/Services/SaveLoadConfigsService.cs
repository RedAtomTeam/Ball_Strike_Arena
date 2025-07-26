using UnityEngine;

public class SaveLoadConfigsService : MonoBehaviour
{
    static public SaveLoadConfigsService Instance;



    private void Awake()
    {



        if (Instance == null)
        {
            Instance = this;
            LoadAll();

            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            SaveAll();
        }
    }


    public void SaveAll()
    {

    }

    public void LoadAll()
    {
        
    }

}
