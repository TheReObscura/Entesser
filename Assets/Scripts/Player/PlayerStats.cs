using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [System.Serializable]
    public class PlayerStats
    {
        // СТАРТОВЫЕ ХАРАКТЕРИСТИКИ
        private const float baseStrength = 1f;
        private const float baseMagic = 1f;
        private const float baseSurvival = 1f;

        // ОСНОВНЫЕ ХАРАКТЕРИСТИКИ
        public float strength = baseStrength;
        public float magic = baseMagic;
        public float survival = baseSurvival;

        // БАЗОВЫЕ ПАРАМЕТРЫ
        public float baseHP = 100f;
        public float baseMana = 50f;
        public float baseDamage = 10f;

        // ТЕКУЩИЕ ЗНАЧЕНИЯ
        public float currentHP;
        public float currentMana;
        // Для регенерации
        public float manaRegenRate = 2f;
        private float regenTimer;

        // ОЧКИ УЛУЧШЕНИЯ
        public int upgradePoints = 0;

        // ПРОИЗВОДНЫЕ ХАРАКТЕРИСТИКИ
        public float PhysicalDamage => baseDamage + strength * 2f;
        public float MagicDamage => magic * 3f;
        public float MaxHP => baseHP + survival * 15f;
        public float PhysicalResistance => survival * 0.5f;
        public float MagicResistance => survival * 0.3f;
        public float MaxMana => baseMana + magic * 10f;
        public float CritChance => strength * 0.3f;
        public float CritDamage => 150f + strength * 2f;

        public void Init()
        {
            currentHP = MaxHP;
            currentMana = MaxMana;
        }

        public void StatUpdate(float deltaTime)
        {
            RegenerateMana(deltaTime);
        }

        public void TakePhysicalDamage(float dmg)
        {
            dmg -= PhysicalResistance;

            if (dmg < 1f)
                dmg = 1f;

            currentHP -= dmg;

            if (currentHP < 0)
                currentHP = 0;
        }
        public void TakeMagicDamage(float dmg)
        {
            dmg -= MagicResistance;

            if (dmg < 1f)
                dmg = 1f;

            currentHP -= dmg;

            if (currentHP < 0)
                currentHP = 0;
        }

        public void Heal(float amount)
        {
            currentHP += amount;

            if (currentHP > MaxHP)
                currentHP = MaxHP;
        }

        public void UseMana(float cost)
        {
            currentMana -= cost;

            if (currentMana < 0)
                currentMana = 0;
        }
        public void RegenerateMana(float deltaTime)
        {
            regenTimer += deltaTime;

            if (regenTimer >= 1f)
            {
                currentMana += manaRegenRate;
                regenTimer = 0f;

                if (currentMana > MaxMana)
                    currentMana = MaxMana;
            }
        }
        public void RestoreMana(float amount)
        {
            currentMana += amount;

            if (currentMana > MaxMana)
                currentMana = MaxMana;
        }
        public void IncreaseStrength()
        {
            if (upgradePoints <= 0)
                return;

            strength += 1;
            upgradePoints--;
        }
        public void IncreaseMagic()
        {
            if (upgradePoints <= 0)
                return;

            float oldMaxMana = MaxMana;

            magic += 1;
            upgradePoints--;

            currentMana += MaxMana - oldMaxMana;
        }
        public void IncreaseSurvival()
        {
            if (upgradePoints <= 0)
                return;

            float oldMaxHP = MaxHP;

            survival += 1;
            upgradePoints--;

            currentHP += MaxHP - oldMaxHP;
        }
        public void DecreaseStrength()
        {
            if (strength <= baseStrength)
                return;

            strength -= 1;
            upgradePoints++;
        }
        public void DecreaseMagic()
        {
            if (magic <= baseMagic)
                return;

            magic -= 1;
            upgradePoints++;
        }
        public void DecreaseSurvival()
        {
            if (survival <= baseSurvival)
                return;

            survival -= 1;
            upgradePoints++;
        }
        public void ResetStats()
        {
            upgradePoints += (int)(
                (strength - baseStrength) +
                (magic - baseMagic) +
                (survival - baseSurvival)
            );

            strength = baseStrength;
            magic = baseMagic;
            survival = baseSurvival;

            currentHP = MaxHP;
            currentMana = MaxMana;
        }
        public float CalculateDamage()
        {
            float dmg = PhysicalDamage;

            bool isCrit = UnityEngine.Random.value < CritChance / 100f;

            if (isCrit)
            {
                dmg *= CritDamage / 100f;
            }

            return dmg;
        }
    }

}
