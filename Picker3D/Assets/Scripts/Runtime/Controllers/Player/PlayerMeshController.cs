using DG.Tweening;
using Runtime.Data.ValueObjects;
using TMPro;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerMeshController : MonoBehaviour
    {
        #region Seld Variables
        [SerializeField] private new Renderer renderer;
        [SerializeField] private TextMeshPro scaleText;
        [SerializeField] private ParticleSystem confetti;
        #endregion
        
        
        private PlayerMeshData _data;
        
        internal void SetData(PlayerMeshData data)
        {
            _data = data;
        }
        internal void ScaleUpPlayer()
        {
            renderer.gameObject.transform.DOScaleX(_data.ScaleCounter, 1f).SetEase(Ease.Flash);
        }
        internal void ShowUpText()
        {
            scaleText.DOFade(1, 0).SetEase(Ease.Flash).OnComplete(() =>
            {
                scaleText.DOFade(0, 0.30f).SetDelay(0.35f);
                scaleText.rectTransform.DOAnchorPosY(1f, 0.65f).SetEase(Ease.Linear);
            });
        }

        internal void PlayConfetti()
        {
            confetti.Play();
        }

        internal void OnReset()
        {
            renderer.gameObject.transform.DOScaleX(1, 1).SetEase(Ease.Linear);
        }
    }
}