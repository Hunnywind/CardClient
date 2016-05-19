using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using GameItem;

partial class LogicManager : MonoBehaviour
{
    public static LogicManager instance = null;

    public GameObject[] fields;
    public GameObject[] samples;

    public Dictionary<int, Card> summoningCards;

    private Player player;

    

    public Level level = Level.Init;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        summoningCards = new Dictionary<int, Card>();
        FirstSettingInit();
        PlayerSetting();
        FieldSetting();
        CardSetting();
    }

    void Update()
    {
        if (Level.Init == level) FirstSettingUpdate();
    }

}
