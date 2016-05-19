using UnityEngine;
using System.Collections;

public class Card_infield : Card {

    public GameObject field;
    [SerializeField]
    private Animator anim;

    private Vector3 Offset = new Vector3(0, 0, 0);

    // Use this for initialization
    override protected void Start()
    {
        
    }
    public void Init()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        if (anim == null) Debug.Log("Can't find animator");
        Offset.x = (field.transform.gameObject.GetComponent<SpriteRenderer>().sprite.texture.width
            - gameObject.GetComponentInChildren<SpriteRenderer>().sprite.texture.width)
            * 0.005f;

        Offset.y = (field.transform.gameObject.GetComponent<SpriteRenderer>().sprite.texture.height
            - gameObject.GetComponentInChildren<SpriteRenderer>().sprite.texture.height)
            * 0.005f;
    }
    // Update is called once per frame
    override protected void Update()
    {
        if (field == null) return;
        gameObject.transform.position = field.transform.position + Offset;

    }

    public void TurnStart()
    {
        leftCooltime--;
        if(leftCooltime <= 0)
        {
            leftCooltime = cooltime;
            LogicManager.instance.CardAdd(gameObject);
        }
    }

    public void Active()
    {
        string attack;
        
        attack = "필드번호 " + fieldNumber + " 쿨타임 : " + cooltime 
            + " 코스트 : " + mana + " " + cardName + " 의 공격!";
        Debug.Log(attack);
        anim.SetTrigger("TriggerAttack");
    }

    // temp
    public void EnemyCard()
    {
        anim.runtimeAnimatorController = 
            GameObject.Find("Enemy").GetComponent<Animator>().runtimeAnimatorController;
    }
}
