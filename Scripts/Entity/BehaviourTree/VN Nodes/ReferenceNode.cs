using Godot;
using MySystems;
using System;

namespace Entities.BehaviourTree.VN_Nodes
{

    public class ReferenceNode : SequenceNode
    {
        [Export]
        private readonly string NODE_PATH;

        [Export]
        private readonly string NODE_NAME;

        public override void InitNode(in TreeController controller)
        {
            //base.InitNode(controller);
            this.Children = new System.Collections.Generic.List<BaseNode>();
            ConsoleSystem.Write("Initialized " + base.Name);

            //look for the node
            Node root = controller.Root;

            root = root.GetNode(NODE_PATH);
            int index = root.GetIndex();
            BaseNode node;
            for (; index < root.GetParent().GetChildCount(); index++)
            {
                node = root.GetParent().GetChild(index) as BaseNode;
                if (node != null)
                {
                    Children.Add(node);
                    ConsoleSystem.Write("Get " + node.Name + " into the ref list");
                }
            }
        }

        public override States Tick(in TreeController controller)
        {
            return base.Tick(controller);
        }
    }
}
