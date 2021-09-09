using Godot;
using MySystems.VisualNovel;
using MySystems;
using System;

namespace Entities.BehaviourTree.VN_Nodes
{

    public class DialogNode : BaseNode
    {
        [Export]
        private readonly string DIAL_ID;

        [Export]
        private MySystems.LoadXML.TextType TO_LOAD;

        private string text;

        public override void InitNode(in TreeController controller)
        {
            //cargamos mierdas
            text = MySystems.LoadXML.LoadXmlElement(controller.Root.Name, DIAL_ID, TO_LOAD);
            ConsoleSystem.Write("Initialized " + base.Name);
        }

        public override void OnEnter(in TreeController controller)
        {
            //throw new NotImplementedException();
            //base.ChangeNodeStatus(StackOverflowException.)
        }

        public override States Tick(in TreeController controller)
        {
            //throw new NotImplementedException();

            ConsoleSystem.Write("Tick on " + base.Name);
            
            if(NodeState == States.RUNNING){
                base.ChangeNodeStatus(controller, States.SUCCESS);
            }else{
                base.ChangeNodeStatus(controller, States.RUNNING);

                DialogSystem dial;
                if(MySystems.System_Manager.GetInstance(this).TryGetSystem<DialogSystem>(out dial, true)){
                    dial.DisplayDialog(text);
                }

            }

            return base.NodeState;
        }
    }
}
