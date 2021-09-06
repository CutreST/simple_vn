using Godot;
using MySystems;
using System;
using System.Collections.Generic;

namespace Entities.BehaviourTree.VN_Nodes
{

    public class ChoiceSelector : BaseNode
    {
        
        //private readonly string CHOICE_GROUP_ID -> esto no es necesario, solo opt
        public List<ChoiceRoute> Routes{ get; private set; }

        public int Selection;

        public ChoiceSelector()
        {
            Routes = new List<ChoiceRoute>();
        }
        public override void InitNode(in TreeController controller)
        {
            //pillar children
            int count = base.GetChildCount();
            if (count == 0)
            {
                ConsoleSystem.Write(base.Name + " didn't get any children and now is lonely :(.");
                return;
            }

            ChoiceRoute temp;
            for (int i = 0; i < count; i++)
            {
                temp = base.GetChild(i) as ChoiceRoute;

                if (temp != null)
                {
                    temp.InitNode(controller);
                    Routes.Add(temp);
                }
            }

            Selection = -1;
            //cargar el texto (lo hace esto?) 
            //|-> no, de momento lo hace el children, aquí hay optimización

        }

        public override void OnEnter(in TreeController controller)
        {
            //creo que esto lo puedo quitar
        }

        public override States Tick(in TreeController controller)
        {
            //cargamos el dialog system
            if (base.NodeState != States.RUNNING)
            {
                ChoiceSystem choice;
                System_Manager.GetInstance(this).TryGetSystem<ChoiceSystem>(out choice, true);
                choice.ShowSelection(this);
                ChangeNodeStatus(controller, States.RUNNING);
            }else{
                ChangeNodeStatus(controller, States.SUCCESS);
            }

            return base.NodeState;
            //metemos refencia con esto
            //nos veolverá un número o algo
            //le damos al hijo que toca

        }

        public override void _ExitTree()
        {
            base._ExitTree();
            Routes.Clear();
        }
    }
}
