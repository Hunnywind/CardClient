using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour
{
    public GameObject gameClient;
    public GameObject logicManager;
    public GameObject serverRoom;

    void Awake()
    {
        if(GameClient.instance == null && gameClient != null)
        {
            Instantiate(gameClient);
        }
        if(LogicManager.instance == null && logicManager != null)
        {
            Instantiate(logicManager);
        }
        if (ServerRoom.instance == null && serverRoom != null)
        {
            Instantiate(serverRoom);
        }
    }

}
