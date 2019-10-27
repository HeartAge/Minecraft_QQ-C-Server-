﻿using Native.Csharp.App;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Color_yr.Minecraft_QQ
{
    class Send
    {
        public static Thread Send_T;
        public static Dictionary<long, string> Send_List = new Dictionary<long, string>();

        public static void Send_()
        {
            while (true)
            {
                if(Send_List.Count != 0)
                {
                    long group = Send_List.Keys.First();
                    foreach (KeyValuePair<long, string> a in Send_List)
                    {
                        string b = null;
                        if (group == a.Key && string.IsNullOrWhiteSpace(a.Value) == false)
                        {
                            b += a.Value + '\n';
                        }
                        Send_List.Remove(a.Key);
                        if (string.IsNullOrWhiteSpace(b) == false)
                        {
                            b = b.Substring(0, b.Length - 1);
                            Common.CqApi.SendGroupMessage(group, b);
                        }
                    }
                }
                Thread.Sleep(admin_config.Send_Tick);
            }
        }
    }
}