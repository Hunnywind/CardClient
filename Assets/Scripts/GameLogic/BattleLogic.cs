using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using GameItem;
using LogicStates;

public partial class LogicManager : MonoBehaviour
{
    private List<GameObject> turnEnableCards = new List<GameObject>();
    private Text turnText;

    public List<GameObject> TurnEnableCards
    {
        get { return turnEnableCards; }
    }
    public Text TurnText
    {
        get { return turnText; }
    }
    public float turnDelay = 0.1f;

    IEnumerator TurnStart()
    {
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
        turnEnableCards.Sort(new CardTurnSort());
        foreach (GameObject obj in turnEnableCards)
        {
            yield return new WaitForSeconds(turnDelay);
            obj.GetComponent<Card>().AttackOrder = true;
        }
        yield return new WaitForSeconds(turnDelay);
        presentTurn++;
        turnEnableCards.Clear();
        if (presentTurn < 7)
        {
            StartCoroutine(TurnStart());
        }
    }
}