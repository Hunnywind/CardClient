using UnityEngine;
using System.Collections;

public class ManaPool : MonoBehaviour {

    [SerializeField]
    private GameObject[] m_manaPool;
    //[HideInInspector]
    public int m_Pmana;

    void Update()
    {
        for(int i = 0; i < m_manaPool.Length; i++)
        {
            if(m_manaPool[i].activeSelf && i > m_Pmana - 1)
            {
                m_manaPool[i].SetActive(false);
            }
            if(!m_manaPool[i].activeSelf && i <= m_Pmana - 1)
            {
                m_manaPool[i].SetActive(true);
            }
        }
    }
}
