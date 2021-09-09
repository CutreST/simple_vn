using Godot;
using MySystems;
using MySystems.VisualNovel;
using System;
using System.Collections.Generic;

namespace UI.Dialog
{

    public class ChoiceDisplay : Control
    {

        [Export]
        private readonly string CONTAINER_NAME;

        public ChoiceSystem ChoiceSys { get; set; }

        List<ChoiceButton> _unusedButtons;
        List<ChoiceButton> _usedButtons;

        ChoiceButton _original_btn;
        VBoxContainer _container;

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
                //creamos el sistema
                ChoiceSystem ch;
                System_Manager.GetInstance(this).TryGetSystem<ChoiceSystem>(out ch, true);
                ChoiceSys = ch;
            }

        }

        public void ShowChoices(in List<Entities.BehaviourTree.VN_Nodes.ChoiceRoute> choices)
        {
            base.Show();

            for (int i = 0; i < choices.Count; i++)
            {
                //spawn button;
                this.SpawnButton(choices[i].Text, i);
            }
        }

        public void MyHide()
        {
            base.Hide();

            for (int i = _usedButtons.Count - 1; i > -1; i--)
            {
                _usedButtons[i].Text = "";
                _usedButtons[i].Hide();
                _unusedButtons.Add(_usedButtons[i]);
                _container.RemoveChild(_usedButtons[i]);
                _usedButtons.RemoveAt(i);
            }
        }

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
