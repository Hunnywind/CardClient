using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Storage : MonoBehaviour {

    [SerializeField]
    private Vector2 m_cardStartPosition;
    [SerializeField]
    private float m_blank;
    [SerializeField]
    private bool m_isHorizon;

    private List<GameObject> m_cards = new List<GameObject>();
    private int m_pageNum = 0;

    public int PageNum
    {
        get { return m_pageNum; }
        set { m_pageNum = value; }
    }
    public int GetCardNum()
    {
        return m_cards.Count;
    }
    void Start()
    {
        StartCoroutine(CardArray());
    }

    public void AddCard(GameObject card)
    {
        if(!m_cards.Contains(card))
        {
            m_cards.Add(card);
        }
    }
    public void RemoveCard(GameObject card)
    {
        if(m_cards.Contains(card))
        {
            m_cards.Remove(card);
        }
    }
    public void SaveDeck()
    {
        foreach(var card in m_cards)
        {
            card.GetComponent<StorageCard>().AddDeck();
        }
    }
    IEnumerator CardArray()
    {
        // 덱 4x2로 정렬
        while(true)
        {
            for(int i = 0; i < m_cards.Count; i++)
            {
                m_cards[i].transform.position = new Vector3(100, 0, 0);
            }
            for(int i = m_pageNum * 8; i < m_cards.Count && i < (m_pageNum + 1) * 8; i++)
            {
                if (m_isHorizon)
                {
                    if(i < 4 + m_pageNum * 8 && i >= m_pageNum * 8)
                        m_cards[i].transform.position = m_cardStartPosition + new Vector2((i - m_pageNum * 8) * m_blank, 0);
                    else
                        m_cards[i].transform.position = m_cardStartPosition + new Vector2((i - 4 - m_pageNum * 8) * m_blank, -m_blank - 1);
                }
                else
                    m_cards[i].transform.position = m_cardStartPosition + new Vector2(0, i * m_blank);
            }
            yield return null;
        }
    }
}
