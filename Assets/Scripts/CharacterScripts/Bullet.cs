using System.Collections;
using UnityEngine;

namespace CharacterScripts
{
    public class Bullet : MonoBehaviour
    {
        private float _damage;

        [SerializeField]
        [Tooltip("the time the bullet died if it didn't hit anything")]
        private float _liveTime = 10;

        public float Damage => _damage;

        /// <summary>
        /// Main function when calculating the bullet direction and calling the fly method 
        /// </summary>
        /// <param name="from">start position</param>
        /// <param name="to">end position</param>
        /// <param name="bD">contain speed and damage</param>
        public void Fly(Vector3 from, Vector3 to, BulletData bD)
        {
            _damage = bD.Damage;
            transform.position = from;
            var direction = (to - from).normalized;

            StartCoroutine(FlyUpdate(direction, bD.Speed));
        }

        /// <summary>
        /// Used instead of the usual Update function
        /// Moving the bullet to direction
        /// If time end disable it
        /// </summary>
        private IEnumerator FlyUpdate(Vector3 direction,float speed)
        {
            var time = 0f;

            while (time<_liveTime)
            {
                transform.Translate(direction * (speed * Time.deltaTime));
                yield return null;
                time += Time.deltaTime;
            }
            Disable();
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            Disable();
        }
    }
}