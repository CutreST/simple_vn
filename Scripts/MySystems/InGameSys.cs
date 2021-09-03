using Godot;
using MySystems.MyInput;
using System;

namespace MySystems
{
    /// <summary>
    /// The InGame system, resposible of the updates of the objects and the input (at some point)
    /// </summary>
    public class InGameSys : Visual_SystemBase
    {              


        /// <summary>
        /// The Ingame input. I think that maybe the inputs has to be an interface instead of
        /// having the <see cref="InputBase"/> on the <see cref="Visual_SystemBase"/>
        /// </summary>

        public static float GameTime { get; private set; }

        #region Constructor
        public InGameSys() : base(null, null)
        {

        }

        public InGameSys(in Node go, in System_Manager manager) : base(go, manager)
        {

        }     
        #endregion

        #region System
        public override void OnEnterSystem(params object[] obj)
        {
            Messages.EnterSystem(this);

        }
        public override void OnExitSystem(params object[] obj)
        {
            Messages.ExitSystem(this);
            LateObjs.Clear();
            UpdateObjs.Clear();
            PhysicObjs.Clear();          

            if(GO != null)
                GO.QueueFree();
        }
        #endregion

        #region Stack
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

        #region Update

        public override void MyUpdate(in float delta)
        {
            //add time to the game time
            GameTime += delta;
            //first, check input, then, look for all the update
            
            base.MyUpdate(delta);
        }

        #endregion

      
    }
}
