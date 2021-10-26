using Godot;
using MySystems.VisualNovel;
using System;


namespace UI.Dialog
{
    /// <summary>
    /// Displays a background using and image control <see cref="TextureRect"/>
    /// </summary>
    public class BackgroundDisplay : Control
    {
        /// <summary>
        /// The name of the <see cref="TextureRect"/>
        /// </summary>
        [Export]
        public readonly string BACKGROUND_NAME;

        /// <summary>
        /// The background image
        /// </summary>
        private TextureRect background;

        /// <summary>
        /// Background System
        /// </summary>
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
