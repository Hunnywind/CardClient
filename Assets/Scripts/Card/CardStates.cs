using System;
using UnityEngine;

namespace CardStates
{
    public class Setting : CardState<Card>
    {
        public override void enter(Card entity)
        {
        }
        public override void update(Card entity)
        {
            if (LogicManager.instance.PresentLevel == GameItem.Level.Battle)
            {
                entity.ChangeState(new Battle());
            }
        }
        public override void exit(Card entity)
        {
        }
        public override string GetStateName()
        {
            return "SETTING";
        }
    }
    public class Battle : CardState<Card>
    {
        private Animator anim;
        private int[] attackField;

        public override void enter(Card entity)
        {
            anim = entity.gameObject.GetComponentInChildren<Animator>();
            if(entity.cardinfo.a_type == 0)
            {
                attackField = new int[1];
                attackField[0] = entity.FieldNumber;
            }
        }
        public override void update(Card entity)
        {
            if(anim != null && entity.AttackOrder)
            {
                entity.AttackOrder = false;
                anim.SetTrigger("TriggerAttack");
                if(!entity.IsEnemyCard)
                {
                    foreach(var val in attackField)
                    {
                        LogicManager.instance.Enemy.Damaged(val, entity.cardinfo.attack);
                    }
                }
                else
                {
                    foreach (var val in attackField)
                    {
                        LogicManager.instance.Player.Damaged(val, entity.cardinfo.attack);
                    }
                }
            }
            if (anim != null && entity.Damaged)
            {
                entity.Damaged = false;
                anim.SetTrigger("TriggerDamaged");
            }
            if (LogicManager.instance.PresentLevel == GameItem.Level.Return)
                entity.ChangeState(new Return());
        }
        public override void exit(Card entity)
        {
        }
        public override string GetStateName()
        {
            return "BATTLE";
        }
    }
    /*public class Hand : CardState<Card>
    {
        public override void enter(Card entity)
        {
            if (!entity.IsEnemyCard)
            {
                LogicManager.instance.Player.AddCardHand(entity.gameObject);
            }
            entity.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
        public override void update(Card entity)
        {
            if(LogicManager.instance.PresentLevel == GameItem.Level.Summon
                && !entity.IsEnemyCard)
            {
                entity.ChangeState(new Setting());
            }
        }
        public override void mouseUp(Card entity)
        {
            Debug.Log("Hand MouseUp");
        }
        public override void mouseDown(Card entity)
        {
            Debug.Log("Hand MouseDown");
        }
        public override void mouseDrag(Card entity)
        {
        }
        public override void exit(Card entity)
        {
            Debug.Log("Hand Exit");
            if(!entity.IsEnemyCard)
            LogicManager.instance.Player.RemoveCardHand(entity.gameObject);
        }
    }*/
    public class Return : CardState<Card>
    {
        public override void enter(Card entity)
        {
        }
        public override void update(Card entity)
        {
            if (LogicManager.instance.PresentLevel == GameItem.Level.Summon)
                entity.ChangeState(new Summon());
        }
        public override void exit(Card entity)
        {
        }
        public override string GetStateName()
        {
            return "RETURN";
        }
    }
    public class Summon : CardState<Card>
    {
        public override void enter(Card entity)
        {
        }
        public override void update(Card entity)
        {
            if (LogicManager.instance.PresentLevel == GameItem.Level.Battle)
                entity.ChangeState(new Battle());
        }
        public override void exit(Card entity)
        {
        }
        public override string GetStateName()
        {
            return "SUMMON";
        }
    }
}
