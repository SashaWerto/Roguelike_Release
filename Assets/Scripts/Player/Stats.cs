using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Stats : MonoBehaviour
{
        [Header("Level")]
        public AnimationCurve expCurve;
        public AnimationCurve healthCurve;
        public AnimationCurve forceCurve;
        public int level = 1;
        public float exp;
        public int skillPoints;
        [Header("Stats")]
        public float health;
        public float armor;
        public float force;
        public float equipForce;
        public float equipArmor;
        public float equipHealth;
        public Action OnAddExp;
        public Action OnRefresh;
        private static Stats stats;
        public static Stats Instance => stats; 
        private void Start()
        {
                Refresh();
        }

        private void Awake()
        {
                stats = this;
        }

        public void Refresh()
        {
                OnRefresh?.Invoke();
                int allEquipmentScore = Mathf.FloorToInt(health + armor + force + equipForce + equipArmor + equipHealth);
                if (PlayerPrefs.HasKey("lastEquipmentScore"))
                {
                       int loadedScore = PlayerPrefs.GetInt("lastEquipmentScore");
                       if (loadedScore < allEquipmentScore)
                       {
                               YandexGame.NewLeaderboardScores("EquipmentScore", allEquipmentScore);
                       }
                }
                PlayerPrefs.SetInt("lastEquipmentScore", allEquipmentScore);
        }

        public void AddExp(float value, float multiplier = 1f)
        {
                if (level >= 100)
                {
                        return;
                }
                exp += value * multiplier;
                while (exp >= expCurve.Evaluate(level))
                {
                        if (level >= 100)
                        {
                                break;
                        }
                        exp -= expCurve.Evaluate(level);
                        level++;
                        skillPoints++;
                        health += healthCurve.Evaluate(level);
                        force += forceCurve.Evaluate(level);
                }
                Refresh();
                OnAddExp?.Invoke();
        }

        public void StatAddHealth(float value)
        {
                health += value;
        }
        public void StatAddForce(float value)
        {
                force += value;
        }
        public void StatAddDefence(float value)
        {
                armor += value;
        }
}
