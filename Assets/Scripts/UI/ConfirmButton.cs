using UnityEngine;
using System.Collections;

public class ConfirmButton : MonoBehaviour {

    public void Confirm()
    {
        LogicManager.instance.SettingEnd();
    }
    public void DeckSave()
    {
        DeckManager.instance.SaveDeck();
    }
}
