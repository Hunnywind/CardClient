using UnityEngine;
using System.Collections;

public class Mana : MonoBehaviour {
    
	void Awake () {
        gameObject.GetComponentInParent<ObjectPool>().AddObject(gameObject);
    }
}
