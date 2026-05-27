using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerLevelSystem : MonoBehaviour
    {
        public int level = 1;
        public int maxLevel = 23;

        public int currentXP = 0;

        public int baseXP = 100;
        public int stepXP = 25;

        public Player player;
        void Start()
        {
            player.stats.Init();
        }
        void Update()
        {
            if (GameInput.instance != null && GameInput.instance.IsDebugXP())
            {
                AddXP(500);
            }
        }
        public void AddXP(int amount)
        {
            Debug.Log("Yup i got this");
            if (level >= maxLevel) return;

            currentXP += amount;

            while (currentXP >= XPToNextLevel() && level < maxLevel)
            {
                Debug.Log("Got this too");
                currentXP -= XPToNextLevel();
                LevelUp();
            }
        }

        int XPToNextLevel()
        {
            return baseXP + (level * stepXP);
        }

        void LevelUp()
        {
            level++;

            player.stats.upgradePoints += 1;

            Debug.Log("Level Up! Level: " + level);
        }
    }
}