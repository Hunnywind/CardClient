using UnityEngine;
using System.Collections;

public class Fields : MonoBehaviour {

    public GameObject[] m_fields;

	public GameObject GetField(int fieldNum)
    {
        return m_fields[fieldNum];
    }
}
