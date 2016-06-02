using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {
    private List<GameObject> objects = new List<GameObject>();
	void Start () {
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
    public void AddObject(GameObject obj)
    {
        objects.Add(obj);
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
    public List<GameObject> getObjects()
    {
        return objects;
    }
}
