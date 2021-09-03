using Godot;
using System;
using UI.Dialog;
using MySystems.MyInput;

namespace MySystems
{

    public class DialogSystem : Visual_SystemBase
    {
        private static string DIALOG_DISPLAY_PATH = "res://Scenes/UI/Dial_Cont.tscn";

        public bool IsRunning{ get; protected set; }

        public DialogSystem(in Node go, in System_Manager manager) : base(go, manager)
        {
        }

        public DialogSystem() : base (null, null){}

        public DialogDisplay DialDisp { get; set; }

        public InputDialog DialInput{ get; set; }

        public void DisplayDialog(in string text){
            DialDisp.PutTextToDisplay(text);
            MyManager.AddToStack(this);
        }

        #region MySystem methods
        public override void OnEnterSystem(params object[] obj)
        {
            Messages.EnterSystem(this);
            //dialog
            //first, cheeck if a console already exists
            Node n = MyManager.NodeManager.GetParent();
            DialDisp = n.TryGetFromChild_Rec<DialogDisplay>();

            if (DialDisp != null)
            {
                Messages.Print("yelooooow, is it me");
            }
            else
            {
                PackedScene sc = GD.Load<PackedScene>(DIALOG_DISPLAY_PATH);
                DialDisp = sc.Instance<DialogDisplay>();
                //n.CallDeferred("add_child", _console);
                //de momento lo metemos aquí, seguramente querremos un level controller o algu
                MyManager.NodeManager.AddChild(DialDisp);                
            }

            GO = DialDisp;
            DialDisp.DialogSystem = this;
            DialInput = new InputDialog(this);
            MyInput = DialInput;
        }

        public override void MyUpdate(in float delta)
        {
            DialInput.GetInput();
            base.MyUpdate(delta);
        }

        public override void OnExitSystem(params object[] obj)
        {
            Messages.ExitSystem(this);
        }
        #endregion

        #region Stack Methods
        public override void OnEnterStack()
        {
            Messages.EnterStack(this);
        }


        public override void OnExitStack()
        {
            Messages.ExitStack(this);
        }

        

        public override void OnPauseStack()
        {
            Messages.PauseStack(this);
        }

        public override void OnResumeStack()
        {
            Messages.ResumeStack(this);
        }
        #endregion
    }
}
