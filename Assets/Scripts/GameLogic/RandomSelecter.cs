using UnityEngine;
using System.Collections;

public class RandomSelecter : MonoBehaviour {
    private static bool[] m_randomData = new bool[5];
    public static bool m_isMyTurn;

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
    public static void ReceiveValue(bool[] value)
    {
        for(int i = 0; i < 5; i++)
        {
            m_randomData[i] = value[i];
        }
    }
}
