using Godot;
using MySystems;
using System;

namespace UI.Dialog
{

    public class ChoiceButton : Button
    {
        public int Index{ get; set; }

        public override void _EnterTree()
        {
            base._EnterTree();
            base.Connect("pressed", this, nameof(OnPressed));
            ConsoleSystem.Write("ChoiceButton entered the tree");
        }
        public void OnPressed(){
            ConsoleSystem.Write("Pressed ChoiceButton " + Index);
        }

    }
}
