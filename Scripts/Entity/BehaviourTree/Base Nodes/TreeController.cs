using Base.Interfaces;
using Godot;
using System;
using System.Collections.Generic;
using Entities.Components;
using MySystems;

//using Entities.BehaviourTree.Loaders;

namespace Entities.BehaviourTree
{
    /// <summary>
    /// The behaviour tree controller
    /// <remarks>
    /// There's work to do, i think. But it works for what we need.
    /// </remarks>
    /// </summary>
    public class TreeController : Node, IComponentNode, IPhysic
    {
        /// <summary>
        /// The root node.
        /// </summary>
        private BaseNode _root;

        public BaseNode Root{
            get => _root;
            set{
                if(_root != null){
                    _root.QueueFree();
                }
                _root = value;
                this.InitRoot();
                ConsoleSystem.Write("New root on Behaviour Tree");
            }
        }

        /// <summary>
        /// The current node. At some points this is going to be a stack, so, we 
        /// can keep track of the running onoes
        /// </summary>
        private BaseNode _currentNode;

        private Stack<BaseNode> _nodeStack;
        public Entity MyEntity { get; set; }

        #region Godot MEthods
        public override void _EnterTree()
        {
            base._EnterTree();
            this.OnAwake();

        }

        public override void _ExitTree()
        {
            base._ExitTree();
            this.OnSetFree();
        }
        #endregion

        /// <summary>
        /// Gets the root. The root is a child RootLoader
        /// </summary>
        /// <returns>got the root?</returns>
        private bool GetRoot()
        {
            _root = base.GetChild(0) as BaseNode;
            if(_root == null){
                return false;
            }
            this.InitRoot();
            return true;
        }

        private void InitRoot(){
            _root.InitNode(this);
            EnterNode(_root);
            _currentNode = _root;
        }

        public void EnterNode(in BaseNode node)
        {
            _currentNode = node;                      
        }

        Queue<BaseNode> Q;

        public void ExitNode(in BaseNode node){
            
            if(node.NodeState == States.RUNNING){
                _currentNode = node;
            }
        }       

        public void MyPhysic(in float delta)
        {
            _currentNode.Tick(this);
        }

        public void Ontart()
        {

        }

        public void OnAwake()
        {
            MyEntity = this.TryGetFromParent_Rec<Entity>();

            GD.Print("Entity is null at Tree controller:", MyEntity == null);

            if (GetRoot())
            {
                MySystems.InGameSys game;

                MySystems.System_Manager.GetInstance(this).TryGetSystem<MySystems.InGameSys>(out game, true);
                game.AddToPhysic(this);
                MySystems.System_Manager.GetInstance(this).AddToStack(game);
            }
        }

        public void OnSetFree()
        {
            _currentNode = null;
            _root = null;
            MyEntity = null;

            MySystems.InGameSys game;

            MySystems.System_Manager.GetInstance(this).TryGetSystem<MySystems.InGameSys>(out game, true);
            game.RemoveFromPhysic(this);

        }

        public void Reset()
        {

        }
    }
}
