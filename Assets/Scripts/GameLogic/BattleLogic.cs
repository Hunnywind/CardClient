using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using GameItem;

partial class LogicManager : MonoBehaviour
{
    public GameObject[] fields_enemy;
    public Dictionary<int, GameObject> enemyCards = new Dictionary<int, GameObject>();
    private List<GameObject> turnEnableCards = new List<GameObject>();

    public float turnDelay = 0.5f;

    public int PresentTurn;

    private Text turnText;

    public void BattleSetting()
    {
        foreach(GameObject card in player.cards)
        {
            card.SetActive(true);
        }
        for(int i = 0; i < 5; i++)
        {
            Vector3 newPosition = fields[i].transform.position;
            newPosition.y = -2f;
            fields[i].transform.position = newPosition;
            newPosition.y = 0f;
            fields_enemy[i].transform.position = newPosition;
            fields_enemy[i].GetComponent<Field>().number = i;
        }
        
        for (int i = 0; i < 5; i++)
        {
            if(player.cards_inField.ContainsKey(i))
            {
                player.cards_inField[i].GetComponent<Card_infield>().field
                    = fields[i];
            }
        }
        PresentTurn = 1;
        TurnInvoke();
    }
    public void CardAdd(GameObject obj)
    {
        turnEnableCards.Add(obj);
    }
    private void TurnInvoke()
    {
        
        StartCoroutine(TurnStart());
    }
    private void EnemyBattleSetting()
    {
        for (int i = 0; i < 5; i++)
        {
            if (Random.Range(0, 2) == 1)
            {
                GameObject card = GameObject.Find("FieldCardPool").GetComponent<ObjectPool>().GetObject();
                card.GetComponent<Card_infield>().field = fields_enemy[i];
                card.GetComponent<Card_infield>().Init();
                card.GetComponent<Card_infield>().fieldNumber = i;
                card.GetComponent<Card_infield>().EnemyCard();
                card.GetComponent<Card_infield>().mana = Random.Range(1, 6);
                card.GetComponent<Card_infield>().cooltime = Random.Range(1, 7);
                card.GetComponent<Card_infield>().leftCooltime = card.GetComponent<Card_infield>().cooltime;
                
                card.GetComponent<Card_infield>().cardName = "EnemyCard";
                enemyCards.Add(i, card);
            }
        }
    }

    IEnumerator TurnStart()
    {
        turnText.gameObject.SetActive(true);
        turnText.text = "Turn " + PresentTurn;
        Debug.Log("Turn " + PresentTurn + " Start");

        yield return new WaitForSeconds(2f);


        turnText.gameObject.SetActive(false);
        player.mana++;
        for(int i = 0; i< 5; i++)
        {
            if(player.cards_inField.ContainsKey(i))
            {
                player.cards_inField[i].GetComponent<Card_infield>().TurnStart();
        }
            if (enemyCards.ContainsKey(i))
            {
                enemyCards[i].GetComponent<Card_infield>().TurnStart();
            }
        }
        turnEnableCards.Sort(new CardTurnSort());
        Debug.Log("Sort Complete");
        foreach(GameObject obj in turnEnableCards)
        {
            yield return new WaitForSeconds(turnDelay);
            obj.GetComponent<Card_infield>().Active();
        }
        yield return new WaitForSeconds(turnDelay);
        PresentTurn++;
        turnEnableCards.Clear();
        if (PresentTurn < 7)
            Invoke("TurnInvoke", 1f);
        else
            Invoke("OrganiSetting", 2f);
    }
}
