using System;
using BodyScripts;
using UnityEngine;
using UnityEngine.Serialization;

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
    public struct BonusToGivePlayer
    {
        public float givenPointsToThePlayer;
        public float givenMoneyToThePlayer;
        public float givenHappinessToPlayer;
    }
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private EnemyType enemyType;
        [SerializeField] private float damageAmount;
        [SerializeField] private float maxHp;
        [FormerlySerializedAs("moneyAndPointInformation")] [SerializeField] private BonusToGivePlayer bonusToGivePlayer;
        [SerializeField] private GameObject deadBodyPrefab;
        
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

        public BonusToGivePlayer GetMoneyAndPointInformation()
        {
            return bonusToGivePlayer;
        }

        public void DestroyEnemy()
        {
            GameObject tempGameObject = Instantiate(deadBodyPrefab, transform.position, transform.rotation);
            tempGameObject.GetComponent<Body>().GivenHappinessLevelToPlayer = bonusToGivePlayer.givenHappinessToPlayer;
            Destroy(gameObject);
        }
    }
}
