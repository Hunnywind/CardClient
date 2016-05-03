using UnityEngine;
using System.Collections;

public class LogicManager : MonoBehaviour {

    public static LogicManager instance = null;

    public GameObject[] cards_inhand = new GameObject[3];

    public GameObject sample;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }
    void Start()
    {
        CardSetting();
    }
    private void CardSetting()
    {
        for (int i = 0; i < cards_inhand.Length; i++)
        {
            cards_inhand[i] = Instantiate(sample) as GameObject;
        }
    }
}
