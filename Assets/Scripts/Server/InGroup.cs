using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nettention.Proud;

partial class GameClient : MonoBehaviour
{
    private HostID m_myP2PGroupID = HostID.HostID_None;

    public void Ready()
    {
        m_C2CProxy.SettingOK(m_myP2PGroupID, RmiContext.ReliableSend);
    }
    private void SetP2PRmiStub()
    {
        m_C2CStub.SettingOK = (Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext) =>
        {
            if(m_myhostID != remote)
            LogicManager.instance.EnemySettingEnd();
            return true;
        };
    }
}
