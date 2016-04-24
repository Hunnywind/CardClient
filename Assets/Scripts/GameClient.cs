using UnityEngine;
using System.Collections;
using Nettention.Proud;

public class GameClient : MonoBehaviour
{
    string m_serverAddr = "localhost";
    string m_groupName = "Group";
    string m_loginButtonText = "Connect";
    string m_failMessage = "";

    NetClient m_netClient = new NetClient();

    C2S.Proxy m_C2SProxy = new C2S.Proxy();
    S2C.Stub m_S2CStub = new S2C.Stub();

    enum State
    {
        Stanby,
        Conneting,
        LoggingOn,
        InGroup,
        Failed,
    }
    State m_state = State.Stanby;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_netClient.FrameMove();
    }

    public void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 70), "Dual Manager");
        GUI.Label(new Rect(10, 60, 180, 30), "Server Address");
        m_serverAddr = GUI.TextField(new Rect(10, 80, 180, 30), m_serverAddr);
        GUI.Label(new Rect(10, 110, 180, 30), "Group Name");
        m_groupName = GUI.TextField(new Rect(10, 130, 180, 30), m_groupName);

        if(GUI.Button(new Rect(10, 190, 100, 30), m_loginButtonText))
        {
            if (m_state == State.Stanby)
            {
                m_state = State.Conneting;
                m_loginButtonText = "Connecting...";
                IssueConnect();
            }
        }
    }
    private void IssueConnect()
    {
        m_netClient.AttachProxy(m_C2SProxy);
        m_netClient.AttachStub(m_S2CStub);

        m_netClient.JoinServerCompleteHandler = (ErrorInfo info, ByteArray replyFromServer) =>
        {
            if (info.errorType == ErrorType.ErrorType_Ok)
            {
                m_state = State.LoggingOn;
                m_loginButtonText = "Logging On...";
                //m_C2SProxy.RequestLogon(HostID.HostID_Server, RmiContext.ReliableSend, m_GroupName, false);
            }
            else
            {
                m_state = State.Failed;
                m_loginButtonText = "FAIL!";
                m_failMessage = info.ToString();
            }
        };
        m_netClient.LeaveServerHandler = (ErrorInfo info) =>
        {
            m_state = State.Failed;
            m_failMessage = "Disconnected from server: " + info.ToString();
        };

        NetConnectionParam cp = new NetConnectionParam();
        //cp.serverIP = "112.166.83.16";
        //cp.clientAddrAtServer = "112.166.83.16";
        cp.serverIP = m_serverAddr;
        cp.clientAddrAtServer = m_serverAddr;
        cp.serverPort = 15001;
        cp.protocolVersion = new Nettention.Proud.Guid("{0x875d452,0xc512,0x4848,{0x98,0xd1,0xd4,0xf0,0xfa,0x86,0x69,0x11}}");

        m_netClient.Connect(cp);
    }

}
