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
    private List<GameObject> returnPlayerCards = new List<GameObject>();
    private List<GameObject> returnEnemyCards = new List<GameObject>();
    private int returnCount = 0;

    public void EnemyReturnInfoUpdate(int fieldNum)
    {
        enemy.AddReturnWait(fieldNum);
    }

    public void AddReturnCard(Card card)
    {
        if (card.IsEnemyCard)
        {
            card.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.cyan;
            returnEnemyCards.Add(card.gameObject);
        }
        else
            returnPlayerCards.Add(card.gameObject);
        returnCount++;
    }
    public void RemoveReturnCard(Card card)
    {
        if (card.IsEnemyCard)
            returnEnemyCards.Remove(card.gameObject);
        else
            returnPlayerCards.Remove(card.gameObject);
        returnCount--;
    }
    IEnumerator ReturnStart()
    {
        presentlevel = Level.Return;
        yield return new WaitForSeconds(1f);
        confirmButton.gameObject.SetActive(true);
    }
    IEnumerator ReturnEnd()
    {
        presentlevel = Level.Return_End;
        yield return new WaitForSeconds(1f);
        StartCoroutine(DoReturn());
        
    }
    IEnumerator DoReturn()
    {
        enemy.ReturnCardSet();
        yield return new WaitForSeconds(1f);
        UIManager.instance.SettingText(false);
        Debug.Log("ReturnCardP " + returnPlayerCards.Count);
        Debug.Log("ReturnCardE " + returnEnemyCards.Count);
        if (RandomSelecter.m_isMyTurn)
        {
            RandomSelecter.RandomActive();
            bool[] ran = { RandomSelecter.GetRandomValue(0), RandomSelecter.GetRandomValue(1), RandomSelecter.GetRandomValue(2),
            RandomSelecter.GetRandomValue(3), RandomSelecter.GetRandomValue(4)};
            GameClient.instance.SendRandInfo(ran);
        }
        for(int i = 0; i < returnCount; i++)
        {
            int playerFieldNum = 6;
            int enemyFieldNum = 6;

            returnPlayerCards.Sort(new CardTurnSort());
            returnEnemyCards.Sort(new CardTurnSort());
            if (returnPlayerCards.Count != 0)
            {
                playerFieldNum = returnPlayerCards[0].GetComponent<Card>().FieldNumber;
            }
            if (returnEnemyCards.Count != 0)
            {
                enemyFieldNum = returnEnemyCards[0].GetComponent<Card>().FieldNumber;
            }
            if (playerFieldNum < enemyFieldNum)
            {
                returnPlayerCards[0].GetComponent<Card>().ChangePosition(new InHand());
                returnPlayerCards[0].GetComponent<Card>().FieldNumber = 0;
                returnPlayerCards[0].GetComponent<Card>().Heal(true, 0);
                returnPlayerCards.RemoveAt(0);
            }
            else if (playerFieldNum > enemyFieldNum)
            {
                returnEnemyCards[0].GetComponent<Card>().ChangePosition(new InHand());
                returnEnemyCards[0].GetComponent<Card>().FieldNumber = 0;
                returnEnemyCards[0].GetComponent<Card>().Heal(true, 0);
                returnEnemyCards.RemoveAt(0);
            }
            else
            {
                if (RandomSelecter.GetRandomValue_n())
                {
                    returnPlayerCards[0].GetComponent<Card>().ChangePosition(new InHand());
                    returnPlayerCards[0].GetComponent<Card>().FieldNumber = 0;
                    returnPlayerCards[0].GetComponent<Card>().Heal(true, 0);
                    returnPlayerCards.RemoveAt(0);
                }
                else
                {
                    returnEnemyCards[0].GetComponent<Card>().ChangePosition(new InHand());
                    returnEnemyCards[0].GetComponent<Card>().FieldNumber = 0;
                    returnEnemyCards[0].GetComponent<Card>().Heal(true, 0);
                    returnEnemyCards.RemoveAt(0);
                }
            }
            yield return new WaitForSeconds(turnDelay);
        }
        player.LockAction();
        returnPlayerCards.Clear();
        returnEnemyCards.Clear();
        returnCount = 0;
        stateMachine.ChangeState(new SummonLogic());
    }
}