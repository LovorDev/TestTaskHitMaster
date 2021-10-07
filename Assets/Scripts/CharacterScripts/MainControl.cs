using ShootingSpotsScript;
using UnityEngine;
using UnityEngine.AI;

namespace CharacterScripts
{
    
    /// <summary>
    /// Main control class used for control all game process
    /// It attached to main character
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Shootable))]
    public class MainControl : MonoBehaviour
    {
        /// <summary>
        /// Main states of character
        /// <remarks> State = Shoot,Idle,Run</remarks>
        /// </summary>
        public ICharacterState State;

        /// <summary>
        /// Class with all info about shooting spot
        /// Waypoints, Enemies...
        /// </summary>
        [SerializeField]
        private ActionIslandsQueue _actionIslandsQueue;
        
        /// <summary>
        /// Just handle click to: Enemy,Another object,Empty point
        /// </summary>
        [SerializeField]
        private ClickOnEnemyHandler _clickOnEnemyHandler;

        /// <summary>
        /// Point where instantiating bullets
        /// </summary>
        [SerializeField]
        private Transform _pointFromShoot;

        
        [SerializeField]
        private ModelAnimationController _modelAnimationController;

        public ModelAnimationController ModelAnimationController => _modelAnimationController;

        public Vector3 PointFromShoot => _pointFromShoot.position;

        public Camera Camera=>Camera.main;
        /// <summary>
        /// Сlass to be able to shoot
        /// contain bullet info and link to bullet pull
        /// </summary>
        public Shootable Shootable { get; private set; }
        /// <summary>
        /// For moving to waypoints
        /// </summary>
        public NavMeshAgent NavMeshAgent { get; private set; }

        /// <summary>
        /// current shotting spot 
        /// </summary>
        public ActionIsland CurrentIsland => _actionIslandsQueue.Peek;


        private void Start()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();
            Shootable = GetComponent<Shootable>();
            State = new StateIdle(this);
            _clickOnEnemyHandler.OnClick += OnPointerClick;
        }

        private void OnDestroy()
        {
            _clickOnEnemyHandler.OnClick -= OnPointerClick;
        }

        /// <summary>
        /// Calls when touch the screen
        /// </summary>
        /// <param name="point"> calculated point where the bullet should go</param>
        private void OnPointerClick(Vector3 point)
        {
            State.OnClick(this,point);
        }

        private void Update()
        {
            State.Update();
        }
    }
}