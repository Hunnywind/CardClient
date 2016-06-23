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

    public void Ready()
    {
        m_C2CProxy.SettingOK(m_myP2PGroupID, RmiContext.ReliableSend);
    }
    public void ClearCard()
    {
        m_C2CProxy.ClearInfo(m_myP2PGroupID, RmiContext.ReliableSend);
    }
    public void SendCardInfo(CardInfo_send info)
    {
        m_C2CProxy.CardInfo(m_myP2PGroupID, RmiContext.ReliableSend, info);
    }
    private void SetP2PRmiStub()
    {
        m_C2CStub.SettingOK = (Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext) =>
        {
            if(m_myhostID != remote)
                LogicManager.instance.EnemySettingEnd();
            return true;
        };
        m_C2CStub.CardInfo = (Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext, CardInfo_send info) =>
        {
            if (m_myhostID != remote)
                LogicManager.instance.EnemyInfoUpdate(info);
            return true;
        };
        m_C2CStub.ClearInfo = (Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext) =>
        {
            if (m_myhostID != remote)
                LogicManager.instance.ClearInfo();
            return true;
        };
    }
}
