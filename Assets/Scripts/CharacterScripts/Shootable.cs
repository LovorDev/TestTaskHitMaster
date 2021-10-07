using System;
using UnityEngine;

namespace CharacterScripts
{
    [Serializable]
    public struct BulletData
    {
        [SerializeField]
        private float _speed;
        [SerializeField]
        private float _damage;

        public float Speed => _speed;

        public float Damage => _damage;
    }

    public class Shootable : MonoBehaviour
    {
        [SerializeField]
        private BulletPull _bulletPool;

        private Bullet _bullet => _bulletPool.AvailableBullet;
        [SerializeField]
        private BulletData _bulletData;



        public void Shoot(Vector3 from, Vector3 to)
        {
            _bullet.Fly(from, to,_bulletData);
        }
        
    }
}