using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeckManager : MonoBehaviour {

    public static DeckManager instance = null;

    private Storage m_deckStorage;
    private Storage m_cardStorage;

    private int m_cost;

    [SerializeField]
    private GameObject m_card;
    [SerializeField]
    private Text m_costText;

    public int Cost
    {
        get
        {
            return m_cost;
        }
        set
        {
            m_cost += value;
            CostUpdate();
        }
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        m_deckStorage = GameObject.Find("DeckList").GetComponent<Storage>();
        m_cardStorage = GameObject.Find("CardList").GetComponent<Storage>();
    }
    void Start()
    {
        Init();
    }
    private void CostUpdate()
    {
        m_costText.text = m_cost + " / 20";
        if(m_cost > 20)
        {
            m_costText.color = Color.red;
        }
        else
        {
            m_costText.color = Color.black;
        }
    }
    public void Init()
    {
        for(int i = 0; i < CardDatabase.Instance().GetCardCount(); i++)
        {
            GameObject card = Instantiate(m_card) as GameObject;
            StorageCard s = card.GetComponent<StorageCard>();
            s.SetData(CardDatabase.Instance().GetCardData(i));
        }
    }
    public void MoveToDeck(GameObject card)
    {
        m_cardStorage.RemoveCard(card);
        m_deckStorage.AddCard(card);
    }
    public void MoveToList(GameObject card)
    {
        m_deckStorage.RemoveCard(card);
        m_cardStorage.AddCard(card);
    }
    public void SaveDeck()
    {
        CardDatabase.Instance().ClearPlayerDeck();
        m_deckStorage.SaveDeck();
    }
}
