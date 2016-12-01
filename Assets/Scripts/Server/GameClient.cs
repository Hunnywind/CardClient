using UnityEngine;
using System.Collections;
using Nettention.Proud;
using System;

using GameItem;

public partial class GameClient : MonoBehaviour
{

    public static GameClient instance = null;

    string m_groupName = "Group";

    NetClient m_netClient = new NetClient();

    C2S.Proxy m_C2SProxy = new C2S.Proxy();
    S2C.Stub m_S2CStub = new S2C.Stub();
    C2C.Proxy m_C2CProxy = new C2C.Proxy();
    C2C.Stub m_C2CStub = new C2C.Stub();

    HostID m_myhostID = HostID.HostID_None;

    State m_state = State.Stanby;
    public State GetState
    {
        get { return m_state; }
    }
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {
        m_netClient.P2PMemberJoinHandler = (HostID memberHostID, HostID groupHostID, int memberCount, ByteArray customField) =>
        {
            m_myP2PGroupID = groupHostID;
            
            ServerRoom.instance.MachingComplete();
            m_state = State.MachingComplete;
        };
        m_S2CStub.ReplyLogon = (Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext, int clientID) =>
        {
            m_myhostID = (HostID)clientID;
            CardDatabase.Instance().InitData();
            SetP2PRmiStub();
            return true;
        };
        m_S2CStub.ReplyClientCount = (Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext, int clientCount) =>
        {
            ServerRoom.instance.ClientCount = clientCount;
            return true;
        };
        m_S2CStub.SendCardData = (Nettention.Proud.HostID remote, Nettention.Proud.RmiContext rmiContext, CardData cardData) =>
        {
            CardDatabase.Instance().AddCardData(cardData);
            return true;
        };
    }

    // Update is called once per frame
    void Update()
    {
        m_netClient.FrameMove();
    }
    
    public void Connect()
    {
        if (m_state == State.Stanby)
        {
            m_state = State.Conneting;
            IssueConnect();
        }
        if (m_state == State.LoggingOn)
        {
            m_state = State.MachingWait;
            MachingRequest();
        }
    }
    public void RequestClientCount()
    {
        m_C2SProxy.RequestClientCount(HostID.HostID_Server, RmiContext.ReliableSend);
    }
    private void IssueConnect()
    {
        m_netClient.AttachProxy(m_C2SProxy);
        m_netClient.AttachStub(m_S2CStub);
        m_netClient.AttachProxy(m_C2CProxy);
        m_netClient.AttachStub(m_C2CStub);

        m_netClient.JoinServerCompleteHandler = (ErrorInfo info, ByteArray replyFromServer) =>
        {
            if (info.errorType == ErrorType.ErrorType_Ok)
            {
                m_state = State.LoggingOn;
                ServerRoom.instance.ServerJoinComplete();
                m_C2SProxy.RequestLogon(HostID.HostID_Server, RmiContext.ReliableSend, m_groupName, false);
                m_C2SProxy.RequestClientCount(HostID.HostID_Server, RmiContext.ReliableSend);
                
            }
            else
            {
                m_state = State.Failed;
                //m_failMessage = info.ToString();
            }
        };
        m_netClient.LeaveServerHandler = (ErrorInfo info) =>
        {
            m_state = State.Failed;
            //m_failMessage = "Disconnected from server: " + info.ToString();
        };
        

        NetConnectionParam cp = new NetConnectionParam();
        //cp.serverIP = "127.0.0.1";
        //cp.clientAddrAtServer = "127.0.0.1";
        cp.serverIP = "112.166.83.92";
        cp.clientAddrAtServer = "112.166.83.92";
        //cp.serverIP = m_serverAddr;
        //cp.clientAddrAtServer = m_serverAddr;
        cp.serverPort = 15005;
        cp.protocolVersion = new Nettention.Proud.Guid("{0x342e9077,0x4619,0x466f,{0xa9,0x34,0x1a,0x12,0x9f,0xde,0xa1,0xd}}");


        m_netClient.Connect(cp);
    }
    private void MachingRequest()
    {
        m_C2SProxy.RequestMaching(HostID.HostID_Server, RmiContext.ReliableSend);
    }
    public void OnDestroy()
    {
        m_netClient.Dispose();
    }
}
