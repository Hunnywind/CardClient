using UnityEngine;
using System.Collections;
using GameItem;

public class StorageCard : MonoBehaviour
{
    private CardData m_cardData;

    private string m_position = "LIST";

    [SerializeField]
    private GameObject m_cardImage;
    [SerializeField]
    private GameObject m_name;
    [SerializeField]
    private GameObject m_attack;
    [SerializeField]
    private GameObject m_speed;

    void Start()
    {
        if (!CardDatabase.Instance().ContainsDeck(m_cardData.number))
        {
            DeckManager.instance.MoveToList(gameObject);
            m_position = "LIST";
        }
        else
        {
            DeckManager.instance.MoveToDeck(gameObject);
            DeckManager.instance.Cost = m_cardData.mana;
            m_position = "DECK";
        }
        m_name.GetComponent<MeshRenderer>().sortingOrder = 99;
        m_attack.GetComponent<MeshRenderer>().sortingOrder = 98;
        m_speed.GetComponent<MeshRenderer>().sortingOrder = 97;
        
    }
    public void AddDeck()
    {
        CardDatabase.Instance().AddPlayerDeck(m_cardData.number);
    }
    public void SetData(CardData data)
    {
        m_cardData = data;
        Texture2D s = Resources.Load(CardDatabase.Instance().GetImageFileName(m_cardData.picture)) as Texture2D;
        Rect r = new Rect(0, 0, s.width, s.height);
        m_cardImage.GetComponent<SpriteRenderer>().sprite = Sprite.Create(s, r, new Vector2(0.5f, 0.5f));
        
    }
    public void OnMouseDown()
    {
        if(m_position.Equals("DECK"))
        {
            DeckManager.instance.MoveToList(gameObject);
            DeckManager.instance.Cost = -m_cardData.mana;
            m_position = "LIST";
        }
        else
        {
            DeckManager.instance.MoveToDeck(gameObject);
            DeckManager.instance.Cost = m_cardData.mana;
            m_position = "DECK";
        }
    }

}
