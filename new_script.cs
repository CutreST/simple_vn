using Godot;
using System.IO;
using MySystems;
using System.Xml;

public class new_script : Node
{
    [Export]
    private readonly string RELATIVE_PATH;

    [Export]
    private readonly string ID_LOADER;

    [Export]
    private readonly string ID_DEF;

    private readonly string FILE_NAME = "dial.xml";

    private readonly string TAG = "dial";
    XmlDocument document;

    public override void _EnterTree()
    {
        base._EnterTree();
        string a = this.LoadXmlElement();
        Messages.Print(this.LoadXmlElement());
        ConsoleSystem c;
        System_Manager.GetInstance(this).TryGetSystem<ConsoleSystem>(out c);
        ConsoleSystem.Write("Testing Console");
    }

    //De momento esto funca bien, habrá que intentar optimizar un poco y demás, pero bueno, también para seleccionar choices y eso
    //sin embargo, esto irá en otra clase, no aquí.
    private string LoadXmlElement(){
        string a = "";
        string path = System.IO.Directory.GetCurrentDirectory();
        Messages.Print(path);
        path += @"/" + RELATIVE_PATH + "//" + FILE_NAME;
        document = new XmlDocument();
        document.Load(path);

        XmlElement root = document.DocumentElement;
        XmlNodeList list = root.GetElementsByTagName(TAG);

        string prop = "";
        XmlAttribute attribute;
        for (int i = 0; i < list.Count; i++){

            attribute = list[i].Attributes[ID_DEF];

            if(attribute != null){
                if(attribute.Value == ID_LOADER){
                    XmlNodeList text = list[i].ChildNodes;

                    for (int j = 0; j < text.Count; j++){
                        prop += text[j].InnerText + "\n";
                    }
                    return prop;
                }
            }
        }
            return a;
    }

}
