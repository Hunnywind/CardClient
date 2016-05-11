using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LogicManager : MonoBehaviour
{
    public static LogicManager instance = null;

    public GameObject[] cards_inhand;
    public GameObject[] fields;
    public GameObject[] samples;

    public List<Card> selectCards;
    public Dictionary<int, Card> summoningCards;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        FieldSetting();
        CardSetting();
    }
    private void FieldSetting()
    {
        Vector3 newPosition = new Vector3();
        int blank = 50;
        for (int i = 0; i < fields.Length; i++)
        {
            fields[i] = Instantiate(samples[1]) as GameObject;
            newPosition.x = (-((float)fields[i].GetComponent<SpriteRenderer>().sprite.texture.width * 1 / 2)
            + ((float)fields[i].GetComponent<SpriteRenderer>().sprite.texture.width + blank) * (i - 2))
            * 1 / 100;
            newPosition.y = 1;
            newPosition.z = 0;
            fields[i].transform.position = newPosition;
        }
    }
    private void CardSetting()
    {
        Vector3 newPosition = new Vector3();
        for (int i = 0; i < cards_inhand.Length; i++)
        {
            cards_inhand[i] = Instantiate(samples[0]) as GameObject;
            newPosition.x = (-(float)Screen.width + 200
                 + ((float)cards_inhand[i].GetComponent<SpriteRenderer>().sprite.texture.width)
                 * i)
                / 100;
            newPosition.y = -2;
            newPosition.z = 0;
            cards_inhand[i].transform.position = newPosition;
        }
    }
}
