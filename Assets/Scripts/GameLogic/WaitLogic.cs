using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using GameItem;
using LogicStates;

public partial class LogicManager : MonoBehaviour
{
    bool enemySettingEnd = false;

    private void SettingWait()
    {

    }
    public void EnemySettingEnd()
    {
        enemySettingEnd = true;
        Debug.Log("Enemy Setting End");
    }
}