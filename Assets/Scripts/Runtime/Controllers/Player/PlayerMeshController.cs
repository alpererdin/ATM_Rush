using System;
using Runtime.Signals;
using Sirenix.OdinInspector.Editor;
using TMPro;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerMeshController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private TextMeshPro scoreText;

        #endregion

        #endregion

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onSetTotalScore += OnSetTotalScore;
        }

        public void OnSetTotalScore(int value)
        {
            scoreText.text = value.ToString();
        }
        private void UnSubscribeEvents()
        {
            PlayerSignals.Instance.onSetTotalScore -= OnSetTotalScore;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
            
        }
    }
}