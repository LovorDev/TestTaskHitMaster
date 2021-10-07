using System.Collections;
using CharacterScripts;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ExplosionBarrel : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Explosion radius")]
    private float _radius = 2;

    [SerializeField]
    private ParticleSystem _explosion;

    [SerializeField]
    private MeshRenderer _meshRenderer;

    /// <summary>
    /// Explosion area
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.82f, 0.21f, 0.13f, 0.2f);
        Gizmos.DrawSphere(transform.position, _radius / 1.33f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Bullet>(out var bullet))
            StartCoroutine(OnBulletHit());
    }

    private IEnumerator OnBulletHit()
    {
        var component = gameObject.AddComponent<SphereCollider>();
        component.radius = _radius;
        component.center = Vector3.zero;
        component.isTrigger = true;
        
        
        _explosion.Play();
        _meshRenderer.enabled = false;
        yield return new WaitForSeconds(.2f); //the time it takes for the colliders to detect it
        component.enabled = false;
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}