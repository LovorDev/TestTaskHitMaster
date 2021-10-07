using System;
using UnityEngine;
using UnityEngine.AI;

namespace EnemyScripts
{
    [Serializable]
    public class Health
    {
        public event Action DieEvent;
        public event Action<float> UpdatedHealthPersentEvent;

        [SerializeField]
        private float _maxHp;

        private float _hp;

        public void Init()
        {
            _hp = _maxHp;
        }

        public void DealDamage(Transform notUsed, float damage = 1)
        {
            _hp -= damage;
            UpdatedHealthPersentEvent?.Invoke(_hp / _maxHp);
            if (_hp < 0)
                DieEvent?.Invoke();
        }
    }

    public class Enemy : MonoBehaviour
    {
        /// <summary>
        /// A class that responsible for bullet collision detection
        /// </summary>
        [SerializeField]
        private Damageable _damageable;

        [SerializeField]
        private Health _health;
        
        public event Action<Enemy> Die;

        [SerializeField]
        private HealthBar _healthBar;

        [SerializeField]
        private ModelAnimationController _modelAnimationController;

        /// <summary>
        /// Activating and deactivating ragdoll for enemy on die
        /// </summary>
        [SerializeField]
        private RagdollController _ragdollController;

        /// <summary>
        /// plays an particles animation when hit
        /// </summary>
        [SerializeField]
        private ParticlesController _particlesController;

        [SerializeField]
        private NavMeshAgent _navMeshAgent;

        public NavMeshAgent NavMeshAgent => _navMeshAgent;


        private void Start()
        {
            _health.Init();
            _health.DieEvent += OnDie;
            Sub();
        }

        /// <summary>
        /// Calls when shooting spot initialize
        /// Set new destination point and play animation
        /// </summary>
        /// <param name="target">the point at which the enemy must come</param>
        public void SetTarget(Vector3 target)
        {
            _navMeshAgent.SetDestination(target);
            _modelAnimationController.Run();
        }

        /// <summary>
        /// Calls when <code>_health._hp < 0</code>
        /// Unsub all Actions
        /// Setup ragdoll
        /// Disable unnecessary
        /// </summary>
        private void OnDie()
        {
            Unsub();
            
            Die?.Invoke(this);

            _navMeshAgent.enabled=false;
            
            _modelAnimationController.Disable();
            _ragdollController.Activate();
            
            _healthBar.Disable();
            _damageable.Disable();
        }
        /// <summary>
        /// Sub and Unsub for all Actions
        /// </summary>
        #region SubscribingActions

        private void Sub()
        {
            _damageable.HitEvent += _health.DealDamage;
            _damageable.HitEvent += _modelAnimationController.Hit;
            _damageable.HitEvent += _ragdollController.Punch;
            _damageable.HitEvent += _particlesController.PlayHit;
            _health.UpdatedHealthPersentEvent += _healthBar.UpdateHpBar;
            
        }

        private void Unsub()
        {
            _damageable.HitEvent -= _health.DealDamage;
            _damageable.HitEvent -= _modelAnimationController.Hit;
            _damageable.HitEvent -= _ragdollController.Punch;
            _damageable.HitEvent -= _particlesController.PlayHit;
            _health.UpdatedHealthPersentEvent -= _healthBar.UpdateHpBar;
        }

        #endregion

    }
}