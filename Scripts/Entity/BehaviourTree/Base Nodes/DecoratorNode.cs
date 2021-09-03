using Godot;
using System;

namespace Entities.BehaviourTree
{
    /// <summary>
    /// Base class for all the decorators. Only have one child
    /// </summary>
    public abstract class DecoratorNode : BaseNode
    {
        /// <summary>
        /// The child of this node
        /// </summary>
        /// <value></value>
        public BaseNode Child { get; set; }

        public override void InitNode(in TreeController controller)
        {
            if(base.GetChildCount() == 0){
                return;
            }
            
            BaseNode node = base.GetChild(0) as BaseNode;
            node.InitNode(controller);
            Child = node;

        }
    }
}
