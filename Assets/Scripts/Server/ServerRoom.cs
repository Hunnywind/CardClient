using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameItem;
using UnityEngine.SceneManagement;

public class ServerRoom : MonoBehaviour {
    public static ServerRoom instance = null;

    private int clientCount = 0;
    private Text clientCountText;
    private Text buttonText;

    public int ClientCount
    {
        set { clientCount = value; }
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        clientCountText = GameObject.Find("ClientCount").GetComponent<Text>();
        buttonText = GameObject.Find("ConnectText").GetComponent<Text>();
        clientCountText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (clientCountText != null)
        {
            clientCountText.text = "Client Count : " + clientCount;
        }
        if (GameClient.instance.GetState == State.MachingWait)
        {
            buttonText.text = "Maching Wait";
        }
    }
    public void ServerJoinComplete()
    {
        clientCountText.gameObject.SetActive(true);
        buttonText.text = "Game Start";
    }
    public void MachingComplete()
    {
        buttonText.text = "Maching Complete";
        Invoke("GameStart", 2f);
    }
    private void GameStart()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
