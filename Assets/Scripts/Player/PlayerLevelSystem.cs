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

        public Player player;
        void Start()
        {
            player.stats.Init();
        }
        void Update()
        {
            if (DebugInputCont.instance.GiveXP())
            {
                AddXP(100);
            }
        }

        public void AddXP(int amount)
        {
            if (level >= maxLevel) return;
            int needed = XPToNextLevel();
            currentXP += amount; 
            while (currentXP >= needed && level < maxLevel) 
            { 
                currentXP -= needed; 
                LevelUp(); 
            }
        }

        public  int XPToNextLevel()
        {
            return baseXP + (level * stepXP);
        }

        void LevelUp()
        {
            level++;

            player.stats.upgradePoints += 1;
            player.stats.upgradePointsSpells += 1;

            Debug.Log("Level Up! Level: " + level);
        }
    }
}