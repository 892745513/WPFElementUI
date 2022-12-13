using System;
using System.Collections.Generic;
using System.Text;

namespace CommonFiles
{
    public enum EnumMsgType
    {
        Info, Success, Warning, Error
    }
    public enum EnumMsgBoxType
    {
       Confirm,Input
    }
    public class MsgModel
    {
        public string Msg { get; set; }
        public EnumMsgType MsgType { get; set; }
         
        public static MsgModel Success(string msg)
        {
            return new MsgModel() { MsgType = EnumMsgType.Success, Msg = msg };
        }
        public static MsgModel Info(string msg)
        {
            return new MsgModel() { MsgType = EnumMsgType.Info, Msg = msg };
        }
        public static MsgModel Warning(string msg)
        {
            return new MsgModel() { MsgType = EnumMsgType.Warning, Msg = msg };
        }
        public static MsgModel Error(string msg)
        {
            return new MsgModel() { MsgType = EnumMsgType.Error, Msg = msg };
        }
    }
}
