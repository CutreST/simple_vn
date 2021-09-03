using Godot;
using MySystems;
using System;
using System.Collections.Generic;

namespace UI.Dialog
{

    public class ChoiceDisplay : Control
    {

        [Export]
        private readonly string CONTAINER_NAME;

        public ChoiceSystem ChoiceSys{ get; set; }

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
            for (int i = 0; i < _container.GetChildCount(); i++){
                if(_container.GetChild(i).GetType() == typeof(ChoiceButton)){
                    button = _container.GetChild<ChoiceButton>(i);
                    _unusedButtons.Add(button);
                    _original_btn = button;
                    button.Hide();
                }
            }

            if(ChoiceSys == null){
                //creamos el sistema
                ChoiceSystem ch;
                System_Manager.GetInstance(this).TryGetSystem<ChoiceSystem>(out ch, true);
                ChoiceSys = ch;
            }
            this.TestChoices();
        }

        private void TestChoices(){
            List<string> testA = new List<string>();
            testA.Add("Choice one!");
            testA.Add("Choice two!");
            testA.Add("Choice three!");
            testA.Add("Choice four!");

            this.ShowChoices(testA);
        }

        public void ShowChoices(in List<string> choices){
            for (int i = 0; i < choices.Count; i++)
            {
                //spawn button;
                this.SpawnButton(choices[i], i);
            }
        }

        private void SpawnButton(in string text, in int index){
            ChoiceButton temp;
            if(_unusedButtons.Count == 0){
                temp = (ChoiceButton)_original_btn.Duplicate();
                _container.AddChild(temp);
            }else{
                temp = _unusedButtons[0];
                _unusedButtons.RemoveAt(0);
            }

            _usedButtons.Add(temp);
            temp.Show();
            temp.Text = text;
            temp.Index = index + 1;
        }

    }
}
