using Godot;
using MySystems;

namespace Entities.BehaviourTree.Audio
{

    public class ChangeBGM : LeafNode
    {
        [Export]
        private readonly AudioStream _song;

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
            musicSys.PlayTrack(_song);
            return base.ChangeNodeStatus(controller, States.SUCCESS);
        }
    }

}