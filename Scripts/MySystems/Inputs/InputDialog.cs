using Godot;
using System;

namespace MySystems.MyInput
{

    public class InputDialog : InputBase
    {
        public bool IsNext{ get; protected set; }

        private string NEXT_KEY = "ui_select";
        private string NEXT_MOUSE = "ui_mouse_select";

        public InputDialog(in Visual_SystemBase visual) : base(visual)
        {

        }

        public override void GetInput()
        {
            IsNext = Input.IsActionJustPressed(NEXT_KEY) || Input.IsActionJustPressed(NEXT_MOUSE);
        }
    }
}
