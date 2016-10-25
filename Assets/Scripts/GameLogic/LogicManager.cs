using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using GameItem;
using LogicStates;

public partial class LogicManager : MonoBehaviour
{
    public static LogicManager instance = null;
    
    public int presentTurn = 1;

    [HideInInspector]
    public Text manaText;
    [HideInInspector]
    public Button confirmButton;

    public GameObject[] fields;
    public GameObject[] enemyfields;

    private Player player;
    private Enemy enemy;

    private Level presentlevel = Level.Init;

    private LogicStateMachine<LogicManager> stateMachine = new LogicStateMachine<LogicManager>();
    private ManaPool m_manapool;

    private GameObject playerFields;
    private GameObject enemyFields;

    
    public Level PresentLevel
    {
        get { return presentlevel; }
    }
    public Player Player
    {
        get { return player; }
    }
    public Enemy Enemy
    {
        get { return enemy; }
    }
    public GameObject PlayerFields
    {
        get { return playerFields; }
    }
    public GameObject EnemyFields
    {
        get { return enemyFields; }
    }
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        turnText = GameObject.Find("TurnText").GetComponent<Text>();
        manaText = GameObject.Find("ManaText").GetComponent<Text>();
        confirmButton = GameObject.Find("ConfirmButton").GetComponent<Button>();
        m_manapool = GameObject.Find("ManaTransform").GetComponent<ManaPool>();

        PlayerSetting();
        FieldSetting();
        stateMachine.Init(instance, new SettingLogic());
    }
    void Update()
    {
        m_manapool.m_Pmana = player.m_mana;
        stateMachine.Update();
        
        Update_server();
    }
    void Update_server()
    {
        if(enemySettingEnd)
        {
            switch (presentlevel)
            {
                case Level.Init_Wait:
                    presentlevel = Level.Battle;
                    stateMachine.ChangeState(new BattleLogic());
                    enemySettingEnd = false;
                    break;
                case Level.Return_Wait:
                    enemySettingEnd = false;
                    StartCoroutine(ReturnEnd());
                    break;
                case Level.Summon_Wait:
                    enemySettingEnd = false;
                    StartCoroutine(DoSummon());
                    //stateMachine.ChangeState(new BattleLogic());
                    //presentlevel = Level.Battle;
                    break;
            }
        }
    }
    public void LogicCoroutineStart(Level level)
    {
        switch (level)
        {
            case Level.Battle:
                StartCoroutine(TurnStart());
                break;
            case Level.Return:
                StartCoroutine(ReturnStart());
                break;
            case Level.Summon:
                StartCoroutine(SummonStart());
                break;
        }
    }
    public void SettingEnd()
    {
        switch (presentlevel)
        {
            case Level.Init:
                confirmButton.gameObject.SetActive(false);
                UIManager.instance.SettingText(true, "- 상대편의 선택을 기다리는 중 -");
                presentlevel = Level.Init_Wait;
                SendInfo();
                GameClient.instance.SendCardCount(player.cards_hand.Count);
                GameClient.instance.Ready();
                break;
            case Level.Return:
                confirmButton.gameObject.SetActive(false);
                UIManager.instance.SettingText(true, "- 상대편의 선택을 기다리는 중 -");
                presentlevel = Level.Return_Wait;
                player.SendReturnInfo();
                GameClient.instance.Ready();
                break;
            case Level.Summon:
                confirmButton.gameObject.SetActive(false);
                UIManager.instance.SettingText(true, "- 상대편의 선택을 기다리는 중 -");
                presentlevel = Level.Summon_Wait;
                player.SendSummonInfo();
                GameClient.instance.Ready();
                break;
        }
    }
    public void CardPlayerAdd(GameObject obj)
    {
        turnEnablePlayerCards.Add(obj);
    }
    public void CardEnemyAdd(GameObject obj)
    {
        turnEnableEnemyCards.Add(obj);
    }
    public void FieldColliderSet(int num, bool enable)
    {
        fields[num - 1].GetComponent<BoxCollider2D>().enabled = enable;
    }
    public void ChangeState(LogicState<LogicManager> state)
    {
        stateMachine.ChangeState(state);
    }
    private void PlayerSetting()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        player.Init();
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        UIManager.instance.SetMasterName(player.Name, enemy.Name);
        UIManager.instance.ShowHP(true, enemy.MaxHP, enemy.PreHP);
        UIManager.instance.ShowHP(false, player.MaxHP, player.PreHP);
    }
    private void FieldSetting()
    {
        playerFields = GameObject.Find("PlayerSpace");
        enemyFields = GameObject.Find("EnemySpace");
        enemyFields.SetActive(false);
        for(int i = 0; i < 5; i++)
        {
            fields[i] = playerFields.GetComponentInChildren<Fields>().GetField(i);
            enemyfields[i] = enemyFields.GetComponentInChildren<Fields>().GetField(i);
        }
    }
    
    public void EnemyInfoUpdate(CardInfo_send info)
    {
        enemy.AddCardWait(info);
    }
    public void SendInfo()
    {
        player.SendInfo();
    }
    
    public void SendClearInfo()
    {
        GameClient.instance.ClearCard();
    }
    public void ClearInfo()
    {
        enemy.CardClear();
    }
}