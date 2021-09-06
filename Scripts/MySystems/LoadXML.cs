using Godot;
using System;
using System.Text;
using System.Xml;

namespace MySystems
{

    public class LoadXML
    {
        private static string RELATIVE_PATH { get => "Loc/en/"; }

        private static string FILE_NAME { get => "dial.xml"; }

        private static string XML_ROOT { get => "FullDial"; }
        private static string TAG_DIAL { get => "dial"; }
        private static string TAG_DIAL_GENERAL { get => "dialogs"; }
        private static string TAG_DIAL_GROUP { get => "dial_group"; }
        private static string TAG_CHOICE { get => "ch"; }
        private static string TAG_CHOICE_GENERAL { get => "choices"; }
        private static string TAG_CHOICE_GROUP { get => "ch_group"; }

        private static string ID_DEF { get => "id"; }

        public enum TextType : byte { DIAL, CHOICE, UI }

        private static string path;

        private static XmlDocument doc;
        private static StringBuilder builder = new StringBuilder();

        public static string LoadXmlElement(in string groupId, in string textId, in TextType type)
        {
            if (doc == null)
            {
                doc = new XmlDocument();
                doc.Load(GetPath());
            }

            builder.Clear();

            XmlNode root = doc.DocumentElement;
            //first get general tag (ej: dialog, choice, ui, etc, first branch)
            XmlNodeList nodeList = root.SelectNodes(GetGeneralTag(type));
            root = nodeList[0];

            //now get the second branch, group id
            root = GetChildByID(root, groupId);

            //and now get the correct child of the group
            root = GetChildByID(root, textId);

            //in theory we have the needed, now save the text!
            for (int i = 0; i < root.ChildNodes.Count; i++){
                builder.Append(root.ChildNodes[i].InnerText);
                builder.Append("\n");
            }

            return builder.ToString();
        }

        private static XmlNode GetChildByID(in XmlNode root, in string id){
            XmlAttribute attribute;

            for (int i = 0; i < root.ChildNodes.Count; i++){
                attribute = root.ChildNodes[i].Attributes[ID_DEF];
                if(attribute != null){
                    if(attribute.Value == id){
                        return root.ChildNodes[i];
                    }
                }
            }

            return null;
        }
        
        private static string GetPath()
        {
            if (path == null)
            {
                path = System.IO.Directory.GetCurrentDirectory();
                path += @"/" + RELATIVE_PATH + "//" + FILE_NAME;
            }
            return path;
        }

        public static string LoadXmlElement(in string id, in TextType type)
        {
            string a = "";
            XmlDocument doc = new XmlDocument();
            doc.Load(GetPath());

            XmlNode root = doc.DocumentElement;
            XmlNodeList nodeList = root.SelectNodes(GetGeneralTag(type));
            root = nodeList[0];

            XmlAttribute attribute;

            for (int i = 0; i < root.ChildNodes.Count; i++)
            {

                attribute = root.ChildNodes[i].Attributes[ID_DEF];

                if (attribute != null)
                {
                    if (attribute.Value == id)
                    {
                        XmlNodeList text = root.ChildNodes[i].ChildNodes;

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
                case TextType.CHOICE:
                    return TAG_CHOICE;
            }
            return "";
        }

        private static string GetGeneralTag(in TextType type)
        {
            switch (type)
            {
                case TextType.DIAL:
                    return TAG_DIAL_GENERAL;
                case TextType.CHOICE:
                    return TAG_CHOICE_GENERAL;
            }
            return "";
        }

    }
}
