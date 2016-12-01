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
    private GameObject deckButton;
    private static bool isServerConnect = false;

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

        deckButton = GameObject.Find("DeckMakeButton");
    }

    void Start()
    {
        clientCountText = GameObject.Find("ClientCount").GetComponent<Text>();
        buttonText = GameObject.Find("ConnectText").GetComponent<Text>();
        clientCountText.gameObject.SetActive(false);
        deckButton.SetActive(false);
        SoundManager.Instance.PlayBGM(0);
        if (isServerConnect)
        {
            ServerJoinComplete();
            GameClient.instance.RequestClientCount();
        }
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
        isServerConnect = true;
        clientCountText.gameObject.SetActive(true);
        buttonText.text = "Game Start";
        deckButton.SetActive(true);
    }
    public void MachingComplete()
    {
        buttonText.text = "Maching Complete";
        Invoke("GameStart", 2f);
    }
    private void GameStart()
    {
        SceneManager.LoadScene("UI");
    }
}
