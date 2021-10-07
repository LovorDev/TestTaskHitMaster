using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CharacterScripts
{
    [RequireComponent(typeof(Image))]
    public class ClickOnEnemyHandler : MonoBehaviour, IPointerClickHandler
    {
        public event Action<Vector3> OnClick;

        [SerializeField]
        [Tooltip("The distance to which the firing point is calculated if no object is hit")]
        private float _rayPointDistance = 15;

        [SerializeField]
        private Camera Camera;

        public void OnPointerClick(PointerEventData eventData)
        {
            var ray = Camera.ScreenPointToRay(eventData.position);

            Vector3 point = ray.GetPoint(_rayPointDistance);
            
            if (Physics.Raycast(ray, out var info))
            {
                point = info.point;
            }

            OnClick?.Invoke(point);
        }


        /// <summary>
        /// Draw <code>_rayPointDistance</code>
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(Camera.transform.position, Camera.transform.position + Vector3.forward * _rayPointDistance);
        }
    }
}