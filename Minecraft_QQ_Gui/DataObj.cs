﻿using System.Collections.Generic;

namespace Minecraft_QQ_Gui.SetWindow
{
    public record Server
    {
        public string Name { get; set; }
        public string Addr { get; set; }
    }
    public record CommandOBJ
    {
        public string Check { get; set; }
        public string Command { get; set; }
        public bool Use { get; set; }
        public bool Send { get; set; }
        public string Server { get; set; }
        public List<string> ServerS = new List<string>();
    }
}
