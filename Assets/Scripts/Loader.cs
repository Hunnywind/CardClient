using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour
{
    public GameObject gameClient;
    public GameObject logicManager;
    void Awake()
    {
        if(GameClient.instance == null)
        {
            Instantiate(gameClient);
        }
        if(LogicManager.instance == null)
        {
            Instantiate(logicManager);
        }
    }

}
