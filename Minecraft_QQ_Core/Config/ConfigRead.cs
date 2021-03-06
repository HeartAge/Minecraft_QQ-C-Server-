﻿using Minecraft_QQ_Core.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Minecraft_QQ_Core.Config
{
    internal class ConfigRead
    {
        /// <summary>
        /// 读取主要配置文件
        /// </summary>
        public static MainConfig ReadConfig()
        {
            Logs.LogOut("[Config]读取主配置");
            try
            {
                MainConfig config = JsonConvert.DeserializeObject<MainConfig>
                    (File.ReadAllText(ConfigFile.主要配置文件.FullName));
                bool save = false;
                if (config.数据库 == null)
                {
                    config.数据库 = new();
                    save = true;
                }
                if (config.检测 == null)
                {
                    config.检测 = new();
                    save = true;
                }
                if (config.消息 == null)
                {
                    config.消息 = new();
                    save = true;
                }
                if (config.管理员 == null)
                {
                    config.管理员 = new();
                    save = true;
                }
                if (config.设置 == null)
                {
                    config.设置 = new();
                    save = true;
                }
                if (config.链接 == null)
                {
                    config.链接 = new();
                    save = true;
                }
                if (save)
                {
                    IMinecraft_QQ.ShowMessageCall?.Invoke("Mainconfig.json配置文件读取发送错误，已经重写");
                    ConfigWrite.Config();
                }
                return config;
            }
            catch (Exception e)
            {
                IMinecraft_QQ.ShowMessageCall?.Invoke("快去检查你的Mainconfig.json文件语法，用的是json就要遵守语法！");
                Logs.LogError(e);
                return new();
            }
        }
        public static PlayerConfig ReadPlayer()
        {
            Logs.LogOut("[Config]读取玩家配置");
            try
            {
                PlayerConfig config = JsonConvert.DeserializeObject<PlayerConfig>
                    (File.ReadAllText(ConfigFile.玩家储存.FullName));
                bool save = false;
                if (config.玩家列表 == null)
                {
                    config.玩家列表 = new();
                    save = true;
                }
                if (config.禁止绑定列表 == null)
                {
                    config.禁止绑定列表 = new() { "Color_yr" };
                    save = true;
                }
                if (config.禁言列表 == null)
                {
                    config.禁言列表 = new();
                    save = true;
                }
                if (save)
                {
                    IMinecraft_QQ.ShowMessageCall?.Invoke("Player.json配置文件读取发送错误，已经重写");
                    ConfigWrite.Player();
                }
                return config;
            }
            catch (Exception e)
            {
                IMinecraft_QQ.ShowMessageCall?.Invoke("快去检查你的Player.json文件语法，用的是json就要遵守语法！");
                Logs.LogError(e);
                return new();
            }
        }
        public static GroupConfig ReadGroup()
        {
            Logs.LogOut("[Config]读取群设置");
            try
            {
                GroupConfig config = JsonConvert.DeserializeObject<GroupConfig>
                    (File.ReadAllText(ConfigFile.群设置.FullName));
                if (config.群列表 == null)
                {
                    IMinecraft_QQ.ShowMessageCall?.Invoke("Group.json配置文件读取发送错误，已经重写");
                    config.群列表 = new();
                    ConfigWrite.Group();
                }
                foreach (var item in config.群列表)
                {
                    if (item.Value.主群 == true)
                    {
                        IMinecraft_QQ.Main.GroupSetMain = item.Key;
                        break;
                    }
                }
                return config;
            }
            catch (Exception e)
            {
                IMinecraft_QQ.ShowMessageCall?.Invoke("快去检查你的Group.json文件语法，用的是json就要遵守语法！");
                Logs.LogError(e);
                return new GroupConfig();
            }
        }
        public static AskConfig ReadAsk()
        {
            Logs.LogOut("[Config]读取自定义应答");
            try
            {
                AskConfig config = JsonConvert.DeserializeObject<AskConfig>
                    (File.ReadAllText(ConfigFile.自动应答.FullName));

                if (config.自动应答列表 == null)
                {
                    config.自动应答列表 = new Dictionary<string, string>()
                    {
                        {
                            "服务器菜单", 
                            "服务器查询菜单：\r\n" +
                            $"【{IMinecraft_QQ.Main.MainConfig.检测.检测头}{IMinecraft_QQ.Main.MainConfig.检测.玩家设置名字} ID】可以绑定你的游戏ID。\r\n" +
                            $"【{IMinecraft_QQ.Main.MainConfig.检测.检测头}{IMinecraft_QQ.Main.MainConfig.检测.在线玩家获取}】可以查询服务器在线人数。\r\n" +
                            $"【{IMinecraft_QQ.Main.MainConfig.检测.检测头}{IMinecraft_QQ.Main.MainConfig.检测.服务器在线检测}】可以查询服务器是否在运行。\r\n" +
                            $"【{IMinecraft_QQ.Main.MainConfig.检测.检测头}{IMinecraft_QQ.Main.MainConfig.检测.发送消息至服务器} 内容】可以向服务器里发送消息。（使用前请确保已经绑定了ID，）"}
                    };
                    IMinecraft_QQ.ShowMessageCall?.Invoke("Ask.json配置文件读取发送错误，已经重写");
                    File.WriteAllText(ConfigFile.自动应答.FullName, JsonConvert.SerializeObject(config, Formatting.Indented));
                }
                return config;
            }
            catch (Exception e)
            {
                IMinecraft_QQ.ShowMessageCall?.Invoke("快去检查你的Ask.json文件语法，用的是json就要遵守语法！");
                Logs.LogError(e);
                return new();
            }
        }
        public static CommandConfig ReadCommand()
        {
            Logs.LogOut("[Config]读取自定义指令");
            try
            {
                CommandConfig config = JsonConvert.DeserializeObject<CommandConfig>
                    (File.ReadAllText(ConfigFile.自定义指令.FullName));
                if (config.命令列表 == null)
                {
                    config.命令列表 = new()
                    {
                        {
                            "插件帮助",
                            new()
                            {
                                命令 = "qq help",
                                玩家使用 = false,
                                玩家发送 = false
                            }
                        },
                        {
                            "查钱",
                            new()
                            {
                                命令 = "money {arg:name}",
                                玩家使用 = true,
                                玩家发送 = false
                            }
                        },
                        {
                            "禁言",
                            new()
                            {
                                命令 = "mute {arg1}",
                                玩家使用 = false,
                                玩家发送 = false
                            }
                        },
                        {
                            "传送",
                            new()
                            {
                                命令 = "tpa {arg:at}",
                                玩家使用 = true,
                                玩家发送 = false
                            }
                        },
                        {
                            "给权限",
                            new()
                            {
                                命令 = "lp user {arg:at} permission set {arg1} true",
                                玩家使用 = false,
                                玩家发送 = false
                            }
                        },
                        {
                            "说话",
                            new()
                            {
                                命令 = "say {argx}",
                                玩家使用 = false,
                                玩家发送 = false
                            }
                        }
                    };
                    IMinecraft_QQ.ShowMessageCall?.Invoke("Command.json配置文件读取发送错误，已经重写");
                    File.WriteAllText(ConfigFile.自定义指令.FullName, JsonConvert.SerializeObject(config, Formatting.Indented));
                }
                return config;
            }
            catch (Exception e)
            {
                IMinecraft_QQ.ShowMessageCall?.Invoke("快去检查你的Command.json文件语法，用的是json就要遵守语法！");
                Logs.LogError(e);
                return new();
            }
        }
    }
}
