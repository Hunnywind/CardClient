using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using GameItem;
using LogicStates;

public class LogicManager : MonoBehaviour
{
    public static LogicManager instance = null;
    public float turnDelay = 2.0f;
    public int presentTurn = 1;

    public GameObject[] fields;
    public GameObject[] enemyfields;

    private Player player;
    private Level presentlevel = Level.Init;

    private LogicStateMachine<LogicManager> stateMachine = new LogicStateMachine<LogicManager>();
    private List<GameObject> turnEnableCards = new List<GameObject>();
    private Text turnText;

    public Level PresentLevel
    {
        get { return presentlevel; }
    }
    public Player Player
    {
        get { return player; }
    }
    public List<GameObject> TurnEnableCards
    {
        get { return turnEnableCards; }
    }
    public Text TurnText
    {
        get { return turnText; }
    }
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        PlayerSetting();
        FieldSetting();
        stateMachine.Init(instance, new SettingLogic());
    }

    void Update()
    {
        stateMachine.Update();
    }
    public void SettingEnd()
    {
        switch (presentlevel)
        {
            case Level.Init:
                stateMachine.ChangeState(new BattleLogic());
                turnText = GameObject.Find("TurnText").GetComponent<Text>();
                presentlevel = Level.Battle;
                BattleStart();
                break;
        }
    }
    public void CardAdd(GameObject obj)
    {
        turnEnableCards.Add(obj);
    }
    public void FieldColliderSet(int num, bool enable)
    {
        fields[num-1].GetComponent<BoxCollider2D>().enabled = enable;
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

    public void BattleStart()
    {
        StartCoroutine(TurnStart());
    }
    IEnumerator TurnStart()
    {
        turnText.gameObject.SetActive(true);
        turnText.text = "Turn " + presentTurn;
        yield return new WaitForSeconds(1f);

        turnText.gameObject.SetActive(false);
        player.mana++;
        foreach(GameObject card in player.cards)
        {
            card.GetComponent<Card>().TurnStart();
        }
        turnEnableCards.Sort(new CardTurnSort());
        Debug.Log("Sort Complete");
        foreach (GameObject obj in turnEnableCards)
        {
            yield return new WaitForSeconds(turnDelay);
            obj.GetComponent<Card>().AttackOrder = true;
        }
        yield return new WaitForSeconds(turnDelay);
        presentTurn++;
        turnEnableCards.Clear();
        if (presentTurn < 7)
            StartCoroutine(TurnStart());
    }
}