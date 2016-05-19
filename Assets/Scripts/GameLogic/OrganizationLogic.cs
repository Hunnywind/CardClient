using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using GameItem;

partial class LogicManager : MonoBehaviour
{
    private void OrganiSetting()
    {
        OrganiGUISetting();
    }
    private void OrganiGUISetting()
    {
        turnText.text = "Select cards to turn in hand.";
        turnText.gameObject.SetActive(true);
        manaText.gameObject.SetActive(true);
        confirmButton.gameObject.SetActive(true);

    }
}
