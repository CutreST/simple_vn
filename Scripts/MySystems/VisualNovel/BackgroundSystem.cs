using Godot;
using System;
using UI.Dialog;

namespace MySystems.VisualNovel
{
    public class BackgroundSystem : System_Base
    {
        private BackgroundDisplay display;
        private readonly string BACK_PATH = "res://Scenes/UI/Background_Cont.tscn";


        #region  system_base methods
        public override void OnEnterSystem(params object[] obj)
        {
            Messages.EnterSystem(this);
            display = GD.Load<PackedScene>(BACK_PATH).Instance<BackgroundDisplay>();
            display.BackSys = this;
            MyManager.NodeManager.AddChild(display);
            //estaría bien un checker, pero paso
            display.Show();
        }

        public override void OnExitSystem(params object[] obj)
        {
            Messages.EnterSystem(this);
        }
        #endregion

        public void ChangeBackground(in Texture text){
            this.display.ChangeTexture(text);
        }
    }
}
