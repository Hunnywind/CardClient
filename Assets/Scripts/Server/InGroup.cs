using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nettention.Proud;
using GameItem;

partial class GameClient : MonoBehaviour
{
    private HostID m_myP2PGroupID = HostID.HostID_None;
    private HostID m_enemyID = HostID.HostID_None;

    public void FirstSetting()
    {
        m_C2CProxy.Init(m_myP2PGroupID, RmiContext.ReliableSend, (int)m_myhostID);
    } 
    public void Ready()
    {
        m_C2CProxy.SettingOK(m_myP2PGroupID, RmiContext.ReliableSend);
    }
    public void ClearCard()
    {
        m_C2CProxy.ClearInfo(m_myP2PGroupID, RmiContext.ReliableSend);
    }
    public void SendRandInfo(bool[] ran)
    {
        m_C2CProxy.RandomInfo(m_myP2PGroupID, RmiContext.ReliableSend, ran[0], ran[1], ran[2], ran[3], ran[4]);
    }
    public void SendCardInfo(CardInfo_send info)
    {
        if (LogicManager.instance.PresentLevel == Level.Init ||
            LogicManager.instance.PresentLevel == Level.Init_Wait)
        {
            m_C2CProxy.CardInfo(m_myP2PGroupID, RmiContext.ReliableSend, info);
        }
        else if (LogicManager.instance.PresentLevel == Level.Summon ||
            LogicManager.instance.PresentLevel == Level.Summon_Wait)
        {
            m_C2CProxy.SummonInfo(m_myP2PGroupID, RmiContext.ReliableSend, info);
        }
    }
    public void SendCardCount(int num)
    {
        m_C2CProxy.HandCardCount(m_myP2PGroupID, RmiContext.ReliableSend, num);
    }
    public void SendReturnCard(int fieldNum)
    {
        m_C2CProxy.ReturnInfo(m_myP2PGroupID, RmiContext.ReliableSend, fieldNum);
    }
    public void SendInitCard(int cardNum)
    {
        m_C2CProxy.InitInfo(m_myP2PGroupID, RmiContext.ReliableSend, cardNum);
    }
    private void SetP2PRmiStub()
    {
        m_C2CStub.Init = (Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext, int hostNum) =>
        {
            if (m_myhostID == remote) return true;
            m_enemyID = remote;
            if((int)m_myhostID > (int)m_enemyID)
            {
                RandomSelecter.m_isMyTurn = true;
            }
            else
            {
                RandomSelecter.m_isMyTurn = false;
            }
            return true;
        };
        m_C2CStub.HandCardCount = (Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext, int num) =>
        {
            if (m_myhostID == remote) return true;

            LogicManager.instance.Enemy.m_cardNum = num;
            Debug.Log("Enemy Card Num: " + num);
            return true;
        };
        m_C2CStub.SettingOK = (Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext) =>
        {
            if(m_myhostID != remote)
                LogicManager.instance.EnemySettingEnd();
            return true;
        };
        m_C2CStub.CardInfo = (Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext, CardInfo_send info) =>
        {
            if (m_myhostID == remote) return true;
            LogicManager.instance.EnemyInfoUpdate(info);
            return true;
        };
        m_C2CStub.SummonInfo = (Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext, CardInfo_send info) =>
        {
            if (m_myhostID == remote) return true;
            LogicManager.instance.EnemySummonInfoUpdate(info);
            return true;
        };
        m_C2CStub.ClearInfo = (Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext) =>
        {
            if (m_myhostID != remote)
                LogicManager.instance.ClearInfo();
            return true;
        };
        m_C2CStub.ReturnInfo = (Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext, int fieldNum) =>
        {
            if (m_myhostID != remote)
            {
                LogicManager.instance.EnemyReturnInfoUpdate(fieldNum);
            }
            return true;
        };
        m_C2CStub.RandomInfo = (Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext, bool ran1, bool ran2, bool ran3, bool ran4, bool ran5) =>
        {
            bool[] ranValue = { ran1, ran2, ran3, ran4, ran5 };
            RandomSelecter.ReceiveValue(ranValue);
            return true;
        };
        m_C2CStub.InitInfo = (Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext, int cardNum) =>
        {
            if (m_myhostID != remote)
            {
                CardDatabase.Instance().AddEnemyDeck(cardNum);
            }
            return true;
        };
    }
}
