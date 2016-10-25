using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Space : MonoBehaviour {

    public bool m_isBattle;
    public bool m_isSetting;
    public bool m_isPrepare;

    [SerializeField]
    private Vector3 m_battleSpace;
    [SerializeField]
    private Vector3 m_settingSpace;
    [SerializeField]
    private Vector3 m_prepareSpace;

    private Vector3 m_objectSpace;
    private Vector3 m_startSpace;

    [SerializeField]
    private float m_speed;
    private float m_timer = 0;

    private bool m_isMovingOn = false;

	void Start ()
    {
        //m_battleSpace = gameObject.transform.position;
	}
	void Update () {
	}
    void FixedUpdate()
    {
        if(m_isSetting)
        {
            m_isSetting = false;
            m_objectSpace = m_settingSpace;
            m_isMovingOn = true;
            m_timer = 0;
            m_startSpace = gameObject.transform.position;
        }
        if(m_isPrepare)
        {
            m_isPrepare = false;
            m_objectSpace = m_prepareSpace;
            m_isMovingOn = true;
            m_timer = 0;
            m_startSpace = gameObject.transform.position;
        }
        if(m_isBattle)
        {
            m_isBattle = false;
            m_objectSpace = m_battleSpace;
            m_isMovingOn = true;
            m_timer = 0;
            m_startSpace = gameObject.transform.position;
        }
        if(m_isMovingOn)
        {
            m_timer += Time.deltaTime;
            gameObject.transform.position = Vector3.Slerp(m_startSpace, m_objectSpace, m_timer);
            if(m_timer >= 1.0f)
            {
                m_isMovingOn = false;
            }
        }
    }
}
