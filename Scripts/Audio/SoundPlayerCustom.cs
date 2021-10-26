using Godot;
using System;

namespace Audio
{

    public class SoundPlayerCustom : AudioStreamPlayer
    {
        public delegate void PlayerCustom(in SoundPlayerCustom pl);
        public event PlayerCustom OnFinishSound;

        public override void _EnterTree()
        {
            base._EnterTree();
            //nos conectaremos a la señal...
            base.Connect("finished", this, nameof(RaiseOnFinishSound));
        }

        public override void _ExitTree()
        {
            base._ExitTree();
            base.Disconnect("finished", this, nameof(RaiseOnFinishSound));
        }

        private void RaiseOnFinishSound(){
            if(OnFinishSound != null){
                this.OnFinishSound(this);
            }
        }
    }
}
