using UnityEngine;

namespace EnemyScripts
{
    public class RagdollController : MonoBehaviour
    {
        private Rigidbody[] _rigidbodies;

        [SerializeField]
        private Rigidbody mainRigidbody;

        [SerializeField]
        private float _punchForce=4000;

        private void Start()
        {
            _rigidbodies = GetComponentsInChildren<Rigidbody>(true);
            Deactivate();
        }

        [ContextMenu("Activate")]
        public void Activate()
        {
            foreach (var rb in _rigidbodies)
            {
                rb.isKinematic = false;
                rb.GetComponent<Collider>().enabled = true;
            }
        }

        [ContextMenu("Punch")]
        public void TestPunch()
        {
            var ob = new GameObject();
            ob.transform.position = transform.position + Vector3.one;
            Punch(ob.transform, 1);
        }
        /// <summary>
        /// Calls when enemy hittng
        /// </summary>
        /// <param name="tf">Transform that came from </param>
        /// <param name="f"> damage, not used. Can be used for controlling force power</param>
        public void Punch(Transform tf, float f)
        {
            var direction = (transform.position - tf.position).normalized+Vector3.up;
            
            mainRigidbody.AddForce(direction*_punchForce);
        }

        private void Deactivate()
        {
            foreach (var rb in _rigidbodies)
            {
                rb.isKinematic = true;
                rb.GetComponent<Collider>().enabled = false;
            }
        }
    }
}
