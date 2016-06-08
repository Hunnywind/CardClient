using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using GameItem;

namespace LogicStates
{
    public class SettingLogic : LogicState<LogicManager>
    {
        public override void enter(LogicManager entity)
        {
            entity.TurnText.gameObject.SetActive(false);
            ObjectPool objpool = GameObject.Find("CardPool").GetComponent<ObjectPool>();
            objpool.Init();
            Vector3 newPosition = new Vector3();
            int blank = 30;
            for (int i = 0; i < entity.Player.cardNum; i++)
            {
                GameObject card = objpool.GetObject();
                newPosition.x = ((-((float)card.GetComponentInChildren<SpriteRenderer>().sprite.texture.width * 0.5f + blank * 0.5f) *
                    (entity.Player.cardNum - 1)) +
                    (((float)card.GetComponentInChildren<SpriteRenderer>().sprite.texture.width + blank)* i))
                    * 1 / 100;
                newPosition.y = -(float)card.GetComponentInChildren<SpriteRenderer>().sprite.texture.height * 1 / 100;
                newPosition.z = 0;
                card.transform.position = newPosition;
                card.GetComponent<Card>().Init();
                entity.Player.cards.Add(card);
            }
        }
        public override void update(LogicManager entity)
        {
            entity.manaText.text = "Left Mana : " + entity.Player.mana;
            entity.Player.CardManaCheck();
        }
        public override void exit(LogicManager entity)
        {
            entity.manaText.gameObject.SetActive(false);
            entity.confirmButton.gameObject.SetActive(false);
        }
    }
    public class BattleLogic : LogicState<LogicManager>
    {
        public override void enter(LogicManager entity)
        {
            entity.TurnText.gameObject.SetActive(true);
            Debug.Log("Battle Logic Start");
            entity.presentTurn = 1;
            Vector3 newPosition = new Vector3();
            for (int i = 0; i < 5; i++)
            {
                newPosition = entity.fields[i].transform.position;
                newPosition.y = -1;
                entity.fields[i].transform.position = newPosition;

                newPosition = entity.enemyfields[i].transform.position;
                newPosition.y = 1;
                entity.enemyfields[i].transform.position = newPosition;
            }
            entity.Player.CardArrange();
            entity.LogicCoroutineStart(Level.Battle);
        }
        public override void update(LogicManager entity)
        {
            if(entity.presentTurn > 6)
            {
                entity.ChangeState(new ReturnLogic());
            }
        }
        public override void exit(LogicManager entity)
        {
            entity.TurnText.gameObject.SetActive(true);
            entity.TurnText.text = "Battle Phase End";
        }
    }
    public class ReturnLogic : LogicState<LogicManager>
    {
        public override void enter(LogicManager entity)
        {
            Debug.Log("Return Logic Start");
            Vector3 newPosition = new Vector3();
            newPosition.x = (Screen.width * 1/2);
            newPosition.y = (-Screen.height * 1/2 + entity.confirmButton.gameObject.GetComponent<RectTransform>().sizeDelta.y);
            newPosition.z = 0;
            entity.confirmButton.gameObject.GetComponent<RectTransform>().anchoredPosition = newPosition;

            entity.LogicCoroutineStart(Level.Return);
        }
        public override void update(LogicManager entity)
        {
            entity.manaText.text = "Left Mana : " + entity.Player.mana;
        }
        public override void exit(LogicManager entity)
        {
        }
    }
    public class SummonLogic : LogicState<LogicManager>
    {
        public override void enter(LogicManager entity)
        {
            Debug.Log("Summon Logic Start");

            entity.LogicCoroutineStart(Level.Summon);
        }
        public override void update(LogicManager entity)
        {
            entity.manaText.text = "Left Mana : " + entity.Player.mana;
        }
        public override void exit(LogicManager entity)
        {
            entity.manaText.gameObject.SetActive(false);
            entity.confirmButton.gameObject.SetActive(false);
        }
    }
}
