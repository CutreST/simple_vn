using Godot;
using MySystems;
using MySystems.VisualNovel;
using System;
using System.Collections.Generic;

namespace UI.Dialog
{
    /// <summary>
    /// Class that displays all the choices
    /// </summary>
    public class ChoiceDisplay : Control
    {
        /// <summary>
        /// Name of the container.
        /// </summary>
        [Export]
        private readonly string CONTAINER_NAME;

        /// <summary>
        /// The <see cref="ChoiceSystem"/> where this is attached
        /// </summary>
        public ChoiceSystem ChoiceSys { get; set; }

        /// <summary>
        /// The buttons loaded but not used 'cause we use a pool
        /// </summary>
        /// <remarks>
        /// TODO: generic class for pools
        /// </remarks>
        private List<ChoiceButton> _unusedButtons;

        /// <summary>
        /// The buttons that are currently used.
        /// </summary>
        private List<ChoiceButton> _usedButtons;

        /// <summary>
        /// Default button for the choices
        /// </summary>
        ChoiceButton _original_btn;

        /// <summary>
        /// The container box.
        /// </summary>
        VBoxContainer _container;

        /// <summary>
        /// TODO: create a button input for the buttons. 
        /// </summary>

        public override void _EnterTree()
        {
            base._EnterTree();
            _container = this.TryGetFromChild_Rec<VBoxContainer>(CONTAINER_NAME);

            _unusedButtons = new List<ChoiceButton>();
            _usedButtons = new List<ChoiceButton>();

            //check for children buttons and add
            ChoiceButton button;
            for (int i = 0; i < _container.GetChildCount(); i++)
            {
                if (_container.GetChild(i).GetType() == typeof(ChoiceButton))
                {
                    button = _container.GetChild<ChoiceButton>(i);
                    _unusedButtons.Add(button);
                    _original_btn = button;
                    button.Hide();
                }
            }

            if (ChoiceSys == null)
            {
                //create the system
                ChoiceSystem ch;
                System_Manager.GetInstance(this).TryGetSystem<ChoiceSystem>(out ch, true);
                ChoiceSys = ch;
            }

        }

        /// <summary>
        /// Show the choices on the screen
        /// </summary>
        /// <param name="choices">A list of <see cref="Entities.BehaviourTree.VN_Nodes.ChoiceRoute"/>
        /// that are the choices</param>
        public void ShowChoices(in List<Entities.BehaviourTree.VN_Nodes.ChoiceRoute> choices)
        {
            base.Show();

            for (int i = 0; i < choices.Count; i++)
            {
                //spawn button;
                this.SpawnButton(choices[i].Text, i);
            }
        }

        /// <summary>
        /// Hides this ui menu
        /// </summary>
        public void MyHide()
        {
            base.Hide();

            //hides all buttons
            for (int i = _usedButtons.Count - 1; i > -1; i--)
            {
                _usedButtons[i].Text = "";
                _usedButtons[i].Hide();
                _unusedButtons.Add(_usedButtons[i]);
                _container.RemoveChild(_usedButtons[i]);
                _usedButtons.RemoveAt(i);
            }
        }

        /// <summary>
        /// Spawns a new choice button
        /// </summary>
        /// <param name="text">The text of the choice</param>
        /// <param name="index">The index of the button at <see cref="_usedButtons"/></param>
        private void SpawnButton(in string text, in int index)
        {
            ChoiceButton temp;
            if (_unusedButtons.Count == 0)
            {
                temp = (ChoiceButton)_original_btn.Duplicate();
            }
            else
            {
                temp = _unusedButtons[0];
                _unusedButtons.RemoveAt(0);
            }
            _container.AddChild(temp);

            _usedButtons.Add(temp);
            temp.Show();
            temp.Text = text;
            temp.Index = index;
        }

    }
}
