using System;
using UnityEngine;
using Button = UnityEngine.UI.Button;

namespace Sc.GeneralSystem
{
    public class JoystickSystem : MonoBehaviour
    {
        public static JoystickSystem Instance { get; private set; }

        public FixedJoystick movementJoystick;
        public FixedJoystick attackJoystick;
        public Button attackButton;
        
        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public static void SetAttackJoystick(bool changeJoystick) // if true attackJoystick true attackButton false
        {
            Instance.attackJoystick.gameObject.SetActive(changeJoystick);
            Instance.attackButton.gameObject.SetActive(!changeJoystick);
        }
    }
}
