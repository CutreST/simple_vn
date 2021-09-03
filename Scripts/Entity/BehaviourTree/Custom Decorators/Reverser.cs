using System;
using Godot;

namespace Entities.BehaviourTree
{
    /// <summary>
    /// A decorator node that reverse the success-failure
    /// </summary>
    public class Reverser : DecoratorNode
    {        
        
        public override States Tick(in TreeController controller)
        {   
            controller.EnterNode(Child);                     
            switch (base.Child.Tick(controller))
            {
                case States.FAILURE:
                    controller.ExitNode(this);
                    return base.NodeState = States.SUCCESS;
                case States.SUCCESS:
                    controller.ExitNode(this);
                    return base.NodeState = States.FAILURE;
            }
            
            
            return base.NodeState = States.RUNNING;
        }

        public override void OnEnter(in TreeController controller)
        {
            
        }
    }
}
