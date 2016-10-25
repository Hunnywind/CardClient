using UnityEngine;
using System.Collections;
using CardStates;
using CardPositions;
using GameItem;

public class Card : MonoBehaviour
{
    private CardStateMachine<Card> stateMachine = new CardStateMachine<Card>();
    private CardPositionMachine<Card> positionMachine = new CardPositionMachine<Card>();

    private int fieldNumber = 0;
    public CardInfo cardinfo;
    private bool attackReady;
    private bool attackOrder;
    private bool damaged;
    private CardInfo_send info;
    [SerializeField]
    private bool isEnemyCard = false;
    public bool m_isReturn = false;

    public bool m_lock;

    [SerializeField]
    private GameObject m_cardImage;


    public bool AttackOrder
    {
        get { return attackOrder; }
        set
        {
            if(value)
            {
                attackReady = false;
            }
            attackOrder = value;
        }
    }
    public bool Damaged
    {
        get { return damaged; }
        set { damaged = value; }
    }
    public bool Damaged
    {
        get { return damaged; }
        set { damaged = value; }
    }
    public int FieldNumber
    {
        get { return fieldNumber; }
        set
        {
            if(value < 6)
<<<<<<< HEAD
                fieldNumber = value;
=======
            fieldNumber = value;
>>>>>>> 70ebc7505a8a6384034b9b65e7ebfab1be2633a1
        }
    }
    public CardInfo Cardinfo
    {
        get { return cardinfo; }
    }
    public bool IsEnemyCard
    {
        get { return isEnemyCard; }
    }
    public bool IsAttackReady
    {
        get { return attackReady; }
    }


    void Awake()
    {
        gameObject.GetComponentInParent<ObjectPool>().AddObject(gameObject);
        m_lock = false;
        m_isReturn = false;
    }

    void Start()
    {
    }
    void Update()
    {
        stateMachine.Update();
        positionMachine.Update();
    }
    public void Init()
    {
        stateMachine.Init(this, new Setting());
        positionMachine.Init(this, new InHand());
    }
    public void ChangeState(CardState<Card> state)
    {
        stateMachine.ChangeState(state);
        
    }
    public void ChangePosition(CardPosition<Card> position)
    {
        positionMachine.ChangeState(position);
    }
<<<<<<< HEAD
    public void ChangePosition(CardPosition<Card> position)
    {
        positionMachine.ChangeState(position);
    }
=======
>>>>>>> 70ebc7505a8a6384034b9b65e7ebfab1be2633a1
    public string GetCurrentStateName()
    {
        return stateMachine.Get_CurrentState().GetStateName();
    }
    public string GetCurrentPositionName()
    {
        return positionMachine.Get_CurrentState().GetPosition();
    }
<<<<<<< HEAD
=======

>>>>>>> 70ebc7505a8a6384034b9b65e7ebfab1be2633a1
    public void OnMouseDown()
    {
        positionMachine.MouseDown();
    }
    public void OnMouseDrag()
    {
        positionMachine.MouseDrag();
    }
    public void OnMouseUp()
    {
        positionMachine.MouseUp();
    }
    public void TurnStart()
    {
        if (fieldNumber == 0) return;

        //Debug.Log("Is Enemy: " + isEnemyCard);
        //Debug.Log("CoolTime: " + cardinfo.cooltime);
        cardinfo.leftcooltime--;
        //Debug.Log("LeftCoolTime: " + cardinfo.leftcooltime);
        if (cardinfo.leftcooltime <= 0)
        {
            cardinfo.leftcooltime = cardinfo.cooltime;
            attackReady = true;
        }
    }
    public void AttackReady()
    {
<<<<<<< HEAD
        if (attackReady)
        {
            if (!isEnemyCard)
                LogicManager.instance.CardPlayerAdd(gameObject);
            else
                LogicManager.instance.CardEnemyAdd(gameObject);
        }
    }
    public void InfoSend()
    {
=======
>>>>>>> 70ebc7505a8a6384034b9b65e7ebfab1be2633a1
        //if (fieldNumber == 0)
        //{
        //    info.cardLocation = (int)CardLocation.HAND;
        //}
        //else
        //{
        //    info.cardLocation = (int)CardLocation.FIELD;
        //    info.FieldLocation = fieldNumber;
        //}
        
        info.FieldLocation = fieldNumber;
        info.cooltime = cardinfo.cooltime;
        info.leftcooltime = cardinfo.leftcooltime;
        info.isReturn = m_isReturn;
        info.isEnemyCard = true;
        GameClient.instance.SendCardInfo(info);
        info.isEnemyCard = false;
    }
    public void Attacked(int dmg)
    {
<<<<<<< HEAD
=======
        
>>>>>>> 70ebc7505a8a6384034b9b65e7ebfab1be2633a1
        damaged = true;
    }
    public void SetInfo(CardInfo_send pinfo)
    {
        isEnemyCard = pinfo.isEnemyCard;
        info = pinfo;
        cardinfo.attack = CardDatabase.Instance().GetCardData(info.number).attack;
        cardinfo.a_type = CardDatabase.Instance().GetCardData(info.number).a_type;
<<<<<<< HEAD
        cardinfo.leftcooltime = info.leftcooltime;
        //cardinfo.mana = info.mana;
=======
        cardinfo.mana = CardDatabase.Instance().GetCardData(info.number).mana;
        cardinfo.leftcooltime = info.leftcooltime;
>>>>>>> 70ebc7505a8a6384034b9b65e7ebfab1be2633a1
        cardinfo.cooltime = info.cooltime;
        fieldNumber = info.FieldLocation;
        int imageNum = CardDatabase.Instance().GetCardData(info.number).picture;
        Texture2D s = Resources.Load(CardDatabase.Instance().GetImageFileName(imageNum)) as Texture2D;
        Rect r = new Rect(0, 0, s.width, s.height);
        m_cardImage.GetComponent<SpriteRenderer>().sprite = Sprite.Create(s, r, new Vector2(0.5f, 0.5f));
        if (pinfo.isReturn)
        {
            LogicManager.instance.AddReturnCard(this);
        }
    }
}
