using UnityEngine;

namespace CharacterScripts
{
    public class StateIdle : ICharacterState
    {
        private MainControl _control;
        private Coroutine _walkCoroutine;

        public StateIdle(MainControl mainControl)
        {
            _control = mainControl;
            _control.ModelAnimationController.Idle();
        }

        /// <summary>
        /// Set new destination to character and play animation
        /// </summary>
        /// <param name="direction">Not used</param>
        public void OnClick(MainControl mainControl, Vector3 direction)
        {
            _control = mainControl;

            if (_control.CurrentIsland == null)
                return;

            _control.NavMeshAgent.SetDestination(_control.CurrentIsland.WayPoint);
            _control.ModelAnimationController.Run();
            _control.State = new RunState(_control);
        }
        
        //NotUsed
        public void Update()
        {
        }


    }
}