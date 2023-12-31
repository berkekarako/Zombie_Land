using System;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

namespace Sc.GeneralSystem
{
    public class CUISystem : MonoBehaviour
    {
        public static CUISystem Instance { get; private set; }

        public FixedJoystick movementJoystick;
        public FixedJoystick attackJoystick;
        public Button attackButton;
        public Slider playerReloadSlider;
        public Image playerReloadImage;
        
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