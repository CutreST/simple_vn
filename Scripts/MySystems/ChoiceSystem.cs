using Entities.BehaviourTree.VN_Nodes;
using Godot;
using System;
using System.Collections.Generic;
using UI.Dialog;

namespace MySystems
{

    public class ChoiceSystem : Visual_SystemBase
    {
        public ChoiceSystem() : base(null, null) { }
        private const string DISP_PATH = "res://Scenes/UI/Choice_Cont.tscn";
        public ChoiceDisplay ChoiceDisp { get; set; }

        public ChoiceSelector Selector { get; private set; }
        private int index;

        public int Index
        {
            get => index;
            set
            {
                index = value;
                Selector.Selection = index;
                ChoiceDisp.MyHide();
                MyManager.RemoveFromStack(this);
            }
        }

        #region stack
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

        #region system
        public override void OnEnterSystem(params object[] obj)
        {
            Messages.EnterSystem(this);

            //cargamos
            ChoiceDisp = GD.Load<PackedScene>(DISP_PATH).Instance() as ChoiceDisplay;
            MyManager.NodeManager.AddChild(ChoiceDisp);
            ChoiceDisp.MyHide();
            //chekear si existe o algo...

        }

        public override void OnExitSystem(params object[] obj)
        {
            Messages.ExitSystem(this);
        }

        #endregion

        public void ShowSelection(in ChoiceSelector selector)
        {
            this.Selector = selector;
            MyManager.AddToStack(this);

            //cargamos el display
            ChoiceDisp.ShowChoices(Selector.Routes);

            //pillamos el dialgo sys
            DialogSystem dial;
            MyManager.TryGetSystem<DialogSystem>(out dial);
            dial.DialDisp.Show();
        }


    }
}
