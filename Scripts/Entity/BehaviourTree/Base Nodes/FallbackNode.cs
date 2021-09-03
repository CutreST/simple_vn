using Godot;
using System;
using System.Diagnostics;


namespace Entities.BehaviourTree
{
    /// <summary>
    /// Fallback or selector node. Returns success if one child success
    /// </summary>
    public class FallbackNode : CompositeNode
    {
        public FallbackNode() : base (new System.Collections.Generic.List<BaseNode>()){
            
        }    
        
        public override States Tick(in TreeController controller)
        {
            Debug.Assert((Children != null), "The children are null");
            Debug.Assert(Children.Count > 0, "There must be, at least, one child at " + this.GetType().Name);
            //controller.EnterNode(this);

            for (byte i = Index; i < Children.Count; i++)
            {
                controller.EnterNode(Children[i]);
                switch (Children[i].Tick(controller))
                {                    
                    case States.SUCCESS:
                        Index = 0;
                        return base.ChangeNodeStatus(controller, States.SUCCESS);
                    case States.RUNNING:
                        Index = ++i;
                        return base.ChangeNodeStatus(controller, States.RUNNING);                    
                }
                
            }
            
            Index = 0;
            return base.ChangeNodeStatus(controller, States.FAILURE);
        }

        public override void OnEnter(in TreeController controller)
        {

        }
    }
}
