﻿




// Generated by PIDL compiler.
// Do not modify this file, but modify the source .pidl file.

using System;
using System.Net;
using GameItem;

namespace C2C
{
	internal class Proxy:Nettention.Proud.RmiProxy
	{
public bool SettingOK(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
		__msg.SimplePacketMode = core.IsSimplePacketMode();
		Nettention.Proud.RmiID __msgid= Common.SettingOK;
		__msg.Write(__msgid);
		
	Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
	__list[0] = remote;
		
	return RmiSend(__list,rmiContext,__msg,
		RmiName_SettingOK, Common.SettingOK);
}

public bool SettingOK(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
__msg.SimplePacketMode = core.IsSimplePacketMode();
Nettention.Proud.RmiID __msgid= Common.SettingOK;
__msg.Write(__msgid);
		
	return RmiSend(remotes,rmiContext,__msg,
		RmiName_SettingOK, Common.SettingOK);
}
public bool CardInfo(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, CardInfo_send info)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
		__msg.SimplePacketMode = core.IsSimplePacketMode();
		Nettention.Proud.RmiID __msgid= Common.CardInfo;
		__msg.Write(__msgid);
		CardClient.Marshaler.Write(__msg, info);
		
	Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
	__list[0] = remote;
		
	return RmiSend(__list,rmiContext,__msg,
		RmiName_CardInfo, Common.CardInfo);
}

public bool CardInfo(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, CardInfo_send info)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
__msg.SimplePacketMode = core.IsSimplePacketMode();
Nettention.Proud.RmiID __msgid= Common.CardInfo;
__msg.Write(__msgid);
CardClient.Marshaler.Write(__msg, info);
		
	return RmiSend(remotes,rmiContext,__msg,
		RmiName_CardInfo, Common.CardInfo);
}
public bool ClearInfo(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
		__msg.SimplePacketMode = core.IsSimplePacketMode();
		Nettention.Proud.RmiID __msgid= Common.ClearInfo;
		__msg.Write(__msgid);
		
	Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
	__list[0] = remote;
		
	return RmiSend(__list,rmiContext,__msg,
		RmiName_ClearInfo, Common.ClearInfo);
}

public bool ClearInfo(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
__msg.SimplePacketMode = core.IsSimplePacketMode();
Nettention.Proud.RmiID __msgid= Common.ClearInfo;
__msg.Write(__msgid);
		
	return RmiSend(remotes,rmiContext,__msg,
		RmiName_ClearInfo, Common.ClearInfo);
}
public bool Init(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int hostNum)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
		__msg.SimplePacketMode = core.IsSimplePacketMode();
		Nettention.Proud.RmiID __msgid= Common.Init;
		__msg.Write(__msgid);
		CardClient.Marshaler.Write(__msg, hostNum);
		
	Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
	__list[0] = remote;
		
	return RmiSend(__list,rmiContext,__msg,
		RmiName_Init, Common.Init);
}

public bool Init(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, int hostNum)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
__msg.SimplePacketMode = core.IsSimplePacketMode();
Nettention.Proud.RmiID __msgid= Common.Init;
__msg.Write(__msgid);
CardClient.Marshaler.Write(__msg, hostNum);
		
	return RmiSend(remotes,rmiContext,__msg,
		RmiName_Init, Common.Init);
}
public bool HandCardCount(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int num)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
		__msg.SimplePacketMode = core.IsSimplePacketMode();
		Nettention.Proud.RmiID __msgid= Common.HandCardCount;
		__msg.Write(__msgid);
		CardClient.Marshaler.Write(__msg, num);
		
	Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
	__list[0] = remote;
		
	return RmiSend(__list,rmiContext,__msg,
		RmiName_HandCardCount, Common.HandCardCount);
}

public bool HandCardCount(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, int num)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
__msg.SimplePacketMode = core.IsSimplePacketMode();
Nettention.Proud.RmiID __msgid= Common.HandCardCount;
__msg.Write(__msgid);
CardClient.Marshaler.Write(__msg, num);
		
	return RmiSend(remotes,rmiContext,__msg,
		RmiName_HandCardCount, Common.HandCardCount);
}
public bool ReturnInfo(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int fieldNum)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
		__msg.SimplePacketMode = core.IsSimplePacketMode();
		Nettention.Proud.RmiID __msgid= Common.ReturnInfo;
		__msg.Write(__msgid);
		CardClient.Marshaler.Write(__msg, fieldNum);
		
	Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
	__list[0] = remote;
		
	return RmiSend(__list,rmiContext,__msg,
		RmiName_ReturnInfo, Common.ReturnInfo);
}

public bool ReturnInfo(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, int fieldNum)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
__msg.SimplePacketMode = core.IsSimplePacketMode();
Nettention.Proud.RmiID __msgid= Common.ReturnInfo;
__msg.Write(__msgid);
CardClient.Marshaler.Write(__msg, fieldNum);
		
	return RmiSend(remotes,rmiContext,__msg,
		RmiName_ReturnInfo, Common.ReturnInfo);
}
public bool SummonInfo(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, CardInfo_send info)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
		__msg.SimplePacketMode = core.IsSimplePacketMode();
		Nettention.Proud.RmiID __msgid= Common.SummonInfo;
		__msg.Write(__msgid);
		CardClient.Marshaler.Write(__msg, info);
		
	Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
	__list[0] = remote;
		
	return RmiSend(__list,rmiContext,__msg,
		RmiName_SummonInfo, Common.SummonInfo);
}

public bool SummonInfo(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, CardInfo_send info)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
__msg.SimplePacketMode = core.IsSimplePacketMode();
Nettention.Proud.RmiID __msgid= Common.SummonInfo;
__msg.Write(__msgid);
CardClient.Marshaler.Write(__msg, info);
		
	return RmiSend(remotes,rmiContext,__msg,
		RmiName_SummonInfo, Common.SummonInfo);
}
public bool RandomInfo(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, bool ran1, bool ran2, bool ran3, bool ran4, bool ran5)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
		__msg.SimplePacketMode = core.IsSimplePacketMode();
		Nettention.Proud.RmiID __msgid= Common.RandomInfo;
		__msg.Write(__msgid);
		CardClient.Marshaler.Write(__msg, ran1);
		CardClient.Marshaler.Write(__msg, ran2);
		CardClient.Marshaler.Write(__msg, ran3);
		CardClient.Marshaler.Write(__msg, ran4);
		CardClient.Marshaler.Write(__msg, ran5);
		
	Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
	__list[0] = remote;
		
	return RmiSend(__list,rmiContext,__msg,
		RmiName_RandomInfo, Common.RandomInfo);
}

public bool RandomInfo(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, bool ran1, bool ran2, bool ran3, bool ran4, bool ran5)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
__msg.SimplePacketMode = core.IsSimplePacketMode();
Nettention.Proud.RmiID __msgid= Common.RandomInfo;
__msg.Write(__msgid);
CardClient.Marshaler.Write(__msg, ran1);
CardClient.Marshaler.Write(__msg, ran2);
CardClient.Marshaler.Write(__msg, ran3);
CardClient.Marshaler.Write(__msg, ran4);
CardClient.Marshaler.Write(__msg, ran5);
		
	return RmiSend(remotes,rmiContext,__msg,
		RmiName_RandomInfo, Common.RandomInfo);
}
public bool InitInfo(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, int cardNum)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
		__msg.SimplePacketMode = core.IsSimplePacketMode();
		Nettention.Proud.RmiID __msgid= Common.InitInfo;
		__msg.Write(__msgid);
		CardClient.Marshaler.Write(__msg, cardNum);
		
	Nettention.Proud.HostID[] __list = new Nettention.Proud.HostID[1];
	__list[0] = remote;
		
	return RmiSend(__list,rmiContext,__msg,
		RmiName_InitInfo, Common.InitInfo);
}

public bool InitInfo(Nettention.Proud.HostID[] remotes,Nettention.Proud.RmiContext rmiContext, int cardNum)
{
	Nettention.Proud.Message __msg=new Nettention.Proud.Message();
__msg.SimplePacketMode = core.IsSimplePacketMode();
Nettention.Proud.RmiID __msgid= Common.InitInfo;
__msg.Write(__msgid);
CardClient.Marshaler.Write(__msg, cardNum);
		
	return RmiSend(remotes,rmiContext,__msg,
		RmiName_InitInfo, Common.InitInfo);
}
// RMI name declaration.
// It is the unique pointer that indicates RMI name such as RMI profiler.
const string RmiName_SettingOK="SettingOK";
const string RmiName_CardInfo="CardInfo";
const string RmiName_ClearInfo="ClearInfo";
const string RmiName_Init="Init";
const string RmiName_HandCardCount="HandCardCount";
const string RmiName_ReturnInfo="ReturnInfo";
const string RmiName_SummonInfo="SummonInfo";
const string RmiName_RandomInfo="RandomInfo";
const string RmiName_InitInfo="InitInfo";
       
const string RmiName_First = RmiName_SettingOK;
		public override Nettention.Proud.RmiID[] GetRmiIDList() { return Common.RmiIDList; } 
	}
}

