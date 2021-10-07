using System;
using CharacterScripts;
using UnityEngine;

namespace EnemyScripts
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    public class Damageable : MonoBehaviour
    {
        //float - Damage , Transform - sender
        public event Action<Transform,float> HitEvent;
        
        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Bullet>(out var bullet))
                HitEvent?.Invoke(bullet.transform,bullet.Damage);
            
            if (other.TryGetComponent<ExplosionBarrel>(out var barrel))
                HitEvent?.Invoke(barrel.transform,10000);
        }
        
        public void Disable()
        {
            GetComponent<Collider>().enabled = false;
        }
    }
}
