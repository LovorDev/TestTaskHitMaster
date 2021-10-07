using System;
using System.Collections.Generic;
using UnityEngine;

namespace ShootingSpotsScript
{
    /// <summary>
    /// Contain info about all shooting spots
    /// </summary>
    public class ActionIslandsQueue : MonoBehaviour
    {
        [SerializeField]
        private List<ActionIsland> _actionIslands;

        public event Action AllIslandsComplete;

        public ActionIsland Peek => _actionIslands.Count > 0 ? _actionIslands[0] : null;

        private void Start()
        {
            foreach (var actionIsland in _actionIslands)
            {
                actionIsland.IslandCompleteEvent += OnActionIslandCompleted;
            }
        }

        private void OnActionIslandCompleted(ActionIsland island)
        {
            _actionIslands.Remove(island);
            island.IslandCompleteEvent -= OnActionIslandCompleted;
        
            if(_actionIslands.Count==0)    
                AllIslandsComplete?.Invoke();
        }
    }
}