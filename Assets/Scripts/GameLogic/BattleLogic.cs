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
    public float turnDelay = 1.0f;

    IEnumerator TurnStart()
    {
        turnText.gameObject.SetActive(true);
        turnText.text = "Turn " + presentTurn;
        yield return new WaitForSeconds(1.5f);

        turnText.gameObject.SetActive(false);
        player.mana++;
        
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
            StartCoroutine(TurnStart());
    }
}