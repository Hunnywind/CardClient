using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using GameItem;
using CardPositions;

namespace LogicStates
{
    public class SettingLogic : LogicState<LogicManager>
    {
        private GameObject skillButton;

        public override void enter(LogicManager entity)
        {
            GameClient.instance.FirstSetting();
            skillButton = GameObject.Find("MasterSkillButton");
            skillButton.SetActive(false);
            UIManager.instance.ShowStatus(false);
            UIManager.instance.ShowMaster(false);
            UIManager.instance.ShowOptionButton(false);
            UIManager.instance.ShowTurn(false);
            UIManager.instance.ShowGameEndText(false, false);
            ObjectPool objpool = GameObject.Find("CardPool").GetComponent<ObjectPool>();
            objpool.Init();

            for (int i = 0; i < entity.Player.m_cardNum; i++)
            {
                GameObject card = objpool.GetObject();
                CardData data = CardDatabase.Instance()
                    .GetCardData(CardDatabase.Instance().GetPlayerCard(i));
                CardInfo_send info;
                info.number = data.number;
                info.isEnemyCard = false;
                info.FieldLocation = 0;
                info.cooltime = data.speed;
                info.isReturn = false;
                info.leftcooltime = data.speed;
                info.health = data.health;
                card.GetComponent<Card>().SetInfo(info);
                card.GetComponent<Card>().Init();
                card.GetComponent<Card>().ChangePosition(new InHand());
                entity.Player.cards.Add(card);
            }

        }
        public override void update(LogicManager entity)
        {
            entity.manaText.text = entity.Player.m_mana + " / 12";
            //entity.Player.CardManaCheck();
        }
        public override void exit(LogicManager entity)
        {
            foreach (var card in entity.Player.cards_hand)
            {
                card.GetComponentInChildren<SpriteRenderer>().color = Color.white;
            }
            //skillButton.SetActive(false);
            UIManager.instance.ShowOptionButton(true);
            UIManager.instance.ShowStatus(true);
            UIManager.instance.ShowMaster(true);
            Vector3 newPosition = new Vector3();
            newPosition.x = 0;
            newPosition.y = 0;
            newPosition.z = 0;
            entity.PlayerFields.transform.position = newPosition;
            entity.EnemyFields.SetActive(true);
        }
    }
    public class BattleLogic : LogicState<LogicManager>
    {
        public override void enter(LogicManager entity)
        {
            UIManager.instance.ShowTurn(true);
            UIManager.instance.SettingText(false);

            Debug.Log("Battle Logic Start");
            entity.presentTurn = 1;
            //Vector3 newPosition = new Vector3();
            //for (int i = 0; i < 5; i++)
            //{
            //    newPosition = entity.fields[i].transform.position;
            //    newPosition.y = -1;
            //    entity.fields[i].transform.position = newPosition;

            //    newPosition = entity.enemyfields[i].transform.position;
            //    newPosition.y = 1;
            //    entity.enemyfields[i].transform.position = newPosition;
            //}
            GameObject.Find("TurnImage").GetComponent<TurnRound>().Init();
            entity.EnemyFields.GetComponent<Space>().m_isBattle = true;
            entity.PlayerFields.GetComponent<Space>().m_isBattle = true;
            entity.Player.CardArrange();
            entity.Player.CardInHandArrange();
            entity.Enemy.CardInit();
            entity.Enemy.CardArrange();
            entity.Enemy.CardInHandArrange();
            entity.LogicCoroutineStart(Level.Battle);
        }
        public override void update(LogicManager entity)
        {
            entity.manaText.text = entity.Player.m_mana + " / 12";
            if (entity.presentTurn > 6)
            {
                entity.ChangeState(new ReturnLogic());
            }
        }
        public override void exit(LogicManager entity)
        {
            //entity.TurnText.gameObject.SetActive(true);
            //entity.TurnText.text = "Battle Phase End";
            UIManager.instance.ShowTurn(false);
        }
    }
    public class ReturnLogic : LogicState<LogicManager>
    {
        public override void enter(LogicManager entity)
        {
            Debug.Log("Return Logic Start");
            UIManager.instance.SettingText(true, "- 회수할 카드를 선택하세요 -");
            entity.EnemyFields.GetComponent<Space>().m_isPrepare = true;
            entity.PlayerFields.GetComponent<Space>().m_isPrepare = true;
            entity.LogicCoroutineStart(Level.Return);
        }
        public override void update(LogicManager entity)
        {
            entity.manaText.text = entity.Player.m_mana + " / 12";
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
            UIManager.instance.SettingText(true, "- 소환할 카드를 선택하세요 -");
            entity.LogicCoroutineStart(Level.Summon);
        }
        public override void update(LogicManager entity)
        {
            entity.manaText.text = entity.Player.m_mana + " / 12";
        }
        public override void exit(LogicManager entity)
        {
            //entity.manaText.gameObject.SetActive(false);
            entity.confirmButton.gameObject.SetActive(false);
        }
    }
    public class GameEndLogic : LogicState<LogicManager>
    {
        public override void enter(LogicManager entity)
        {
        }
        public override void update(LogicManager entity)
        {
            
        }
        public override void exit(LogicManager entity)
        {
        }
    }
}
