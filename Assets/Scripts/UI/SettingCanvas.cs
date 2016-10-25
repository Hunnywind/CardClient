using UnityEngine;
using System.Collections;
using GameItem;

public class SettingCanvas : MonoBehaviour {
	void Start () {
        StartCoroutine(SettingPhase());
	}

    IEnumerator SettingPhase()
    {
        while(LogicManager.instance.PresentLevel == GameItem.Level.Init
            || LogicManager.instance.PresentLevel == GameItem.Level.Init_Wait)
        {
            yield return null;
        }
        gameObject.SetActive(false);
        
    }
}
