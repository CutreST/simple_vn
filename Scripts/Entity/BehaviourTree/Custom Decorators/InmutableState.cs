using Godot;
using System;

namespace Entities.BehaviourTree
{
    /// <summary>
    /// A decorator that always return the same state
    /// </summary>
    public class InmutableState : DecoratorNode
    {
        /// <summary>
        /// The desired state to return
        /// </summary>
        [Export]
        private States _inmutableState;

        /// <summary>
        /// Empty constructor
        /// </summary>
        public InmutableState()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="state">Desired state</param>
        public InmutableState(in States state)
        {
            base.NodeState = state;
        }

        public override void InitNode(in TreeController controller)
        {
            base.InitNode(controller);
            base.NodeState = _inmutableState;
        }

        public override States Tick(in TreeController controller)
        {
            if (this.Child == null)
            {
                GD.Print("Child nuuuuull");
                return base.NodeState;
            }
            
            controller.EnterNode(Child);
            Child.Tick(controller);

            if (_inmutableState != States.RUNNING)
            {
                controller.ExitNode(Child);
            }

            return base.NodeState;
        }

        public override void OnEnter(in TreeController controller)
        {

        }


    }
}
