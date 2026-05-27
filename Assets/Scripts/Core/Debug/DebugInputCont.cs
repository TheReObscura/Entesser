using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Core.Debug
{
    public class DebugInputCont : MonoBehaviour
    {

        private DebugInput debugInput;
        public static DebugInputCont instance { get; private set; }

        private void Awake()
        {
            instance = this;
            debugInput = new DebugInput();
            debugInput.Enable();
        }

        public bool DealDamage() => debugInput.Debug.DealDamage.WasPressedThisFrame();
        public bool TakeMana() => debugInput.Debug.TakeMana.WasPressedThisFrame();
        public bool Heal() => debugInput.Debug.Heal.WasPressedThisFrame();
        public bool GiveXP() => debugInput.Debug.GiveXP.WasPressedThisFrame();
        public bool SpawnItem() => debugInput.Debug.SpawnPotion.WasPressedThisFrame();

    }
}
