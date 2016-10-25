using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameItem;

public class Participant : MonoBehaviour {

    public int m_cardNum;
    public int m_mana;

    protected int m_MaxHp;
    protected int m_preHp;
    protected string m_name;
    
    public int MaxHP
    {
        get { return m_MaxHp; }
    }
    public int PreHP
    {
        get { return m_preHp; }
    }
    public string Name
    {
        get { return m_name; }
    }

    [HideInInspector]
    public List<GameObject> cards = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> cards_hand = new List<GameObject>();

    [SerializeField]
    protected Vector2 m_size = new Vector2();
    [SerializeField]
    protected Vector2[] m_cardPositions;
    [SerializeField]
    protected float m_interval;

    protected virtual void Awake()
    {

    }
    protected virtual void Start () {
	}
	protected virtual void Update () {
	}
    public virtual void CardArrange()
    {
    }
}
