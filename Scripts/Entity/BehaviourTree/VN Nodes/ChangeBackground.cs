using Godot;
using System;
using MySystems;
using MySystems.VisualNovel;

namespace Entities.BehaviourTree.VN_Nodes
{

    public class ChangeBackground : LeafNode
    {
        [Export]
        private Texture background;

        public override void InitNode(in TreeController controller)
        {
            
        }

        public override void OnEnter(in TreeController controller)
        {
            
        }

        public override States Tick(in TreeController controller)
        {
            BackgroundSystem back;
            System_Manager.GetInstance(controller).TryGetSystem<BackgroundSystem>(out back, true);

            ConsoleSystem.Write("Tick on " + base.Name);
            
            back.ChangeBackground(this.background);
            return base.ChangeNodeStatus(controller, States.SUCCESS);
        }
    }
}
