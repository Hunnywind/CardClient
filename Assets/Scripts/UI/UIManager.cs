﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static UIManager instance;

    [SerializeField]
    private GameObject m_card;

    [SerializeField]
    private Vector2 m_cardSize;
    [SerializeField]
    private float m_middleBoxSize;
    [SerializeField]
    private Vector2 m_middleBoxStartPosition;
    [SerializeField]
    private float m_revision;

    [SerializeField]
    private GameObject m_turnText;
    [SerializeField]
    private GameObject m_turnImage;

    private Text m_mainText;

    private GameObject m_optionButton;

    private GameObject m_statusP;
    private GameObject m_statusE;

    private GameObject m_playerMaster;
    private GameObject m_enemyMaster;

    private Text m_playerName;
    private Text m_enemyName;

    private Scrollbar m_playerHPBar;
    private Scrollbar m_enemyHPBar;

    private Text m_playerHealth;
    private Text m_enemyHealth;

    private GameObject m_playerBar;
    private GameObject m_enemyBar;

    private Text m_gameEndText;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        m_mainText = GameObject.Find("SettingText").GetComponent<Text>();
        //m_optionButton = GameObject.Find("OptionButton");
        //m_statusP = GameObject.Find("P_Status");
        //m_statusE = GameObject.Find("E_Status");
        m_playerMaster = GameObject.Find("PlayerPentagram");
        m_enemyMaster = GameObject.Find("EnemyPentagram");
        m_playerHealth = GameObject.Find("PlayerHealth").GetComponent<Text>();
        m_enemyHealth = GameObject.Find("EnemyHealth").GetComponent<Text>();
        m_playerBar = GameObject.Find("PlayerBar");
        m_enemyBar = GameObject.Find("EnemyBar");
        m_gameEndText = GameObject.Find("GameEndText").GetComponent<Text>();

        //m_playerName = GameObject.Find("P_Name").GetComponent<Text>();
        //m_enemyName = GameObject.Find("E_Name").GetComponent<Text>();
        //m_playerHPBar = GameObject.Find("P_HPBar").GetComponent<Scrollbar>();
        //m_enemyHPBar = GameObject.Find("E_HPBar").GetComponent<Scrollbar>();
    }

    void Start () {

        m_cardSize.x = (float)m_card.GetComponentInChildren<SpriteRenderer>().sprite.texture.width / 75f;
        m_cardSize.y = (float)m_card.GetComponentInChildren<SpriteRenderer>().sprite.texture.height / 75f;
    }
    public void SettingText(bool able, string content = null)
    {
        m_mainText.gameObject.SetActive(able);
        m_mainText.text = content;
    }
    public void ShowTurn(bool able)
    {
        m_turnImage.SetActive(able);
        m_turnText.SetActive(able);
    }
    public void ShowOptionButton(bool able)
    {
        //m_optionButton.SetActive(able);
    }
    public void ShowHP(bool isEnemy, int maxHp, int preHp)
    {
        if(isEnemy)
        {
            m_enemyHealth.text = preHp.ToString();
            if (maxHp > preHp)
                m_enemyHealth.color = Color.red;
            else
                m_enemyHealth.color = Color.white;
        }
        else
        {
            m_playerHealth.text = preHp.ToString();
            if (maxHp > preHp)
                m_playerHealth.color = Color.red;
            else
                m_playerHealth.color = Color.white;
        }
    }
    public void SetMasterName(string player, string enemy)
    {
        //m_playerName.text = player;
        //m_enemyName.text = enemy;
    }
    public void ShowMaster(bool able)
    {
        m_playerBar.SetActive(able);
        m_enemyBar.SetActive(able);
        m_playerMaster.SetActive(able);
        m_enemyMaster.SetActive(able);
        m_playerHealth.gameObject.SetActive(able);
        m_enemyHealth.gameObject.SetActive(able);
    }
    public void DamagedMaster(bool isEnemy)
    {
        if (isEnemy) m_enemyBar.GetComponent<Animator>().SetTrigger("Damaged");
        else m_playerBar.GetComponent<Animator>().SetTrigger("Damaged");
    }
    public void ShowStatus(bool able)
    {
        //m_statusP.SetActive(able);
        //m_statusE.SetActive(able);
    }
    public float GetCardPosition(int size, int count)
    {
        if (m_middleBoxSize > size * m_cardSize.x)
        {
            return (-(size - 1) * m_cardSize.x * 0.5f + (count) * m_cardSize.x + m_revision);
        }
        else
        {
            float interval = (m_middleBoxSize - m_cardSize.x)  / (size-1);
            return m_middleBoxStartPosition.x + m_cardSize.x * 0.5f + (interval * count);

        }
    }
    public void ShowGameEndText(bool able, bool isWin)
    {
        if(able)
        {
            if(isWin)
            {
                m_gameEndText.text = "승리!";
            }
            else
            {
                m_gameEndText.text = "패배!";
            }
        }
        m_gameEndText.gameObject.SetActive(able);
    }
}
