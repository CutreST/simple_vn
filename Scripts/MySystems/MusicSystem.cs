using Godot;
using System;
using System.Collections.Generic;
using Audio;

namespace MySystems
{

    public class MusicSystem : System_Base
    {
        const string PATH = "res://Scenes/Audio/MusicDisp.tscn";

        AudioStream _currentStream;

        Stack<AudioStream> _stackStream;

        MusicDisp _disp;

        public MusicSystem(){
            _stackStream = new Stack<AudioStream>();
        }

        private void Init(){
            Node n = MyManager.NodeManager.GetParent();
            _disp = n.TryGetFromChild_Rec<MusicDisp>();

            if(_disp != null){
                Messages.Print("yelooooow, is it me");
            }else{
                PackedScene  sc= GD.Load<PackedScene>(PATH);
                _disp = sc.Instance<MusicDisp>();
                MyManager.NodeManager.AddChild(_disp);
            }
        }

        internal void StopTrack()
        {
            _disp.StopTrack();
        }

        /// <summary>
        /// Adds and plays a new audio stream.
        /// If wanted to change the audio stream, only pass the new audistream
        /// If wanted to change the audio, but at some point, recover the previous, mark "intoTheStack = true"
        /// TODO: maybe an enum?
        /// </summary>
        /// <param name="music"></param>
        /// <param name="intoTheStack"></param>
        public void PlayTrack(in AudioStream music, in bool intoTheStack = false){           
            
            if(intoTheStack){
                _stackStream.Push(_currentStream);                
            }
            _currentStream = music;
            _disp.PlayTrack(_currentStream);

        }

        public bool TryRemoveFromStack(){

            if(_stackStream.Count == 0){
                return false;
            }else{
                _currentStream = _stackStream.Pop();
                return true;
            }
        }


        #region System methods

        public override void OnEnterSystem(params object[] obj)
        {
            Messages.EnterSystem(this);
            this.Init();
        }

        public override void OnExitSystem(params object[] obj)
        {
            Messages.ExitSystem(this);
        }

        #endregion
    }
}
