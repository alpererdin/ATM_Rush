using System.Collections;
using Runtime.Controllers.Player;
using Runtime.Data.UnityObjects;
using Runtime.Data.ValueObjects;
using Runtime.Enums;
using Runtime.Keys;
using Runtime.Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Runtime.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Serialize Variables

        [SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerAnimationController animationController;
        [SerializeField] private PlayerPhysicsController pysicsController;
        [SerializeField] private PlayerMeshController meshController;

        #endregion
        #region Private Variables

        [ShowInInspector] private PlayerData _data;
        private const string PlayerDataPath = "Data/CD_Player";
         

        #endregion

        #endregion

        private void Awake()
        {
            _data = GetPlayerData();
            SendPlayerDataToControllers();
        }
        private PlayerData GetPlayerData() => Resources.Load<CD_Player>(PlayerDataPath).Data;

        private void SendPlayerDataToControllers()
        {
            movementController.SetMovementData(_data.MovementData);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            InputSignals.Instance.onInputTaken += () => PlayerSignals.Instance.onMoveConditionChanged?.Invoke(true);
            InputSignals.Instance.onInputReleased += () => PlayerSignals.Instance.onMoveConditionChanged?.Invoke(false);
            InputSignals.Instance.onInputDragged += OnInputDragged;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onLevelSuccessful +=    () => PlayerSignals.Instance.onPlayConditionChanged?.Invoke(true);
            CoreGameSignals.Instance.onLevelFailed +=    () => PlayerSignals.Instance.onPlayConditionChanged?.Invoke(false);
            CoreGameSignals.Instance.onReset += OnReset;

            //PlayerSignals.Instance.onSetTotalScore += () => PlayerSignals.Instance.OnSetTotalScore?.Invoke();
            CoreGameSignals.Instance.onMiniGameEntered += OnMiniGameEntered;
        }
        private void OnPlay()
        {
            PlayerSignals.Instance.onPlayConditionChanged?.Invoke(true);
            PlayerSignals.Instance.onChangePlayerAnimationState?.Invoke(PlayerAnimationStates.Idle);
            
        }
         
        private void OnInputDragged(HorizontalInputParams inputParams)
        {
            movementController.UpdateInputValue(inputParams);
        }
      
        private void OnMiniGameEntered()
        {
           PlayerSignals.Instance.onPlayConditionChanged?.Invoke(false);
           StartCoroutine(WaitForFinal());

        }
        
        private void OnReset()
        {

            movementController.OnReset();
            animationController.OnReset();
        }
        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= () => PlayerSignals.Instance.onMoveConditionChanged?.Invoke(true);
            InputSignals.Instance.onInputReleased -= () => PlayerSignals.Instance.onMoveConditionChanged?.Invoke(false);
            InputSignals.Instance.onInputDragged -= OnInputDragged;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onLevelSuccessful -= () => PlayerSignals.Instance.onPlayConditionChanged?.Invoke(true);
            CoreGameSignals.Instance.onLevelFailed -= () => PlayerSignals.Instance.onPlayConditionChanged?.Invoke(false);
            CoreGameSignals.Instance.onReset -= OnReset;
   
            //PlayerSignals.Instance.onSetTotalScore -= () => PlayerSignals.Instance.OnSetTotalScore?.Invoke();
            CoreGameSignals.Instance.onMiniGameEntered -= OnMiniGameEntered;
            
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
            
        }
        internal void SetStackPosition()
        {
            var position = transform.position;
            Vector2 pos = new Vector2(position.x, position.z);
           // StackSignals.Instance.onStackFollowPlayer?.Invoke(pos);
        }
        IEnumerator WaitForFinal()
        {
            PlayerSignals.Instance.onChangePlayerAnimationState?.Invoke(PlayerAnimationStates.Idle);
            yield return new WaitForSeconds(2f);
            gameObject.SetActive(false);
            CoreGameSignals.Instance.onMiniGameStart?.Invoke();
        }

        
    }
}