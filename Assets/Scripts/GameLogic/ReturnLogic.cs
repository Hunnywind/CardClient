using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using GameItem;
using LogicStates;

public partial class LogicManager : MonoBehaviour
{
    IEnumerator ReturnStart()
    {
        presentlevel = Level.Return;
        yield return new WaitForSeconds(3f);
        turnText.text = "Return Phase";
        manaText.gameObject.SetActive(true);
        confirmButton.gameObject.SetActive(true);
    }
    IEnumerator ReturnEnd()
    {
        presentlevel = Level.Return_End;
        turnText.text = "Return End";
        confirmButton.gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);
        
        stateMachine.ChangeState(new SummonLogic());
    }
}