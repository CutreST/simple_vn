using Audio;
using Godot;
using System;

namespace MySystems
{
    /// <summary>
    /// Sound system. Connects to <see cref="SoundDisp"/>
    /// </summary>
    public class SoundSystem : System_Base
    {
        /// <summary>
        /// The <see cref="SoundDisp"/> that actually plays the sounds and so on
        /// </summary>
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

        /// <summary>
        /// Plays a sound
        /// </summary>
        /// <param name="st">sound to play</param>
        public void PlaySound(in AudioStream st){
            _display.PlaySound(st);
        }
    }
}
