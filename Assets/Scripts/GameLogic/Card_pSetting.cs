using UnityEngine;
using System.Collections;

public class Card_pSetting : Card {

    private GameObject dragCard;
    bool inField = false;
    Vector3 initialPosition;
    protected override void Start()
    {
        initialPosition = gameObject.transform.position;

        dragCard = new GameObject();
        dragCard.AddComponent<SpriteRenderer>();
        dragCard.gameObject.transform.position = gameObject.transform.position;
        dragCard.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        dragCard.GetComponent<SpriteRenderer>().enabled = false;

        mana = Random.Range(1, 4);
        cooltime = Random.Range(1, 3);
        leftCooltime = cooltime;
    }
    protected override void Update()
    {
    }
    protected override void OnMouseUp()
    {
        if (enabled)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
            dragCard.GetComponent<SpriteRenderer>().enabled = false;

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (inField) OnMouseUp_Field(hit);
            else OnMouseUp_Hand(hit);
        }

    }
    protected override void OnMouseUp_Field(RaycastHit2D hit)
    {
        if (!hit || !hit.transform.tag.Equals("Field"))
        {
            inField = false;
            gameObject.transform.position = initialPosition;
            LogicManager.instance.RemoveField(this, fieldNumber);
            fieldNumber = 5;
        }
    }
    protected override void OnMouseUp_Hand(RaycastHit2D hit)
    {
        if (!hit) return;
        if (hit.transform.tag.Equals("Field"))
        {
            Vector3 Offset = new Vector3();
            Offset.x = (hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite.texture.width
                - gameObject.GetComponent<SpriteRenderer>().sprite.texture.width)
                * 0.005f;
            Offset.y = (hit.transform.gameObject.GetComponent<SpriteRenderer>().sprite.texture.height
                - gameObject.GetComponent<SpriteRenderer>().sprite.texture.height)
                * 0.005f;
            Offset.z = 0;
            inField = true;
            gameObject.transform.position = hit.transform.position;
            gameObject.transform.position += Offset;
            fieldNumber = hit.transform.gameObject.GetComponent<Field>().number;
            LogicManager.instance.AddField(this, fieldNumber);
        }
        else
        {
            dragCard.transform.position = gameObject.transform.position;
        }
    }
    IEnumerator OnMouseDrag()
    {
        if (enabled)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            dragCard.GetComponent<SpriteRenderer>().enabled = true;
        }
        while (Input.GetMouseButton(0))
        {
            if (enabled)
            {
                Vector3 newPosition = new Vector3();
                newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                newPosition.x -= (float)(dragCard.GetComponent<SpriteRenderer>().sprite.texture.width * 0.5 * 1 / 100);
                newPosition.y -= (float)(dragCard.GetComponent<SpriteRenderer>().sprite.texture.height * 0.5 * 1 / 100);
                newPosition.z = 0;
                dragCard.transform.position = newPosition;
            }
            yield return null;
        }
    }
}
