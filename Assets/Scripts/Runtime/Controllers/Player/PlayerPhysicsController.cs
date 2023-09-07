using System;
using DG.Tweening;
using Runtime.Signals;
using UnityEngine;

namespace Runtime.Controllers.Player
{
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Serialized Variables

        [SerializeField] private new Rigidbody managerRigidbody;
        [SerializeField] private new Collider collider;
        



        #endregion

        #region Private

        private readonly string _obstacle = "Obstacle";
        private readonly string _atm = "ATM";
        private readonly string _collectable = "Collectable";
        private readonly string _minigameArea = "MiniGameArea";

        #endregion
        #endregion

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_obstacle))
            {
                managerRigidbody.transform.DOMoveZ(managerRigidbody.transform.position.z - 10f, 1f)
                    .SetEase(Ease.OutBack);
                return;
            }

            if (other.CompareTag(_atm))
            {
                CoreGameSignals.Instance.onAtmTouched?.Invoke(other.gameObject);
                return;
            }
            if (other.CompareTag(_collectable))
            {
                other.tag = "Collected";
                //signals collactable
                return;
            }

            if (other.CompareTag(_minigameArea))
            {
                CoreGameSignals.Instance.onMiniGameEntered?.Invoke();
                return;
            }
        }
    }
}