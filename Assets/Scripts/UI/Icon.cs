using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Icon : MonoBehaviour {

    [SerializeField]
    private Sprite m_expand;
    [SerializeField]
    private Sprite m_base;
    [SerializeField]
    private GameObject m_info;

    public static int s_number = 0;
    public int m_number;

    private bool m_isShow = false;
    private bool m_isClick = false;

    void Start()
    {
        m_number = s_number;
        s_number++;
    }
    void Update()
    {
        //if(s_number == m_number && !m_isShow && m_isClick)
        //{
        //    gameObject.GetComponent<Image>().sprite = m_expand;
        //    m_info.SetActive(true);
        //    m_isShow = true;
        //}
        //else if((s_number != m_number && m_isShow
        //    )|| !m_isClick)
        //{
        //    m_info.SetActive(false);
        //    gameObject.GetComponent<Image>().sprite = m_base;
        //    m_isShow = false;
        //}

        if (s_number == m_number && !m_isShow
            && m_isClick)
        {
            gameObject.GetComponent<Image>().sprite = m_expand;
            m_info.SetActive(true);
            m_isShow = true;
        }
        else if ((s_number != m_number || !m_isClick) && m_isShow)
        {
            gameObject.GetComponent<Image>().sprite = m_base;
            m_info.SetActive(false);
            m_isShow = false;
            m_isClick = false;
        }
    }

    public void Highlight()
    {
        if (m_isClick) m_isClick = false;
        else
        {
            s_number = m_number;
            m_isClick = true;
        }
    }
}
