using Godot;
using System;

namespace MySystems
{

    public class ConsoleSystem : System_Base
    {
        readonly string CONSOLE_PATH = "res://Scenes/UI/Console.tscn";
        private static UI.Console _console;

        #region colors
        private readonly Color COLOR_ATTACK = Colors.Red;
        private static readonly Color COLOR_DEFAULT = Colors.White;
        private readonly Color COLOR_FAIL = Colors.Blue;
        private readonly Color COLOR_DEAD = Colors.LightSlateGray;
        #endregion

        private void Init(){
            //first, cheeck if a console already exists
            Node n = MyManager.NodeManager.GetParent();
            _console = n.TryGetFromChild_Rec<UI.Console>();

            if(_console != null){
                Messages.Print("yelooooow, is it me");
            }else{
                PackedScene  sc= GD.Load<PackedScene>(CONSOLE_PATH);
                _console = sc.Instance<UI.Console>();
                //n.CallDeferred("add_child", _console);
                MyManager.NodeManager.AddChild(_console);
            }
        }

        #region Main console methods
        public void Show(){
            _console.Show();
        }

        public void Hide(){
            _console.Hide();
        }

        public void ClearConsoleText(){
            _console.ClearConsoleText();
        }
        #endregion

        #region Write Methods (TODO: Another class?)

        public static void Write(in string mess){
            Messages.Print("yelloooow");
            _console.WriteOnConsole(mess, COLOR_DEFAULT);
        }

        #endregion


        #region System methods
        public override void OnEnterSystem(params object[] obj)
        {
            Messages.EnterSystem(this);
            this.Init();
        }

        public override void OnExitSystem(params object[] obj)
        {
            Messages.ExitSystem(this);
        }
        #endregion
    }
}
