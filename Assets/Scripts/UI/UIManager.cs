using UnityEngine;
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


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        m_mainText = GameObject.Find("SettingText").GetComponent<Text>();
        m_optionButton = GameObject.Find("OptionButton");
        m_statusP = GameObject.Find("P_Status");
        m_statusE = GameObject.Find("E_Status");
        m_playerMaster = GameObject.Find("PlayerMaster");
        m_enemyMaster = GameObject.Find("EnemyMaster");
        m_playerName = GameObject.Find("P_Name").GetComponent<Text>();
        m_enemyName = GameObject.Find("E_Name").GetComponent<Text>();
        m_playerHPBar = GameObject.Find("P_HPBar").GetComponent<Scrollbar>();
        m_enemyHPBar = GameObject.Find("E_HPBar").GetComponent<Scrollbar>();
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
        m_optionButton.SetActive(able);
    }
    public void ShowHP(bool isEnemy, int maxHp, int preHp)
    {
        if(isEnemy)
        {
            m_enemyHPBar.size = (float)maxHp / (float)preHp;
        }
        else
        {
            m_playerHPBar.size = (float)maxHp / (float)preHp;
        }
    }
    public void SetMasterName(string player, string enemy)
    {
        m_playerName.text = player;
        m_enemyName.text = enemy;
    }
    public void ShowMaster(bool able)
    {
        m_playerMaster.SetActive(able);
        m_enemyMaster.SetActive(able);
    }
    public void ShowStatus(bool able)
    {
        m_statusP.SetActive(able);
        m_statusE.SetActive(able);
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
}
