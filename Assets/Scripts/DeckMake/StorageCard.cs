using UnityEngine;
using System.Collections;
using GameItem;

public class StorageCard : MonoBehaviour
{
    private CardData m_cardData;

    private string m_position = "LIST";

    [SerializeField]
    private Sprite m_cardSprite;
    [SerializeField]
    private Sprite m_deckSprite;

    [SerializeField]
    private GameObject m_cardImage;
    [SerializeField]
    private GameObject m_name;
    [SerializeField]
    private GameObject m_attack;
    [SerializeField]
    private GameObject m_speed;
    [SerializeField]
    private GameObject m_health;
    [SerializeField]
    private GameObject m_mana;

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
            ToList();
        }
        m_name.GetComponent<MeshRenderer>().sortingOrder = 99;
        m_attack.GetComponent<MeshRenderer>().sortingOrder = 98;
        m_speed.GetComponent<MeshRenderer>().sortingOrder = 97;
        m_health.GetComponent<MeshRenderer>().sortingOrder = 96;
        m_mana.GetComponent<MeshRenderer>().sortingOrder = 95;
    }
    public void AddDeck()
    {
        CardDatabase.Instance().AddPlayerDeck(m_cardData.number);
    }
    public void SetData(CardData data)
    {
        m_cardData = data;
        m_name.GetComponent<TextMesh>().text = data.name;
        m_attack.GetComponent<TextMesh>().text = data.attack.ToString();
        m_speed.GetComponent<TextMesh>().text = data.speed.ToString();
        m_health.GetComponent<TextMesh>().text = data.health.ToString();
        m_mana.GetComponent<TextMesh>().text = data.mana.ToString();

        //Texture2D s = Resources.Load(CardDatabase.Instance().GetImageFileName(m_cardData.picture)) as Texture2D;
        //Rect r = new Rect(0, 0, s.width, s.height);
        //m_cardImage.GetComponent<SpriteRenderer>().sprite = Sprite.Create(s, r, new Vector2(0.5f, 0.5f));
        
    }
    public void OnMouseDown()
    {
        if(m_position.Equals("DECK"))
        {
            DeckManager.instance.MoveToList(gameObject);
            DeckManager.instance.Cost = -m_cardData.mana;
            ToList();
        }
        else
        {
            DeckManager.instance.MoveToDeck(gameObject);
            DeckManager.instance.Cost = m_cardData.mana;
            ToDeck();
        }
    }
    private void ToDeck()
    {
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(3.8f, 0.5f);
        m_mana.SetActive(true);
        m_name.GetComponent<TextMesh>().anchor = TextAnchor.MiddleLeft;
        //m_name.transform.position = new Vector3(-1.2f, 0, 0);
        m_name.transform.localPosition = new Vector3(-1.2f, 0, 0);
        m_attack.SetActive(false);
        m_speed.SetActive(false);
        m_health.SetActive(false);
        m_cardImage.GetComponent<SpriteRenderer>().sprite = m_deckSprite;
        m_position = "DECK";
    }
    private void ToList()
    {
        m_name.GetComponent<TextMesh>().anchor = TextAnchor.MiddleCenter;
        m_mana.SetActive(false);
        m_name.transform.localPosition = new Vector3(0, 0.8f, 0);
        m_attack.SetActive(true);
        m_speed.SetActive(true);
        m_health.SetActive(true);
        m_cardImage.GetComponent<SpriteRenderer>().sprite = m_cardSprite;
        m_position = "LIST";
    }
}
