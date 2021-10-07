using System;
using System.Collections.Generic;
using EnemyScripts;
using UnityEngine;

namespace ShootingSpotsScript
{
    /// <summary>
    /// Enemies on shooting spot
    /// </summary>
    [Serializable]
    public class IslandsEnemies
    {
        [SerializeField]
        [Tooltip("AllEnemies on this shooting spot")]
        private List<Enemy> _enemies;

        public event Action AllEnemiesDieEvent;

        public bool Exist => _enemies.Count != 0;

    
        /// <param name="targetPosition">waypoint where the enemy is heading</param>
        public void Init(Vector3 targetPosition)
        {
        
            foreach (var enemy in _enemies)
            {
                enemy.Die += OnEnemyDie;
                enemy.SetTarget(targetPosition);
            }
        }


        private void OnEnemyDie(Enemy sender)
        {
            _enemies.Remove(sender);
            sender.Die -= OnEnemyDie;

            if (!Exist)
                AllEnemiesDieEvent?.Invoke();
        }
    
    }


    /// <summary>
    /// One shooting spot contain info about all Enemies and Waypoint
    /// </summary>
    public class ActionIsland : MonoBehaviour
    {
        [SerializeField]
        private IslandsEnemies _islandsEnemies;

    
    
        ///It is recommended to place at a distance of <code>ClickOnEnemyHandler._rayPointDistance</code> 
        [SerializeField]
        [Tooltip("Look at the ray from the camera and place it at an approximate distance")]
        private Transform _wayPoint;

        public Vector3 WayPoint => _wayPoint.position;
    
        /// <summary>
        /// calls when all enemy dead or doesn't exist
        /// </summary>
        public event Action<ActionIsland> IslandCompleteEvent;

        public void StartInteraction()
        {
            if (!_islandsEnemies.Exist)
                IslandCompleteEvent?.Invoke(this);

            _islandsEnemies.Init(WayPoint);
            _islandsEnemies.AllEnemiesDieEvent += OnAllIslandEnemiesDead;

        }
    

        private void OnAllIslandEnemiesDead()
        {
            _islandsEnemies.AllEnemiesDieEvent -= OnAllIslandEnemiesDead;

            IslandCompleteEvent?.Invoke(this);
        }
    }
}