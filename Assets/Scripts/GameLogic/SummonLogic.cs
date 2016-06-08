using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using GameItem;
using LogicStates;

public partial class LogicManager : MonoBehaviour
{
    IEnumerator SummonStart()
    {
        yield return new WaitForSeconds(3f);
        confirmButton.gameObject.SetActive(true);
        turnText.text = "Summon Phase";
        presentlevel = Level.Summon;
    }
}