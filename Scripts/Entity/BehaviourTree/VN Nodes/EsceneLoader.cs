using Godot;
using System;

namespace Entities.BehaviourTree.VN_Nodes
{

    public class EsceneLoader : BaseNode
    {
        [Export]
        private readonly string SCENE_PATH;

        private BaseNode newRoot;

        public override void InitNode(in TreeController controller)
        {
            //check if the scene is a base node
            PackedScene temp = GD.Load<PackedScene>(SCENE_PATH);
            newRoot = temp.Instance() as BaseNode;

            if (newRoot == null)
            {
                MySystems.ConsoleSystem.Write($"OJU!!! EsceneLoader{base.Name} found an incompatible scene.\n" +
                                                "Node will be marked success 'cause don't want to break the operation flow.");
                //base.QueueFree();
            }

        }

        public override void OnEnter(in TreeController controller)
        {

        }

        public override States Tick(in TreeController controller)
        {
            //change root            
            if (newRoot != null)
            {
                controller.Root = newRoot;
                controller.AddChild(newRoot);
            }

            return NodeState = States.SUCCESS;
        }
    }
}
