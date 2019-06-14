﻿using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Color_yr.Minecraft_QQ
{
    class XML
    {
        /// <summary>
        /// 使用linq 建立xml
        /// </summary>
        /// <param name="file">文件</param>
        /// <param name="mode">模式</param>>
        public void CreateFile(string file, int mode)
        {
            if (File.Exists(config_read.path + file) && mode == 1) //文件存在就删除
                File.Delete(config_read.path + file);
            XElement contacts = new XElement("config");
            contacts.Save(config_read.path + file);
        }
        /// <summary>
        /// //修改XML文件中的元素
        /// </summary>
        /// <param name="file">文件名</param>
        /// <param name="type">类型名</param>
        /// <param name="attribute">属性名</param>
        /// <param name="data">数据</param>
        public void setXml(string file, string type, string attribute, string data)
        {
            if (File.Exists(config_read.path + file) == false)
                CreateFile(file, 0);//创建该文件，如果路径文件夹不存在，则报错。
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(config_read.path + file);
            XmlNodeList nodeList = xmldoc.SelectSingleNode("config/" + type).ChildNodes;//获取bookstore节点的所有子节点
            foreach (XmlNode xn in nodeList)//遍历所有子节点
            {
                XmlElement xe2 = (XmlElement)xn;//转换类型
                if (xe2.Name == attribute)//如果找到
                {
                    xe2.InnerText = data;//则修改
                    break;//找到退出来就可以了
                }
            }
            xmldoc.Save(config_read.path + file);//保存。
        }
        /// <summary>
        /// //增加元素到XML文件
        /// </summary>
        /// <param name="fine">文件名</param>
        /// <param name="type">类型名</param>
        /// <param name="attribute">属性名</param>
        /// <param name="data">数据</param>
        public void write(string file, string type, string attribute, string data)
        {
            if (File.Exists(config_read.path + file) == false)
                CreateFile(file, 0);//创建该文件，如果路径文件夹不存在，则报错。
            try
            {
                string a = read(file, type, attribute);
                if (a != null)
                    setXml(file, type, attribute, data);
                else
                {
                    ///导入XML文件
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(config_read.path + file);

                    XmlElement node = (XmlElement)xmldoc.SelectSingleNode("config/" + type);
                    if (node == null)
                    {
                        node = xmldoc.CreateElement(type);
                    }
                    node.SetAttribute("type", type);
                    XmlElement xesub1 = xmldoc.CreateElement(attribute);
                    xesub1.InnerText = data;
                    node.AppendChild(xesub1);

                    xmldoc.DocumentElement.AppendChild(node);
                    xmldoc.Save(config_read.path + file);
                }
            }
            catch (Exception)
            {
                if (MessageBox.Show("配置文件在读取时发发生了错误，是否要删除原来的配置文件再新生成一个？",
                    "配置文件错误", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    CreateFile(file, 0);
                    write(file, type, attribute, data);
                }
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="fine">文件名</param>
        /// <param name="type">类型名</param>
        /// <param name="attribute">属性名</param>
        public string read(string file, string type, string attribute)
        {
            string temp = null;
            if (File.Exists(config_read.path + file) == false)
                CreateFile(file, 0);//创建该文件，如果路径文件夹不存在，则报错。
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(config_read.path + file);

                XmlNode xnP = xmlDoc.SelectSingleNode("config/" + type + "/" + attribute);
                temp = xnP.InnerText;
                if (temp == "") temp = null;
            }
            catch (Exception)
            { }
            return temp;
        }

        public string read_memory(string file, string type, string attribute)
        {
            string temp = null;
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(file);

                XmlNode xnP = xmlDoc.SelectSingleNode("config/" + type + "/" + attribute);
                temp = xnP.InnerText;
                if (temp == "") temp = null;
            }
            catch (Exception)
            { }
            return temp;
        }
    }
}