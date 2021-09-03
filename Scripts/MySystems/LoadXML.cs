using Godot;
using System;
using System.Xml;

namespace MySystems
{

    public class LoadXML : Node
    {
        private static string RELATIVE_PATH { get => "Loc/en/"; }

        private static string FILE_NAME { get => "dial.xml"; }

        private static string TAG_DIAL { get => "dial"; }

        private static string ID_DEF { get => "id"; }

        public enum TextType : byte { DIAL, CHOICE, UI }


        public static string LoadXmlElement(in string id, in TextType type)
        {
            string a = "";
            string path = System.IO.Directory.GetCurrentDirectory();
            path += @"/" + RELATIVE_PATH + "//" + FILE_NAME;
            XmlDocument doc = new XmlDocument();
            doc.Load(path);

            XmlNodeList nodeList = doc.DocumentElement.GetElementsByTagName(GetTag(type));

            XmlAttribute attribute;
            for (int i = 0; i < nodeList.Count; i++)
            {

                attribute = nodeList[i].Attributes[ID_DEF];

                if (attribute != null)
                {
                    if (attribute.Value == id)
                    {
                        XmlNodeList text = nodeList[i].ChildNodes;

                        for (int j = 0; j < text.Count; j++)
                        {
                            a += text[j].InnerText + "\n";
                        }
                        return a;
                    }
                }
            }
            return a;
        }

        private static string GetTag(in TextType type)
        {
            switch (type)
            {
                case TextType.DIAL:
                    return TAG_DIAL;
            }
            return "";
        }

    }
}
