using UnityEngine;

namespace CharacterScripts
{
    public class RunState : ICharacterState
    {
        private MainControl _control;

        public RunState(MainControl mainControl)
        {
            _control = mainControl;
            _control.ModelAnimationController.Run();
        }

        //Not used
        public void OnClick(MainControl state, Vector3 point)
        {
        }

        /// <summary>
        /// Checking if we have reached a new waypoint
        /// <code>_control.NavMeshAgent.pathPending</code> this need to prevent checking before there is a new point
        /// </summary>
        public void Update()
        {
            if (_control.NavMeshAgent.pathPending)
                return;
            if (_control.NavMeshAgent.remainingDistance < .1f)
                _control.State = new StateShoot(_control);
        }
    }
}