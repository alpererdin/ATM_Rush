using System;
using DG.Tweening;
using Runtime.Signals;
using TMPro;
using UnityEngine;

namespace Runtime.Managers
{
    public class AtmManager : MonoBehaviour
    {
        #region self variables

        #region serialized variables

        
        [SerializeField]private DOTweenAnimation doTweenAnimation;

        [SerializeField] private TextMeshPro scoreText;

        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            doTweenAnimation = GetComponentInChildren<DOTweenAnimation>();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onAtmTouched += OnAtmTouched;
            AtmSignals.Instance.onSetAtmScoreText += OnSetAtmScoreText;
        }

      
        private void OnAtmTouched(GameObject touchedATMObject)
        {
            if (touchedATMObject.GetInstanceID() == gameObject.GetInstanceID())
            {
                doTweenAnimation.DOPlay();
            }
        }
        private void OnSetAtmScoreText(int value)
        {
            scoreText.text = value.ToString();
        }


        private void UnSubscribeEvents()
        {
            throw new NotImplementedException();
        }
        private void OnDisable()
        {
            UnSubscribeEvents();
        }

      
    }
}