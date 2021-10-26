using Godot;
using MySystems;
using System;

namespace Entities.BehaviourTree.Audio
{

    public class PlaySound : LeafNode
    {
        [Export]
        private AudioStream sound;

        public override void InitNode(in TreeController controller)
        {
            
        }

        public override void OnEnter(in TreeController controller)
        {
            
        }

        public override States Tick(in TreeController controller)
        {
            SoundSystem soundSystem;
            System_Manager.GetInstance(this).TryGetSystem<SoundSystem>(out soundSystem, true);
            soundSystem.PlaySound(this.sound);
            return base.ChangeNodeStatus(controller, States.SUCCESS);
        }
    }
}
