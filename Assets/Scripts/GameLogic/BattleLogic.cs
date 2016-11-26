using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using GameItem;
using LogicStates;

public partial class LogicManager : MonoBehaviour
{
    private List<GameObject> turnEnablePlayerCards = new List<GameObject>();
    private List<GameObject> turnEnableEnemyCards = new List<GameObject>();

    private Text turnText;

    public List<GameObject> TurnEnableCards
    {
        get { return turnEnablePlayerCards; }
    }
    public Text TurnText
    {
        get { return turnText; }
    }
    public float turnDelay = 0.1f;

    IEnumerator TurnStart()
    {
        int num = 0;
        int playerFieldNum = 6;
        int enemyFieldNum = 6;

        player.m_mana++;
        turnText.text = presentTurn.ToString();
        GameObject.Find("TurnImage").GetComponent<TurnRound>().TurnEnd();
        if(RandomSelecter.m_isMyTurn)
        {
            RandomSelecter.RandomActive();
            bool[] ran = { RandomSelecter.GetRandomValue(0), RandomSelecter.GetRandomValue(1), RandomSelecter.GetRandomValue(2),
            RandomSelecter.GetRandomValue(3), RandomSelecter.GetRandomValue(4)};
            GameClient.instance.SendRandInfo(ran);
        }
        yield return new WaitForSeconds(0.5f);        
        foreach (GameObject card in player.cards)
        {
            card.GetComponent<Card>().TurnStart();
        }
        foreach (GameObject card in enemy.cards)
        {
            card.GetComponent<Card>().TurnStart();
        }
        foreach (GameObject card in player.cards)
        {
            if (card.GetComponent<Card>().IsAttackReady)
                num++;
        }
        foreach (GameObject card in enemy.cards)
        {
            if (card.GetComponent<Card>().IsAttackReady)
                num++;
        }
        for(int i = 0; i < num; i++)
        {
            foreach (GameObject card in player.cards)
            {
                card.GetComponent<Card>().AttackReady();
            }
            foreach (GameObject card in enemy.cards)
            {
                card.GetComponent<Card>().AttackReady();
            }
            turnEnablePlayerCards.Sort(new CardTurnSort());
            turnEnableEnemyCards.Sort(new CardTurnSort());
            //foreach (GameObject obj in turnEnablePlayerCards)
            //{
            //    yield return new WaitForSeconds(turnDelay);
            //    obj.GetComponent<Card>().AttackOrder = true;
            //}
            if(turnEnablePlayerCards.Count != 0)
            {
                playerFieldNum = turnEnablePlayerCards[0].GetComponent<Card>().FieldNumber;
            }
            if(turnEnableEnemyCards.Count != 0)
            {
                enemyFieldNum = turnEnableEnemyCards[0].GetComponent<Card>().FieldNumber;
            }
            // compare field number
            if(playerFieldNum < enemyFieldNum)
            {
                turnEnablePlayerCards[0].GetComponent<Card>().AttackOrder = true;
            }
            else if(playerFieldNum > enemyFieldNum)
            {
                turnEnableEnemyCards[0].GetComponent<Card>().AttackOrder = true;
            }
            else
            {
                if(RandomSelecter.GetRandomValue_n())
                {
                    turnEnablePlayerCards[0].GetComponent<Card>().AttackOrder = true;
                }
                else
                {
                    turnEnablePlayerCards[0].GetComponent<Card>().AttackOrder = false;
                }
            }
            yield return new WaitForSeconds(turnDelay);
            
            turnEnablePlayerCards.Clear();
            turnEnableEnemyCards.Clear();
            playerFieldNum = 6;
            enemyFieldNum = 6;
        }
        presentTurn++;
        if (presentTurn < 7)
        {
            StartCoroutine(TurnStart());
        }
    }
}