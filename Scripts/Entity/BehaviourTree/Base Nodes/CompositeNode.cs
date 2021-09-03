using Godot;
using System;

using System.Collections.Generic;

namespace Entities.BehaviourTree
{
    /// <summary>
    /// The base class for all the composite node, it have a list of children and an byte index
    /// </summary>
    public abstract class CompositeNode : BaseNode
    {
        /// <summary>
        /// List of the children
        /// </summary>
        /// <value></value>
        public List<BaseNode> Children { get; protected set; }

        /// <summary>
        /// The index
        /// </summary>
        protected byte Index;

        /// <summary>
        /// empty constructor
        /// </summary>
        public CompositeNode(){}

        /// <summary>
        /// Constructor withd a list of children
        /// </summary>
        /// <param name="childList"></param>
        public CompositeNode(in List<BaseNode> childList)
        {
            Children = childList;
        }

        /// <summary>
        /// Adds a child without check
        /// </summary>
        /// <param name="child"></param>
        public void AddChild(in BaseNode child)
        {
            this.Children.Add(child);
        }

        /// <summary>
        /// Adds a unique child to avoid duplicates.
        /// OJU! Only checks the instance
        /// </summary>
        /// <param name="child"></param>
        public void AddUniqueChild(in BaseNode child)
        {
            if (this.Children.Contains(child))
            {
                return;
            }

            Children.Add(child);
        }
        
        public override void InitNode(in TreeController controller)
        {
            this.Children = new List<BaseNode>();
            BaseNode node;
            for (byte i = 0; i < base.GetChildCount(); i++)
            {
                if ((node = base.GetChild((int)i) as BaseNode) != null)
                {
                    this.AddUniqueChild(base.GetChild((int)i) as BaseNode);
                    node.InitNode(controller);
                    continue;
                }                
            }
        }

    }
}
