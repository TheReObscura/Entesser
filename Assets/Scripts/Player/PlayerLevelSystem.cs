using Assets.Scripts.Core.Debug;
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

        Player player;
        void Start()
        {
            player.stats.Init();
            player = Player.Instance;
        }
        void Update()
        {
            if (DebugInputCont.instance.GiveXP())
            {
                AddXP(500);
            }
        }
        public void AddXP(int amount)
        {
            int needed = XPToNextLevel();

            while (currentXP >= needed && level < maxLevel)
            {
                currentXP -= needed;
                LevelUp();
                needed = XPToNextLevel();
            }
        }

        public int XPToNextLevel()
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