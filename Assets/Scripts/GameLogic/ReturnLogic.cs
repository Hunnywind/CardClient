using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using GameItem;
using LogicStates;
using CardPositions;

public partial class LogicManager : MonoBehaviour
{
    private List<GameObject> returnCards = new List<GameObject>();

    public void EnemyReturnInfoUpdate(int fieldNum)
    {
        enemy.AddReturnWait(fieldNum);
    }

    public void AddReturnCard(Card card)
    {
        returnCards.Add(card.gameObject);
    }
    public void RemoveReturnCard(Card card)
    {
        returnCards.Remove(card.gameObject);
    }
    IEnumerator ReturnStart()
    {
        presentlevel = Level.Return;
        yield return new WaitForSeconds(3f);
        confirmButton.gameObject.SetActive(true);
    }
    IEnumerator ReturnEnd()
    {
        presentlevel = Level.Return_End;
        yield return new WaitForSeconds(3f);
        StartCoroutine(DoReturn());
        
    }
    IEnumerator DoReturn()
    {
        enemy.ReturnCardSet();
        yield return new WaitForSeconds(1f);
        returnCards.Sort(new CardTurnSort());
        UIManager.instance.SettingText(false);
        Debug.Log("Return Cards Num:" + returnCards.Count);
        foreach (var card in returnCards)
        {
            Debug.Log("Return Card Active!");
            card.GetComponent<Card>().ChangePosition(new InHand());
            yield return new WaitForSeconds(0.5f);
        }
        player.LockAction();
<<<<<<< HEAD
=======
        returnCards.Clear();
>>>>>>> 70ebc7505a8a6384034b9b65e7ebfab1be2633a1
        stateMachine.ChangeState(new SummonLogic());
    }
}