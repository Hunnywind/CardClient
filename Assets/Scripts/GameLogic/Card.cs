using UnityEngine;
using System.Collections;

public class Card : MonoBehaviour
{

    private GameObject dragCard;
    bool inField = false;
    Vector3 initialPosition;
    protected virtual void Start()
    {
        initialPosition = gameObject.transform.position;

        dragCard = new GameObject();
        dragCard.AddComponent<SpriteRenderer>();
        dragCard.gameObject.transform.position = gameObject.transform.position;
        dragCard.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        dragCard.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
    }

    public void OnMouseUp()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        dragCard.GetComponent<SpriteRenderer>().enabled = false;

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        

        if (inField) OnMouseUp_Field(hit);
        else OnMouseUp_Hand(hit);
        
    }
    private void OnMouseUp_Field(RaycastHit2D hit)
    {
        if(!hit || !hit.transform.tag.Equals("Field"))
        {
            inField = false;
            gameObject.transform.position = initialPosition;
        }
    }
    private void OnMouseUp_Hand(RaycastHit2D hit)
    {
        if (!hit) return;
        if (hit.transform.tag.Equals("Field"))
        {
            Vector3 Offset = new Vector3();
            Offset.x = 0.05f;
            Offset.y = 0.05f;
            Offset.z = 0;
            inField = true;
            gameObject.transform.position = hit.transform.position;
            gameObject.transform.position += Offset;
        }
        else
        {
            dragCard.transform.position = gameObject.transform.position;
        }
    }
    IEnumerator OnMouseDrag()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        dragCard.GetComponent<SpriteRenderer>().enabled = true;
        while (Input.GetMouseButton(0))
        {
            Vector3 newPosition = new Vector3();
            newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPosition.x -= (float)(dragCard.GetComponent<SpriteRenderer>().sprite.texture.width * 0.5 * 1 / 100);
            newPosition.y -= (float)(dragCard.GetComponent<SpriteRenderer>().sprite.texture.height * 0.5 * 1 / 100);
            newPosition.z = 0;
            dragCard.transform.position = newPosition;
            yield return null;
        }
    }
}
