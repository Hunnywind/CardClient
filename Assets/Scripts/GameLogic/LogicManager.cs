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
    private List<GameObject> manaObjects = new List<GameObject>();

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
        manaText = GameObject.Find("LeftManaText").GetComponent<Text>();
        confirmButton = GameObject.Find("ConfirmButton").GetComponent<Button>();

        PlayerSetting();
        FieldSetting();
        stateMachine.Init(instance, new SettingLogic());
        Debug.Log(Screen.height);
    }

    void Update()
    {
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
                    
                    stateMachine.ChangeState(new BattleLogic());
                    presentlevel = Level.Battle;
                    enemySettingEnd = false;
                    break;
                case Level.Return_Wait:
                    
                    enemySettingEnd = false;
                    StartCoroutine(ReturnEnd());
                    break;
                case Level.Summon_Wait:
                    enemySettingEnd = false;
                    stateMachine.ChangeState(new BattleLogic());
                    presentlevel = Level.Battle;
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
                presentlevel = Level.Init_Wait;
                SendInfo();
                GameClient.instance.Ready();
                break;
            case Level.Return:
                confirmButton.gameObject.SetActive(false);
                presentlevel = Level.Return_Wait;
                GameClient.instance.Ready();
                break;
            case Level.Summon:
                confirmButton.gameObject.SetActive(false);
                presentlevel = Level.Summon_Wait;
                GameClient.instance.Ready();
                break;
        }
    }
    public void CardAdd(GameObject obj)
    {
        turnEnableCards.Add(obj);
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
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
    }
    private void FieldSetting()
    {
        playerFields = GameObject.Find("PlayerField");
        enemyFields = GameObject.Find("EnemyField");
        enemyFields.SetActive(false);
        for(int i = 0; i < 5; i++)
        {
            fields[i] = playerFields.GetComponent<Fields>().GetField(i);
            enemyfields[i] = enemyFields.GetComponent<Fields>().GetField(i);
        }
    }
    public void EnemyInfoUpdate(CardInfo_send info)
    {
        enemy.CardAdd(info);
    }
    public void SendInfo()
    {
        GameClient.instance.ClearCard();
        player.SendInfo();
    }
    public void ClearInfo()
    {
        enemy.CardClear();
    }

}