using UnityEngine;

namespace EnemyScripts
{
    public class ParticlesController : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem _hitParticles;

        public void PlayHit(Transform transPos, float notUsed)
        {
            _hitParticles.transform.position = transPos.position;
            _hitParticles.Play();
        }
    }
}
