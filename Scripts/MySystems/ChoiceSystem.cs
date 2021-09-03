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
        public ChoiceDisplay ChoiceDisp{ get; set; }

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

            //chekear si existe o algo...
            
        }

        public override void OnExitSystem(params object[] obj)
        {
            Messages.ExitSystem(this);
        }

        #endregion

        public void ShowChoices(){
            List<string> testA = new List<string>();
            testA.Add("Choice one!");
            testA.Add("Choice two!");
            testA.Add("Choice three!");
            testA.Add("Choice four!");

            ChoiceDisp.ShowChoices(testA);
        }

    }
}
