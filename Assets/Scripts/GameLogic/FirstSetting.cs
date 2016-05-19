using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameItem;

partial class LogicManager : MonoBehaviour
{
    private Button confirmButton;
    private Text manaText;

    private void FirstSettingInit()
    {
        confirmButton = GameObject.Find("ConfirmButton").GetComponent<Button>();
        manaText = GameObject.Find("LeftManaText").GetComponent<Text>();
        turnText = GameObject.Find("TurnText").GetComponent<Text>();
        turnText.gameObject.SetActive(false);
    }
    private void FirstSettingUpdate()
    {
           foreach (GameObject card_hand in player.cards_inHand)
           {
               if (card_hand.GetComponent<Card>().mana > player.mana)
                   card_hand.SetActive(false);
               else
                   card_hand.SetActive(true);
           }
    }
    public void OnGUI()
    {
        manaText.text = "Left Mana : " + player.mana;
    }
    private void PlayerSetting()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null) Debug.Log("Can't find player");
    }
    private void FieldSetting()
    {
        Vector3 newPosition = new Vector3();
        int blank = 50;
        float odd_even = (fields.Length % 2 == 0) ? 1 : 0.5f;
        GameObject.Find("FieldPool").
                GetComponent<ObjectPool>().Init();
        for (int i = 0; i < fields.Length; i++)
        {
            fields[i] = GameObject.Find("FieldPool").
                GetComponent<ObjectPool>().GetObject();
            fields[i].GetComponent<Field>().number = i;
            newPosition.x = (-((float)fields[i].GetComponent<SpriteRenderer>().sprite.texture.width * odd_even)
            + ((float)fields[i].GetComponent<SpriteRenderer>().sprite.texture.width + blank) * (i - 2))
            * 1 / 100;
            newPosition.y = 1;
            newPosition.z = 0;
            fields[i].transform.position = newPosition;
        }
    }
    private void CardSetting()
    {
        GameObject.Find("FieldCardPool").GetComponent<ObjectPool>().Init();
        Vector3 newPosition = new Vector3();
        int blank = 30;
        float odd_even = (player.cardNum % 2 == 0) ? 1 : 0.5f;
        for (int i = 0; i < player.cardNum; i++)
        {
            GameObject card = Instantiate(samples[0]) as GameObject;
            card.AddComponent<Card_pSetting>();
            card.GetComponent<Card_pSetting>().cardName = "PlayerCard";
            newPosition.x = (-((float)card.GetComponent<SpriteRenderer>().sprite.texture.width * odd_even)
            + ((float)card.GetComponent<SpriteRenderer>().sprite.texture.width + blank) * (i - 3))
            * 1 / 100;
            newPosition.y = -2;
            newPosition.z = 0;
            card.transform.position = newPosition;
            player.cards.Add(card);
            player.cards_inHand.Add(card);
        }
    }
    public void AddField(Card card, int fieldNumber)
    {
        player.cards_inField.Add(fieldNumber, card.gameObject);
        player.mana -= card.mana;
        player.cards_inHand.Remove(card.gameObject);
    }
    public void RemoveField(Card card, int fieldNumber)
    {
        player.mana += card.mana;
        player.cards_inField.Remove(fieldNumber);
        player.cards_inHand.Add(card.gameObject);
    }
    public void SettingEnd()
    {
        level = Level.Battle;
        for(int i = 0; i < 5; i++)
        {
            fields_enemy[i] = GameObject.Find("FieldPool").
            GetComponent<ObjectPool>().GetObject();
        }
        confirmButton.gameObject.SetActive(false);
        manaText.gameObject.SetActive(false);
        player.SetBattle();
        EnemyBattleSetting();
        BattleSetting();
        
        
    }
}
