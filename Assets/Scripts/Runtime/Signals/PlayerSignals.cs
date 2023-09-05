using Runtime.Controllers.Player;
using Runtime.Extentions;
using UnityEngine;
using UnityEngine.Events;
using Runtime.Enums;

namespace Runtime.Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        public UnityAction<PlayerAnimationStates> onChangePlayerAnimationState= delegate{  };
        public UnityAction<bool> onPlayConditionChanged= delegate{  };
        public UnityAction<bool> onMoveConditionChanged= delegate{  };
        public UnityAction<int> onSetTotalScore= delegate{  };
        
    }

   
}