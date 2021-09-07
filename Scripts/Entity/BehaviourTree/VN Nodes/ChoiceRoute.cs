using Godot;
using MySystems;
using System;

namespace Entities.BehaviourTree.VN_Nodes
{

    public class ChoiceRoute : SequenceNode
    {
        [Export]
        public readonly string CHOICE_ID;

        [Export]
        private MySystems.LoadXML.TextType TO_LOAD;

        public string Text{ get; private set; }

        public override void InitNode(in TreeController controller)
        {
            base.InitNode(controller);
            Text = MySystems.LoadXML.LoadXmlElement(controller.Root.Name, CHOICE_ID, TO_LOAD);
            ConsoleSystem.Write("Initialized " + base.Name);
            //ConsoleSystem.Write("Text is: " + Text);
        }

        public override void OnEnter(in TreeController controller)
        {
            //throw new NotImplementedException();
        }        
    }
}
