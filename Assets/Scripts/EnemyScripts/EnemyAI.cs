using System;
using UnityEngine;

namespace EnemyScripts
{
    enum EnemyType
    {
        Melee = 0,
        Ranged = 1,
        MeleeBoss = 2,
        RangedBoss = 3,
    }

    [System.Serializable]
    public struct MoneyAndPointInformation
    {
        public float givenPointsToThePlayer;
        public float givenMoneyToThePlayer;
    }
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private EnemyType enemyType;
        [SerializeField] private float damageAmount;
        [SerializeField] private float maxHp;
        [SerializeField] private MoneyAndPointInformation moneyAndPointInformation;
        
        //TODO: Add animation control for attack status

        private float currentHp;

        //TODO: For enemy hp bar
        public float CurrentHp => currentHp;

        private void Start()
        {
            currentHp = maxHp;
        }

        public void LowerHealth(float decreaseAmount)
        {
            currentHp -= decreaseAmount;
        }

        public bool CheckIsDead()
        {
            return currentHp <= 0;
        }

        public MoneyAndPointInformation GetMoneyAndPointInformation()
        {
            return moneyAndPointInformation;
        }

        public void DestroyEnemy()
        {
            //TODO: Also spawn body prefab here
            Destroy(gameObject);
        }
    }
}
