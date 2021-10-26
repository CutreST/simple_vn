using Audio;
using Godot;
using System;

namespace MySystems
{

    public class SoundSystem : System_Base
    {
        private SoundDisp _display;

        public override void OnEnterSystem(params object[] obj)
        {
            ConsoleSystem.Write("Hola");
            Messages.EnterSystem(this);
            _display = new SoundDisp();
            MyManager.NodeManager.AddChild(_display);
        }

        public override void OnExitSystem(params object[] obj)
        {
            Messages.ExitSystem(this);
        }

        public void PlaySound(in AudioStream st){
            _display.PlaySound(st);
        }
    }
}
