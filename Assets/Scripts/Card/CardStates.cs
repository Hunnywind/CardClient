using System;
using UnityEngine;

namespace CardStates
{
    public class Setting : CardState<Card>
    {
        private GameObject drag;
        private SpriteRenderer render;
        private SpriteRenderer drag_render;
        private Vector3 initialPosition;

        public override void enter(Card entity)
        {
            initialPosition = entity.gameObject.transform.position;

            drag = new GameObject();
            drag.AddComponent<SpriteRenderer>();
            drag.gameObject.transform.position = entity.gameObject.transform.position;
            drag.GetComponent<SpriteRenderer>().sprite = entity.gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
            drag.GetComponent<SpriteRenderer>().enabled = false;
            drag.GetComponent<SpriteRenderer>().sortingOrder = 5;
            render = entity.gameObject.GetComponentInChildren<SpriteRenderer>();
            drag_render = drag.GetComponent<SpriteRenderer>();
        }
        public override void update(Card entity)
        {
            if (LogicManager.instance.PresentLevel == GameItem.Level.Battle)
            {
                if (entity.FieldNumber != 0)
                    entity.ChangeState(new Battle());
                else
                    entity.ChangeState(new Hand());
            }
        }
        public override void mouseUp(Card entity)
        {
            Debug.Log("Setting MouseUp");
            render.enabled = true;
            drag_render.enabled = false;

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (!hit)
                moveToHand(entity);
            else if (hit.transform.tag.Equals("Field") && !hit.transform.gameObject.GetComponent<Field>().IsEnemyField)
                moveToField(entity, hit);
            else if (hit)
                moveToHand(entity);
        }
        public override void mouseDown(Card entity)
        {
            Debug.Log("Setting MouseDown");
            if(entity.FieldNumber == 0)
                LogicManager.instance.Player.SubtractMana(entity.Cardinfo.mana);
        }
        public override void mouseDrag(Card entity)
        {
            render.enabled = false;
            drag_render.enabled = true;
            Vector3 newPosition = new Vector3();
            newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition.z = 0;
            drag.transform.position = newPosition;
        }
        public override void exit(Card entity)
        {

            drag.AddComponent<Destroy>();
        }
        private void moveToField(Card entity, RaycastHit2D hit)
        {
            if (entity.FieldNumber != 0)
            {
                LogicManager.instance.FieldColliderSet(entity.FieldNumber, true);
            }
            entity.gameObject.transform.position = hit.transform.position;
            entity.FieldNumber = hit.transform.gameObject.GetComponent<Field>().number;
            LogicManager.instance.FieldColliderSet(entity.FieldNumber, false);
        }
        private void moveToHand(Card entity)
        {
            LogicManager.instance.Player.AddMana(entity.Cardinfo.mana);
            entity.gameObject.transform.position = initialPosition;
            if(entity.FieldNumber != 0)
            {
                LogicManager.instance.FieldColliderSet(entity.FieldNumber, true);
            }
            entity.FieldNumber = 0;
        }
    }
    public class Battle : CardState<Card>
    {
        private Animator anim;

        public override void enter(Card entity)
        {
            anim = entity.gameObject.GetComponentInChildren<Animator>();
            Debug.Log(entity.Cardinfo.cooltime);
        }
        public override void update(Card entity)
        {
            if(anim != null && entity.AttackOrder)
            {
                entity.AttackOrder = false;
                anim.SetTrigger("TriggerAttack");
            }
            if (LogicManager.instance.PresentLevel == GameItem.Level.Return)
                entity.ChangeState(new Return());
        }
        public override void mouseUp(Card entity)
        {
        }
        public override void mouseDown(Card entity)
        {
        }
        public override void mouseDrag(Card entity)
        {
        }
        public override void exit(Card entity)
        {
        }
    }
    public class Hand : CardState<Card>
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
    }
    public class Return : CardState<Card>
    {
        bool isclick;
        public override void enter(Card entity)
        {
            isclick = false;
        }
        public override void update(Card entity)
        {
            if (!entity.IsEnemyCard)
            {
                if (!isclick)
                    entity.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.white;
                else
                    entity.gameObject.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
            }
            if (LogicManager.instance.PresentLevel == GameItem.Level.Return_End)
                change(entity);
        }
        public override void mouseUp(Card entity)
        {
            
        }
        public override void mouseDown(Card entity)
        {
            if (isclick) isclick = false;
            else isclick = true;
        }
        public override void mouseDrag(Card entity)
        {
        }
        public override void exit(Card entity)
        {
        }
        private void change(Card entity)
        {
            if (!entity.IsEnemyCard)
            {
                if (isclick) entity.ChangeState(new Hand());
                else entity.ChangeState(new Battle());
            }
        }
    }
}
