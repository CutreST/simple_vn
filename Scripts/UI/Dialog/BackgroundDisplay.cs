using Godot;
using MySystems.VisualNovel;
using System;

namespace UI.Dialog
{
    public class BackgroundDisplay : Control
    {
        [Export]
        public readonly string BACKGROUND_NAME;

        private TextureRect background;

        public BackgroundSystem BackSys{ get; set; }


        public override void _EnterTree()
        {
            background = this.TryGetFromChild_Rec<TextureRect>(BACKGROUND_NAME);
        }

        public void ChangeTexture(in Texture text){
            background.Texture = text;
            MySystems.ConsoleSystem.Write("Background changed!!");
        }

       

    }
}
