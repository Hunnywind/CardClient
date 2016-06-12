using UnityEngine;
using System.Collections;

public class ConnectButton : MonoBehaviour
{

    public void Connect()
    {
        GameClient.instance.Connect();
    }
}
