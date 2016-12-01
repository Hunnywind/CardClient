using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using GameItem;
using LogicStates;
using CardPositions;
using CardStates;

public partial class LogicManager : MonoBehaviour
{
    private List<GameObject> summonCards = new List<GameObject>();
    private List<GameObject> summonEnablePlayerCards = new List<GameObject>();
    private List<GameObject> summonEnableEnemyCards = new List<GameObject>();
    private int summonCount = 0;

    public void EnemySummonInfoUpdate(CardInfo_send info)
    {
        GameObject obj = GameObject.Find("CardPool").GetComponent<ObjectPool>().GetObject();
        Card summonCard = obj.GetComponent<Card>();

        summonCard.SetInfo(info);
        obj.GetComponentInChildren<SpriteRenderer>().color = Color.red;
        summonCard.CardTextSet(false);
        obj.transform.position = LogicManager.instance.enemyfields[summonCard.FieldNumber - 1].transform.position;
        //AddSummonCard(obj);
        summonEnableEnemyCards.Add(obj);
        summonCount++;
    }

    public void AddSummonCard(GameObject card)
    {
        summonCount++;
        summonEnablePlayerCards.Add(card);
        card.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;
    }
    public void RemoveSummonCard(GameObject card)
    {
        summonCards.Remove(card);
    }
    IEnumerator SummonStart()
    {
        yield return new WaitForSeconds(3f);
        confirmButton.gameObject.SetActive(true);
        presentlevel = Level.Summon;
    }
    IEnumerator DoSummon()
    {
        UIManager.instance.SettingText(true, "- 소환 단계 진행중 -");
        foreach (var obj in summonEnableEnemyCards)
        {
            obj.GetComponent<Card>().CardTextSet(true);
        }
        yield return new WaitForSeconds(0.5f);
        if (RandomSelecter.m_isMyTurn)
        {
            RandomSelecter.RandomActive();
            bool[] ran = { RandomSelecter.GetRandomValue(0), RandomSelecter.GetRandomValue(1), RandomSelecter.GetRandomValue(2),
            RandomSelecter.GetRandomValue(3), RandomSelecter.GetRandomValue(4)};
            GameClient.instance.SendRandInfo(ran);
        }

        for (int i = 0; i < summonCount; i++)
        {
            int playerFieldNum = 6;
            int enemyFieldNum = 6;

            summonEnablePlayerCards.Sort(new CardTurnSort());
            summonEnableEnemyCards.Sort(new CardTurnSort());
            if (summonEnablePlayerCards.Count != 0)
            {
                playerFieldNum = summonEnablePlayerCards[0].GetComponent<Card>().FieldNumber;
            }
            if (summonEnableEnemyCards.Count != 0)
            {
                enemyFieldNum = summonEnableEnemyCards[0].GetComponent<Card>().FieldNumber;
            }
            if (playerFieldNum < enemyFieldNum)
            {
                summonEnablePlayerCards[0].gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                summonEnablePlayerCards.RemoveAt(0);
            }
            else if (playerFieldNum > enemyFieldNum)
            {
                summonEnableEnemyCards[0].gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                Card card = summonEnableEnemyCards[0].GetComponent<Card>();
                card.Init();
                card.ChangeState(new Summon());
                card.ChangePosition(new InField());
                LogicManager.instance.Enemy.AddCard(card.gameObject);
                enemy.RemoveCardHand(null);
                summonEnableEnemyCards.RemoveAt(0);
            }
            else
            {
                if (RandomSelecter.GetRandomValue_n())
                {
                    summonEnablePlayerCards[0].gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                    summonEnablePlayerCards.RemoveAt(0);
                }
                else
                {
                    summonEnableEnemyCards[0].gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                    Card card = summonEnableEnemyCards[0].GetComponent<Card>();
                    card.Init();
                    card.ChangeState(new Summon());
                    card.ChangePosition(new InField());
                    LogicManager.instance.Enemy.AddCard(card.gameObject);
                    enemy.RemoveCardHand(null);
                    summonEnableEnemyCards.RemoveAt(0);
                }
            }
            yield return new WaitForSeconds(turnDelay);
        }
        Debug.Log("Summon End");
        summonCount = 0;
        yield return new WaitForSeconds(0.5f);
        presentlevel = Level.Battle;
        summonCards.Clear();

        ObjectPool objpool = GameObject.Find("CardPool").GetComponent<ObjectPool>();
        foreach (CardInfo_send soul in player.GetTrashCards())
        {
            GameObject card = objpool.GetObject();
            CardInfo_send newInfo;
            newInfo = soul;
            newInfo.FieldLocation = 0;
            newInfo.isEnemyCard = false;
            newInfo.isReturn = false;
            card.GetComponent<Card>().SetInfo(newInfo);
            card.GetComponent<Card>().Init();
            card.GetComponent<Card>().ChangePosition(new InHand());
            player.cards.Add(card);
        }
        player.GetTrashCards().Clear();
        foreach (CardInfo_send soul in enemy.GetTrashCards())
        {
            GameObject card = objpool.GetObject();
            CardInfo_send newInfo;
            newInfo = soul;
            newInfo.FieldLocation = 0;
            newInfo.isEnemyCard = true;
            newInfo.isReturn = false;
            card.GetComponent<Card>().SetInfo(newInfo);
            card.GetComponent<Card>().Init();
            card.GetComponent<Card>().ChangePosition(new InHand());
            enemy.cards.Add(card);
        }
        enemy.GetTrashCards().Clear();
        ChangeState(new BattleLogic());
    }
}