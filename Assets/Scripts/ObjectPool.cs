using UnityEngine;
using System.Collections;

public class ObjectPool : MonoBehaviour {
    public GameObject[] objects;

	void Start () {
        //objects = gameObject.GetComponentsInChildren<GameObject>();
        
        
	}
	
    public void Init()
    {
        SetActiveFalse();
    }
    void SetActiveFalse()
    {
        foreach(GameObject obj in objects)
        {
            obj.SetActive(false);
        }
    }

    public GameObject GetObject()
    {
        foreach(GameObject obj in objects)
        {
            if(!obj.activeSelf)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        Debug.Log("Can't add object");
        return null;
    }
}
