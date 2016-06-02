using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace LogicStates
{
    public class SettingLogic : LogicState<LogicManager>
    {
        private Button confirmButton;
        private Text manaText;
        private Text turnText;

        public override void enter(LogicManager entity)
        {
            confirmButton = GameObject.Find("ConfirmButton").GetComponent<Button>();
            manaText = GameObject.Find("LeftManaText").GetComponent<Text>();
            turnText = GameObject.Find("TurnText").GetComponent<Text>();
            turnText.gameObject.SetActive(false);

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
                newPosition.y = -2;
                newPosition.z = 0;
                card.transform.position = newPosition;
                card.GetComponent<Card>().Init();
                entity.Player.cards.Add(card);
            }
        }
        public override void update(LogicManager entity)
        {
            manaText.text = "Left Mana : " + entity.Player.mana;
            entity.Player.CardManaCheck();
        }
        public override void exit(LogicManager entity)
        {
            confirmButton.gameObject.SetActive(false);
            manaText.gameObject.SetActive(false);
            turnText.gameObject.SetActive(true);
        }
    }
    public class BattleLogic : LogicState<LogicManager>
    {
        
        public override void enter(LogicManager entity)
        {
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
        }
        public override void update(LogicManager entity)
        {
        }
        public override void exit(LogicManager entity)
        {
        }
    }

}
