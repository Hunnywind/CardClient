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
            Debug.Log("Setting enter");
            initialPosition = entity.gameObject.transform.position;

            drag = new GameObject();
            drag.AddComponent<SpriteRenderer>();
            drag.gameObject.transform.position = entity.gameObject.transform.position;
            drag.GetComponent<SpriteRenderer>().sprite = entity.gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
            drag.GetComponent<SpriteRenderer>().enabled = false;

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
            else if (hit.transform.tag.Equals("Field"))
                moveToField(entity, hit);
            else if (hit)
                moveToHand(entity);
        }
        public override void mouseDown(Card entity)
        {
            Debug.Log("Setting MouseDown");
            if(entity.FieldNumber == 0)
                LogicManager.instance.Player.mana -= entity.Cardinfo.mana;
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
            Debug.Log("Setting Exit");
            
        }
        private void moveToField(Card entity, RaycastHit2D hit)
        {
            entity.gameObject.transform.position = hit.transform.position;
            entity.FieldNumber = hit.transform.gameObject.GetComponent<Field>().number;
            LogicManager.instance.FieldColliderSet(entity.FieldNumber, false);
        }
        private void moveToHand(Card entity)
        {
            LogicManager.instance.Player.mana += entity.Cardinfo.mana;
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
            Debug.Log("Battle enter");
            anim = entity.gameObject.GetComponentInChildren<Animator>();
            Debug.Log(entity.Cardinfo.cooltime);
        }
        public override void update(Card entity)
        {
            if(anim != null && entity.AttackOrder)
            {
                Debug.Log("Attack!");
                entity.AttackOrder = false;
                anim.SetTrigger("TriggerAttack");
            }
        }
        public override void mouseUp(Card entity)
        {
            Debug.Log("Setting MouseUp");
        }
        public override void mouseDown(Card entity)
        {
            Debug.Log("Setting MouseDown");
        }
        public override void mouseDrag(Card entity)
        {
        }
        public override void exit(Card entity)
        {
            Debug.Log("Setting Exit");
        }
    }
    public class Hand : CardState<Card>
    {
        public override void enter(Card entity)
        {
            Debug.Log("Hand enter");
        }
        public override void update(Card entity)
        {
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
        }
    }
}
