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
    private int m_preHealth;

    [SerializeField]
    private GameObject m_cardImage;
    [SerializeField]
    private Sprite m_cardSprite;

    // 그래픽
    [SerializeField]
    private GameObject m_manacrystal;
    [SerializeField]
    private TextMesh m_mana;
    [SerializeField]
    private TextMesh m_name;
    [SerializeField]
    private TextMesh m_attack;
    [SerializeField]
    private TextMesh m_speed;
    [SerializeField]
    private TextMesh m_health;
    [SerializeField]
    private GameObject m_renderObj;

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
    public int FieldNumber
    {
        get { return fieldNumber; }
        set
        {
            if(value < 6)
                fieldNumber = value;
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
        //positionMachine.Init(this, new InHand());
        positionMachine.Init(this, null);
    }
    public void ChangeState(CardState<Card> state)
    {
        stateMachine.ChangeState(state);
        
    }
    public void ChangePosition(CardPosition<Card> position)
    {
        positionMachine.ChangeState(position);
    }
    public string GetCurrentStateName()
    {
        return stateMachine.Get_CurrentState().GetStateName();
    }
    public string GetCurrentPositionName()
    {
        return positionMachine.Get_CurrentState().GetPosition();
    }
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
        m_speed.text = cardinfo.leftcooltime.ToString();
        if (cardinfo.leftcooltime <= 0)
        {
            cardinfo.leftcooltime = cardinfo.cooltime;
            m_speed.text = "A";
            m_speed.color = Color.yellow;
            attackReady = true;
        }
    }
    public void AttackReady()
    {
        if (attackReady)
        {
            if (!isEnemyCard)
                LogicManager.instance.CardPlayerAdd(gameObject);
            else
                LogicManager.instance.CardEnemyAdd(gameObject);
        }
    }
    public void AttackComplete()
    {
        m_speed.text = cardinfo.cooltime.ToString();
        m_speed.color = Color.white;
    }
    public void InfoSend()
    {
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
        //damaged = true;
        m_preHealth -= dmg;
        if (m_preHealth < cardinfo.health) m_health.color = Color.red;
        m_health.text = m_preHealth.ToString();
        if (m_preHealth < 1)
        {
            gameObject.GetComponentInChildren<Animator>().SetTrigger("TriggerDestroy");
            CardDestroy();
        }
    }
    public void SetInfo(CardInfo_send pinfo)
    {
        isEnemyCard = pinfo.isEnemyCard;
        info = pinfo;
        cardinfo.attack = CardDatabase.Instance().GetCardData(info.number).attack;
        cardinfo.a_type = CardDatabase.Instance().GetCardData(info.number).a_type;
        cardinfo.leftcooltime = info.leftcooltime;
        cardinfo.mana = CardDatabase.Instance().GetCardData(info.number).mana;
        cardinfo.cardName = CardDatabase.Instance().GetCardData(info.number).name;
        cardinfo.cooltime = info.cooltime;
        cardinfo.health = info.health;
        m_preHealth = info.health;
        fieldNumber = info.FieldLocation;
        m_mana.text = cardinfo.mana.ToString();
        m_name.text = cardinfo.cardName;
        m_attack.text = cardinfo.attack.ToString();
        m_speed.text = cardinfo.cooltime.ToString();
        m_health.text = m_preHealth.ToString();
        m_mana.GetComponent<MeshRenderer>().sortingOrder = 95;
        m_name.GetComponent<MeshRenderer>().sortingOrder = 99;
        m_attack.GetComponent<MeshRenderer>().sortingOrder = 98;
        m_speed.GetComponent<MeshRenderer>().sortingOrder = 97;
        m_health.GetComponent<MeshRenderer>().sortingOrder = 96;
        //int imageNum = CardDatabase.Instance().GetCardData(info.number).picture;
        //Texture2D s = Resources.Load(CardDatabase.Instance().GetImageFileName(imageNum)) as Texture2D;
        //Rect r = new Rect(0, 0, s.width, s.height);
        //m_cardImage.GetComponent<SpriteRenderer>().sprite = Sprite.Create(s, r, new Vector2(0.5f, 0.5f));
        if (pinfo.isReturn)
        {
            LogicManager.instance.AddReturnCard(this);
        }
        
    }
    public void EnemyCardSetting()
    {
        m_name.gameObject.SetActive(false);
        m_attack.gameObject.SetActive(false);
        m_speed.gameObject.SetActive(false);
        m_health.gameObject.SetActive(false);
        m_mana.gameObject.SetActive(false);
        m_manacrystal.gameObject.SetActive(false);
    }

    public void CardTextPositionMove(Vector3 position)
    {
        m_name.transform.position = position + new Vector3(0, 0.8f, 0);
        m_attack.transform.position = position + new Vector3(0, 0.33f, 0);
        m_speed.transform.position = position + new Vector3(0, -0.04f, 0);
        m_health.transform.position = position + new Vector3(0, -0.43f, 0);
        m_mana.transform.position = position + new Vector3(-0.7f, 1, 0);
        m_manacrystal.transform.position = position + new Vector3(-0.7f, 1, -0.5f);
    }
    public void CardTextSet(bool able)
    {
        m_renderObj.SetActive(able);
        m_name.gameObject.SetActive(able);
        m_attack.gameObject.SetActive(able);
        m_speed.gameObject.SetActive(able);
        m_health.gameObject.SetActive(able);
        m_mana.gameObject.SetActive(able);
    }
    public void Heal(bool isFull, int value)
    {
        if (isFull)
            m_preHealth = cardinfo.health;
        else
            m_preHealth += value;

        if (m_preHealth >= cardinfo.health) m_health.color = Color.white;
        m_health.text = m_preHealth.ToString();
    }
    public void CardDestroy()
    {
        if (attackReady)
            LogicManager.instance.CardDestroyAtBattle(this);
        attackReady = false;
        attackOrder = false;
    }
    public void CardDestroyComplete()
    {
        if (isEnemyCard)
        {
            LogicManager.instance.Enemy.cards.Remove(gameObject);
            LogicManager.instance.Enemy.RemoveCardHand(gameObject);
            LogicManager.instance.Enemy.AddTrashCard(info);
        }
        else
        {
            if (fieldNumber > 0 && fieldNumber < 6)
                LogicManager.instance.FieldColliderSet(fieldNumber, true);
            LogicManager.instance.Player.AddTrashCard(info);
            LogicManager.instance.Player.cards.Remove(gameObject);
        }
        m_renderObj.GetComponent<SpriteRenderer>().sprite = m_cardSprite;
        m_attack.color = Color.white;
        m_speed.color = Color.white;
        m_health.color = Color.white;
        fieldNumber = 0;
        gameObject.SetActive(false);
    }
}
