using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TurnRound : MonoBehaviour {
    private RectTransform m_transform;

    void Start()
    {
        m_transform = gameObject.GetComponent<RectTransform>();    
    }
    public void Init()
    {
        m_transform = gameObject.GetComponent<RectTransform>();
    }
    public void TurnEnd()
    {
        m_transform.Rotate(0, 0, -60);
    }
}
