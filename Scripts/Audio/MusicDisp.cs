using System;
using Godot;
using MySystems;

namespace Audio
{
    public class MusicDisp : Node
    {
        AudioStreamPlayer _player;
        MusicSystem _musicSys;

        public override void _EnterTree()
        {
            _player = this.TryGetFromChild_Rec<AudioStreamPlayer>();

            //buscamos sys
            System_Manager.GetInstance(this).TryGetSystem<MusicSystem>(out _musicSys);
        }

        public void PlayTrack(in AudioStream st){
            _player.Stream = st;
            _player.Play();
        }

        internal void StopTrack()
        {
            _player.Stop();
        }
    }
}
