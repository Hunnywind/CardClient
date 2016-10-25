using System;
using UnityEngine;
using GameItem;

namespace CardPositions
{
    // 고려해야할 정보: 피아식별, 게임 상태
    public class InHand : CardPosition<Card>
    {
        private GameObject m_drag;
        private SpriteRenderer m_render;
        private SpriteRenderer m_drag_render;
        private Vector3 m_initialPosition;
        private bool m_goField = false;

        public override void enter(Card entity)
        {
            m_initialPosition = entity.gameObject.transform.position;

            m_drag = new GameObject();
            m_drag.AddComponent<SpriteRenderer>();
            m_drag.gameObject.transform.position = entity.gameObject.transform.position;
            m_drag.GetComponent<SpriteRenderer>().sprite = entity.gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
            m_drag.GetComponent<SpriteRenderer>().enabled = false;
            m_drag.GetComponent<SpriteRenderer>().sortingOrder = 5;
            m_render = entity.gameObject.GetComponentInChildren<SpriteRenderer>();
            m_drag_render = m_drag.GetComponent<SpriteRenderer>();

            if (!entity.IsEnemyCard)
            {
                LogicManager.instance.Player.AddCardHand(entity.gameObject);
            }
            else
            {
                LogicManager.instance.Enemy.AddCardHand(entity.gameObject);
            }

        }
        public override void exit(Card entity)
        {
            m_drag.AddComponent<Destroy>();
            if (!entity.IsEnemyCard)
            {
                LogicManager.instance.Player.RemoveCardHand(entity.gameObject);
            }
            else
            {
                LogicManager.instance.Enemy.RemoveCardHand(entity.gameObject);
            }
        }
        public override void update(Card entity)
        {
            if(m_goField)
            {
                entity.ChangePosition(new InField());
            }
        }
        public override void mouseDown(Card entity)
        {
        }
        public override void mouseUp(Card entity)
        {
            if(entity.IsEnemyCard)
            {
                // show card info
            }
            else if (entity.GetCurrentStateName().Equals("BATTLE")
                || entity.GetCurrentStateName().Equals("RETURN"))
            {
                // show card info
            }
            else if (entity.GetCurrentStateName().Equals("SETTING")
                || entity.GetCurrentStateName().Equals("SUMMON"))
            {
                m_render.enabled = true;
                m_drag_render.enabled = false;
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if(!hit)
                {
                    entity.gameObject.transform.position = m_initialPosition;
                }
                else if (hit.transform.tag.Equals("PlayerField"))
                {
                    LogicManager.instance.Player.SubtractMana(entity.Cardinfo.mana);
                    entity.gameObject.transform.position = hit.transform.position;
                    entity.FieldNumber = hit.transform.gameObject.GetComponent<Field>().number;
                    LogicManager.instance.FieldColliderSet(entity.FieldNumber, false);
                    m_goField = true;
                    
                }
                else
                {
                    entity.gameObject.transform.position = m_initialPosition;
                }
            }
        }
        public override void mouseDrag(Card entity)
        {
            if (entity.IsEnemyCard) return;
            if (entity.GetCurrentStateName().Equals("BATTLE") || entity.GetCurrentStateName().Equals("RETURN"))
                return;

            m_render.enabled = false;
            m_drag_render.enabled = true;
            Vector3 newPosition = new Vector3();
            newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = 100;
            m_drag.transform.position = newPosition;
        }

        public override string GetPosition()
        {
            return "HAND";
        }
    }
    public class InField : CardPosition<Card>
    {
        private bool m_goHand = false;
        private bool m_highlight = false;

        public override void enter(Card entity)
        {
        }
        public override void exit(Card entity)
        {
            entity.m_isReturn = false;
            entity.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
            LogicManager.instance.FieldColliderSet(entity.FieldNumber, true);
            entity.FieldNumber = 0;

        }
        public override void update(Card entity)
        {
            if (m_goHand) entity.ChangePosition(new InHand());
            else
            {
                if (entity.IsEnemyCard)
                {
                    entity.gameObject.transform.position = LogicManager.instance.enemyfields[entity.FieldNumber - 1].transform.position;
                }
                else
                {
                    entity.gameObject.transform.position = LogicManager.instance.fields[entity.FieldNumber - 1].transform.position;
                }

                if (LogicManager.instance.PresentLevel == Level.Return
                    || LogicManager.instance.PresentLevel == Level.Return_Wait
                    || LogicManager.instance.PresentLevel == Level.Return_Init)
                {
                    if (entity.m_isReturn)
                    {
                        entity.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.cyan;
                    }
                    else
                    {
                        entity.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                    }
                }
            }
        }
        public override void mouseDown(Card entity)
        {   
        }
        public override void mouseUp(Card entity)
        {
            if (entity.IsEnemyCard)
            {
                // show card info
            }
            else if (entity.GetCurrentStateName().Equals("SETTING"))
            {
                m_goHand = true;
                LogicManager.instance.Player.AddMana(entity.Cardinfo.mana);
                
            }
            else if (entity.GetCurrentStateName().Equals("BATTLE"))
            {
                // show card info
            }
            else if (entity.GetCurrentStateName().Equals("RETURN"))
            {
                if (m_highlight)
                {
                    LogicManager.instance.RemoveReturnCard(entity);
                    entity.m_isReturn = false;
                    m_highlight = false;
                }
                else
                {
                    LogicManager.instance.AddReturnCard(entity);
                    entity.m_isReturn = true;
                    m_highlight = true;
                }
            }
            else if (entity.GetCurrentStateName().Equals("SUMMON")
                && !entity.m_lock)
            {
                m_goHand = true;
                LogicManager.instance.Player.AddMana(entity.Cardinfo.mana);
            }
        }
        public override void mouseDrag(Card entity)
        {
        }
        public override string GetPosition()
        {
            return "FIELD";
        }
    }
}
