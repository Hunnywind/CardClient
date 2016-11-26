using UnityEngine;
using System.Collections;

public class RandomSelecter : MonoBehaviour {
    private static bool[] m_randomData = new bool[5];
    public static bool m_isMyTurn;
    private static int m_index = 0;
    public static void RandomActive()
    {
        for(int i = 0; i < m_randomData.Length; i++)
        {
            m_randomData[i] = (Random.Range(0, 2) == 0) ? true : false;
        }
    }
    public static bool GetRandomValue(int num)
    {
        return m_randomData[num];
    }
    public static bool GetRandomValue_n()
    {
        bool value = m_randomData[m_index];
        m_index++;
        if (m_index > 4) m_index = 0;
        if (m_isMyTurn)
            return value;
        else
            return !value;
    }
    public static void ReceiveValue(bool[] value)
    {
        for(int i = 0; i < 5; i++)
        {
            m_randomData[i] = value[i];
        }
    }
}
