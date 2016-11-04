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

    public void EnemySummonInfoUpdate(CardInfo_send info)
    {
        GameObject obj = GameObject.Find("CardPool").GetComponent<ObjectPool>().GetObject();
        Card summonCard = obj.GetComponent<Card>();

        summonCard.SetInfo(info);

        obj.GetComponentInChildren<SpriteRenderer>().enabled = false;
        obj.transform.position = LogicManager.instance.enemyfields[summonCard.FieldNumber - 1].transform.position;
        AddSummonCard(obj);
    }

    public void AddSummonCard(GameObject card)
    {
        summonCards.Add(card);
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
        foreach (var obj in summonCards)
        {
            obj.GetComponentInChildren<SpriteRenderer>().enabled = true;
        }
        yield return new WaitForSeconds(2f);
        summonCards.Sort(new CardTurnSort());
        foreach (var obj in summonCards)
        {
            Debug.Log(obj.GetComponent<Card>().FieldNumber);
            obj.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
            if (obj.GetComponent<Card>().IsEnemyCard)
            {
                Card card = obj.GetComponent<Card>();
                card.Init();
                card.ChangeState(new Summon());
                card.ChangePosition(new InField());
                enemy.RemoveCardHand(null);
            }
            yield return new WaitForSeconds(1f);
        }
        Debug.Log("Summon End");
        yield return new WaitForSeconds(0.5f);
        presentlevel = Level.Battle;
        summonCards.Clear();
        ChangeState(new BattleLogic());
    }
}