using Godot;
using MySystems;
using MySystems.VisualNovel;
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

        public override void _ExitTree()
        {
            base._ExitTree();
            base.Disconnect("pressed", this, nameof(OnPressed));
        }

        public void OnPressed(){
            ConsoleSystem.Write("Pressed ChoiceButton " + Index);

            //get control sys, ge
            ChoiceSystem choiceSystem;
            System_Manager.GetInstance(this).TryGetSystem<ChoiceSystem>(out choiceSystem);

            choiceSystem.Index = this.Index;
        }

    }
}
