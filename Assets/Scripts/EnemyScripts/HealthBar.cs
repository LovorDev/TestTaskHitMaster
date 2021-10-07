using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace EnemyScripts
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField]
        private Image _fillingImage;

        public void Disable()
        {
            gameObject.SetActive(false);
        }
        
        public void UpdateHpBar(float value)
        {
            _fillingImage.DOFillAmount(value, .4f).SetEase(Ease.OutBack);
        }
    }
}