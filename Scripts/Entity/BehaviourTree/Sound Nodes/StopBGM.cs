using Godot;
using MySystems;
using System;

namespace Entities.BehaviourTree.Audio
{

    public class StopBGM : LeafNode
    {
        public override void InitNode(in TreeController controller)
        {
            
        }

        public override void OnEnter(in TreeController controller)
        {
            
        }

        public override States Tick(in TreeController controller)
        {
            MusicSystem musicSys;
            System_Manager.GetInstance(this).TryGetSystem<MusicSystem>(out musicSys, true);
            musicSys.StopTrack();
            return base.ChangeNodeStatus(controller, States.SUCCESS);
        }
    }
}
