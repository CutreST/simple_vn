using Godot;
using MySystems;
using System;

namespace Entities.BehaviourTree.VN_Nodes
{

    public class ReferenceNode_U : DecoratorNode
    {
        [Export]
        private readonly string NODE_PATH;


        public override void InitNode(in TreeController controller)
        {
            //base.InitNode(controller);
            //this.Children = new System.Collections.Generic.List<BaseNode>();
            ConsoleSystem.Write("Initialized " + base.Name);

            //look for the node
            Node root = controller.Root;

            root = root.GetNode(NODE_PATH);

            //TODO: Hacer que no sean leaf nodes!
            Child = (BaseNode)root;
        }

        public override void OnEnter(in TreeController controller) { }
    
    public override States Tick(in TreeController controller)
        {
            // this node is succesful if theres some child to put on running
            try
            {
                Child.ChangeNodeStatus(controller, States.RUNNING);
                return base.ChangeNodeStatus(controller, States.SUCCESS);
            }
            catch{
                ConsoleSystem.Write("Ojuuu! There's n child on " + base.Name);
                // TODO: unify the console and the log
                return base.ChangeNodeStatus(controller, States.FAILURE);
            }

        }
    }
}
