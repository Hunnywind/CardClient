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
    private Level presentlevel = Level.Init;

    private LogicStateMachine<LogicManager> stateMachine = new LogicStateMachine<LogicManager>();


    public Level PresentLevel
    {
        get { return presentlevel; }
    }
    public Player Player
    {
        get { return player; }
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
            switch(presentlevel)
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
        if (player == null) Debug.Log("Can't find player");
    }
    private void FieldSetting()
    {
        Vector3 newPosition = new Vector3();
        int blank = 50;
        float odd_even = (fields.Length % 2 == 0) ? 1 : 0.5f;
        GameObject.Find("FieldPool").
                GetComponent<ObjectPool>().Init();
        for (int i = 0; i < 5; i++)
        {
            fields[i] = GameObject.Find("FieldPool").
                GetComponent<ObjectPool>().GetObject();
            fields[i].GetComponent<Field>().number = i + 1;

            enemyfields[i] = GameObject.Find("FieldPool").
                GetComponent<ObjectPool>().GetObject();
            enemyfields[i].GetComponent<Field>().number = i + 1;
            enemyfields[i].GetComponent<Field>().IsEnemyField = true;

            newPosition.x = ((-((float)fields[i].GetComponent<SpriteRenderer>().sprite.texture.width * 0.5f + blank * 0.5f) *
                    (fields.Length - 1)) +
                    (((float)fields[i].GetComponent<SpriteRenderer>().sprite.texture.width + blank) * (i)))
                    * 1 / 100;

            newPosition.y = 1;
            newPosition.z = 0;
            fields[i].transform.position = newPosition;
            newPosition.y = 100;
            enemyfields[i].transform.position = newPosition;
        }
    }


}