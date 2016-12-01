using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Page : MonoBehaviour {
    [SerializeField]
    private Storage cardStorage;

    public bool isNext;
    private bool isAble = true;

    void Update()
    {
        if (!isNext)
        {
            if (cardStorage.PageNum == 0)
            {
                isAble = false;
                gameObject.GetComponent<Image>().color = Color.black;
            }
            else
            {
                isAble = true;
                gameObject.GetComponent<Image>().color = Color.white;
            }
        }
        else
        {
            if((cardStorage.PageNum + 1) * 8 > cardStorage.GetCardNum())
            {
                isAble = false;
                gameObject.GetComponent<Image>().color = Color.black;
            }
            else
            {
                isAble = true;
                gameObject.GetComponent<Image>().color = Color.white;
            }
        }
        
    }
    public void PageClick()
    {
        if (isAble)
        {
            int num = 1;
            if (!isNext) num *= -1;
            cardStorage.PageNum = cardStorage.PageNum + num;
        }
    }
}
