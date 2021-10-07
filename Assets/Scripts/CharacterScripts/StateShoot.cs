using ShootingSpotsScript;
using UnityEngine;

namespace CharacterScripts
{
    public class StateShoot : ICharacterState
    {
        private MainControl _control;
        private ActionIsland _currentLevel;

        
        /// <summary>
        /// The constructor in which: applied animation , initialized new shooting spot and sub for an event on a completed stage
        /// </summary>
        /// <param name="mainControl"></param>
        public StateShoot(MainControl mainControl)
        {
            _control = mainControl;
            _control.ModelAnimationController.Idle();
            
            if(_control.CurrentIsland==null)
                return;
            _currentLevel =_control.CurrentIsland; 
            _currentLevel.StartInteraction();
            _currentLevel.IslandCompleteEvent += OnCompleteLevel;
        }
        
        /// <summary>
        /// Calling shoot and play animation
        /// </summary>
        public void OnClick(MainControl mainControl, Vector3 point)
        {
            _control = mainControl;
            _control.Shootable.Shoot(mainControl.PointFromShoot,point);
            _control.ModelAnimationController.Shoot();
            
        }

        //NotUsed
        public void Update()
        {
        }

        /// <summary>
        /// Calls when all enemies dead
        /// </summary>
        private void OnCompleteLevel(ActionIsland island)
        {
            if(_currentLevel==null)
                return;
            
            _currentLevel.IslandCompleteEvent -= OnCompleteLevel;
            _control.State = new StateIdle(_control);
        }
    }
}