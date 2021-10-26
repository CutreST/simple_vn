using Godot;
using Base.Interfaces;
using System;
using MySystems;
using MySystems.VisualNovel;
using System.Text;

/// <summary>
/// Group for all UI dialog related
/// </summary>
/// <remarks>
/// So, we are using something as a System based model, so every UI display
/// has a system that connects the outside with this. Maybe it's not the 
/// best, but it works well. 
/// </remarks>

namespace UI.Dialog
{
    /// <summary>
    /// Display for the dialog.
    /// </summary>
    public class DialogDisplay : Control, IUpdate
    {
        /// <summary>
        /// default lable name
        /// </summary>
        [Export]
        private readonly string LBL_DIAL_NAME;

        /// <summary>
        /// Time for loading the text
        /// </summary>
        [Export]
        private readonly float TIME_TO_SHOW;

        /// <summary>
        /// The <see cref="DialogSystem"/> attached to this display
        /// </summary>
        public DialogSystem DialogSystem { get; set; }
        
        /// <summary>
        /// Full text to display. It has all the sentences that we want to pirnt
        /// </summary>
        public StringBuilder TextToDisplay { get; private set; }

        /// <summary>
        /// It holds only the text to show, generally a sentence or paragraph. 
        /// </summary>
        public StringBuilder TextToShow { get; private set; }

        private RichTextLabel lbl_dial;

        private int index;

        private float currentTime; //-> CHANGE THIS!

        /// <summary>
        /// New line character
        /// </summary>
        private const char NEW_LINE_A = '\n';

        /// <summary>
        /// Another new line character, just in case
        /// </summary>
        private const char NEW_LINE_B = '\r';

        /// <summary>
        /// Status of the current dialog sentence
        /// </summary>
        public enum Status : byte { RUNNING, FINISHED, SHOWING, WAITING }
        public Status DialStatus { get; private set; }

        public override void _EnterTree()
        {
            base._EnterTree();
            this.TestInit();
            TextToShow = new StringBuilder();
            TextToDisplay = new StringBuilder();
            lbl_dial = this.TryGetFromChild_Rec<RichTextLabel>(LBL_DIAL_NAME);
            //this.PutTextToDisplay("La [color=green]señorita caquita[/color] era famosa por esos lares.\n La llamaban fea, a saber por qué.\n Quizás estaba fuera de sí.\n");
        }

        /// <summary>
        /// A simple test for debug prourpouses
        /// </summary>
        private void TestInit()
        {
            DialogSystem temp;
            System_Manager man = System_Manager.GetInstance(this);
            man.TryGetSystem<DialogSystem>(out temp, true);
            DialogSystem = temp;
            DialogSystem.AddToUpdate(this);
            man.AddToStack(DialogSystem);
        }

        /// <summary>
        /// Sets <see cref="DialStatus"/> to waiting or finished if the
        /// <see cref="TextToDisplay"/> equals the index
        /// </summary>
        private void SetWaitingStatus()
        {

            if (index == TextToDisplay.Length)
                DialStatus = Status.FINISHED;
            else
                DialStatus = Status.WAITING;

        }

        public void MyUpdate(in float delta)
        {
            //if there's input
            if (DialogSystem.DialInput.IsNext)
            {
                ConsoleSystem.Write("Accepted!");
                // change to method?
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

            // i think i can do it better
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

        /// <summary>
        /// Sets the next line from <see cref="TextToDisplay"/>
        /// </summary>
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

        /// <summary>
        /// Puts a text to <see cref="TextToDisplay"/>
        /// </summary>
        /// <param name="text">Text wanted to display</param>
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
