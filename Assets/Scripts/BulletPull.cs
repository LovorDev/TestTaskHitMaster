using System.Collections.Generic;
using System.Linq;
using CharacterScripts;
using UnityEngine;

public class BulletPull : MonoBehaviour
{
    [SerializeField]
    private Bullet _bulletPrefab;

    [SerializeField]
    private int _bulletAmount;

    private List<Bullet> _bullets;

    private void OnValidate()
    {
        GenerateBullets();
    }

    private void GenerateBullets()
    {
        if (!_bulletPrefab) return;

        _bullets = new List<Bullet>();
        var count = transform.childCount;

        if (count > _bulletAmount)
        {
            for (int i = 0; i < count - _bulletAmount; i++)
            {
                Destroy(GetComponentsInChildren<Bullet>().Last());
            }
        }

        for (int i = 0; i < _bulletAmount - count; i++)
        {
            CreatBullet();
        }
    }

    private void Start()
    {
        if (transform.childCount <= 0) GenerateBullets();
        else
        {
            _bullets = new List<Bullet>();
            for (int i = 0; i < transform.childCount; i++)
            {
                _bullets.Add(transform.GetChild(i).GetComponent<Bullet>());
            }
        }
    }

    private Bullet CreatBullet()
    {
        var newBullet = Instantiate(_bulletPrefab, Vector3.zero, Quaternion.identity, transform);
        newBullet.gameObject.SetActive(false);
        _bullets.Add(newBullet);
        return newBullet;
    }


    public Bullet AvailableBullet
    {
        get
        {
            var bullet = _bullets.FirstOrDefault(k => k.gameObject.activeInHierarchy == false);
            if (bullet == null) bullet = CreatBullet();
            bullet.gameObject.SetActive(true);
            return bullet;
        }
    }
}