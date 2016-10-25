using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameItem;
using System.IO;
using UnityEngine;


class CardDatabase
{
    private static CardDatabase s_instance = null;
    public static CardDatabase Instance()
    {
        if (s_instance == null)
        {
            s_instance = new CardDatabase();
            s_instance.InitData();
        }
        return s_instance;
    }

    private Dictionary<int, CardData> m_cardList = new Dictionary<int, CardData>();
    private Dictionary<int, string> m_imageList = new Dictionary<int, string>();
    private List<int> m_playerDeck = new List<int>();
    private List<int> m_enemyDeck = new List<int>();

    public void InitData()
    {
<<<<<<< HEAD
        //StreamReader fp = File.OpenText("./Assets/Resources/image_info.json");
        //string s = fp.ReadToEnd();
=======
>>>>>>> 70ebc7505a8a6384034b9b65e7ebfab1be2633a1
        TextAsset textAsset = Resources.Load<TextAsset>("image_info");
        var loadData = JsonUtility.FromJson<ImageDataList>(textAsset.text);
        for (int i = 0; i < loadData.CardImages.Length; i++)
        {
            m_imageList[loadData.CardImages[i].number] = loadData.CardImages[i].name;
        }
    }
    public void AddCardData(CardData data)
    {
        m_cardList.Add(data.number, data);
    }
    public CardData GetCardData(int num)
    {
        return m_cardList[num];
    }
    public int GetCardCount()
    {
        return m_cardList.Count;
    }
    public string GetImageFileName(int num)
    {
        return m_imageList[num];
    }
    public void ClearPlayerDeck()
    {
        m_playerDeck.Clear();
    }
    public void AddPlayerDeck(int num)
    {
        m_playerDeck.Add(num);
    }
    public int GetPlayerDeckCount()
    {
        return m_playerDeck.Count;
    }
    public int GetPlayerCard(int num)
    {
        return m_playerDeck[num];
    }
    public bool ContainsDeck(int num)
    {
        return m_playerDeck.Contains(num);
    }
    public void SendData()
    {
        
    }
    public void AddEnemyDeck(int num)
    {
        m_enemyDeck.Add(num);
    }
}