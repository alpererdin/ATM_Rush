using Runtime.Enums;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {

        #region self Variables

        #region SerilaziedVariables

        [SerializeField] private Animator animator;
        

        #endregion

        #endregion
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onChangePlayerAnimationState += OnChangeAnimationState;
        }

        private void OnChangeAnimationState(PlayerAnimationStates animationState)
        {
            animator.SetTrigger(animationState.ToString());
        }
        private void UnsubscribeEvents()
        {
            PlayerSignals.Instance.onChangePlayerAnimationState -= OnChangeAnimationState;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        internal void OnReset()
        {
            PlayerSignals.Instance.onChangePlayerAnimationState?.Invoke(PlayerAnimationStates.Idle);
        }
    }
}