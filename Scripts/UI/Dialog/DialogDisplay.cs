using Godot;
using Base.Interfaces;
using System;
using MySystems;
using MySystems.VisualNovel;
using System.Text;

namespace UI.Dialog
{
    public class DialogDisplay : Control, IUpdate
    {
        [Export]
        private readonly string LBL_DIAL_NAME;

        [Export]
        private readonly float TIME_TO_SHOW;

        public DialogSystem DialogSystem { get; set; }

        public StringBuilder TextToDisplay { get; private set; }

        public StringBuilder TextToShow { get; private set; }

        private RichTextLabel lbl_dial;

        private int index;

        private float currentTime; //-> CHANGE THIS!

        private char NEW_LINE_A = '\n';
        private char NEW_LINE_B = '\r';

        public enum Status : byte { RUNNING, FINISHED, SHOWING, WAITING }
        public Status DialStatus { get; private set; }

        public override void _EnterTree()
        {
            base._EnterTree();
            this.TestInit();
            TextToShow = new StringBuilder();
            TextToDisplay = new StringBuilder();
            lbl_dial = this.TryGetFromChild_Rec<RichTextLabel>(LBL_DIAL_NAME);
            this.PutTextToDisplay("La [color=green]señorita caquita[/color] era famosa por esos lares.\n La llamaban fea, a saber por qué.\n Quizás estaba fuera de sí.\n");
        }

        private void TestInit()
        {
            DialogSystem temp;
            System_Manager man = System_Manager.GetInstance(this);
            man.TryGetSystem<DialogSystem>(out temp, true);
            DialogSystem = temp;
            DialogSystem.AddToUpdate(this);
            man.AddToStack(DialogSystem);
        }

        private void SetWaitingStatus()
        {

            if (index == TextToDisplay.Length)
                DialStatus = Status.FINISHED;
            else
                DialStatus = Status.WAITING;

        }

        public void MyUpdate(in float delta)
        {
            if (DialogSystem.DialInput.IsNext)
            {
                ConsoleSystem.Write("Accepted!");

                switch (DialStatus)
                {
                    case Status.SHOWING:
                        lbl_dial.VisibleCharacters = -1;
                        this.SetWaitingStatus();
                        break;

                    case Status.WAITING:
                        DialStatus = Status.RUNNING;
                        break;

                    case Status.FINISHED:
                        //lo ponemos así como debug.
                        base.Hide();
                        TextToDisplay.Clear();
                        TextToShow.Clear();
                        MySystems.ConsoleSystem.Write("Removed?" + DialogSystem.MyManager.RemoveFromStack(DialogSystem));
                        break;
                }
            }

            switch (DialStatus)
            {
                case Status.SHOWING:
                    currentTime += delta;

                    if (currentTime > TIME_TO_SHOW)
                    {
                        currentTime = 0;
                        lbl_dial.VisibleCharacters = lbl_dial.VisibleCharacters + 1;

                        if (lbl_dial.VisibleCharacters == lbl_dial.GetTotalCharacterCount())
                            this.SetWaitingStatus();

                    }
                    break;

                case Status.RUNNING:
                    this.SetNextLine();
                    DialStatus = Status.SHOWING;
                    break;
            }
        }

        private void SetNextLine()
        {
            char current;
            TextToShow.Clear();

            for (; index < TextToDisplay.Length; index++)
            {
                current = TextToDisplay[index];
                TextToShow.Append(current);

                if (current == NEW_LINE_A || current == NEW_LINE_B)
                {
                    index++;
                    break;
                }
            }

            lbl_dial.BbcodeText = TextToShow.ToString();
            lbl_dial.VisibleCharacters = 0;
        }

        public void PutTextToDisplay(in string text)
        {
            base.Show();
            TextToDisplay.Clear();
            TextToDisplay.Append(text);
            index = 0;
            DialStatus = Status.RUNNING;
        }
    }
}
