using Godot;
using System;

namespace Entities.BehaviourTree
{

    public class MessageByTick : LeafNode
    {
        [Export]
        private string _message;


        private enum MessageType : byte { LOG, ERROR, ADV }
        [Export]
        private MessageType _type;   
        

        public override void InitNode(in TreeController controller)
        {

        }

        public override void OnEnter(in TreeController controller)
        {

        }

        public override States Tick(in TreeController controller)
        {
            if (_message != null)
            {
                switch (_type)
                {
                    case MessageType.ERROR:
                        GD.PrintErr(_message);
                        break;
                    case MessageType.ADV:
                        GD.PrintRaw(_message);
                        break;
                    case MessageType.LOG:
                        GD.Print(_message);
                        break;
                }
            }
            return base.ChangeNodeStatus(controller, States.SUCCESS);
        }
    }
}
