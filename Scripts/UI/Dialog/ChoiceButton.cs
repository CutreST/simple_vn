using Godot;
using MySystems;
using MySystems.VisualNovel;
using System;

namespace UI.Dialog
{
    /// <summary>
    /// The button that holds a choice
    /// </summary>
    public class ChoiceButton : Button
    {
        /// <summary>
        /// Index of the button at <see cref="ChoiceDisplay._usedButtons"/>
        /// </summary>
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

        /// <summary>
        /// Event raised when the button is pressed
        /// </summary>
        public void OnPressed(){
            ConsoleSystem.Write("Pressed ChoiceButton " + Index);

            //get control sys, ge
            ChoiceSystem choiceSystem;
            System_Manager.GetInstance(this).TryGetSystem<ChoiceSystem>(out choiceSystem);

            choiceSystem.Index = this.Index;
        }

    }
}
