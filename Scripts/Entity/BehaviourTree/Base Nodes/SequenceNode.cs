using Godot;
using System;
using System.Diagnostics;

namespace Entities.BehaviourTree
{
    /// <summary>
    /// A seuqnece node. Returns failure if one node returns it
    /// </summary>
    public class SequenceNode : CompositeNode
    {
        public SequenceNode() : base(new System.Collections.Generic.List<BaseNode>())
        {

        }
        public override States Tick(in TreeController controller)
        {

            Debug.Assert((Children != null || Children.Count > 0), "There must be, at least, one child");

            for (byte i = Index; i < Children.Count; i++)
            {
                controller.EnterNode(Children[i]);
                switch (Children[i].Tick(controller))
                {
                    case States.FAILURE:
                        Index = 0;
                        return base.ChangeNodeStatus(controller, States.FAILURE);
                    case States.RUNNING:
                        Index = i;
                        return base.ChangeNodeStatus(controller, States.RUNNING);

                }
            }

            //controller.ExitNode(Children[i]);
            Index = 0;

            return base.ChangeNodeStatus(controller, States.SUCCESS);
        }

        public override void OnEnter(in TreeController controller)
        {

        }
    }
}
