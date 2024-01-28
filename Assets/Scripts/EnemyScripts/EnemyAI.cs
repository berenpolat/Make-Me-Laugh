using System;
using BodyScripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace EnemyScripts
{
    enum EnemyType
    {
        Bunny = 0,
        Clown = 1,
        HugiWugi = 2,
        Bear = 3,
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
        [FormerlySerializedAs("deadBodyPrefab")] [SerializeField] private GameObject deadBodyPrefabBunny;
        [SerializeField] private GameObject deadBodyPrefabClown;
        [FormerlySerializedAs("deadBodyPrefabHuggle")] [SerializeField] private GameObject deadBodyPrefabHugiWugi;
        
        private float currentHp;

        public event Action<EnemyAI> GameFinished;

        public float CurrentHp => currentHp;

        private void Start()
        {
            currentHp = maxHp;
            if (enemyType == EnemyType.Bear)
            {
                GameObject tempObject = GameObject.FindGameObjectWithTag("GameManager");
                tempObject.GetComponent<GameManager>().BearSpawned(this);
            }
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
            GameObject tempGameObject = null;
            switch (enemyType)
            {
                case EnemyType.Bunny:
                    tempGameObject = Instantiate(deadBodyPrefabBunny, transform.position, transform.rotation);
                    break;
                case EnemyType.Clown:
                    tempGameObject = Instantiate(deadBodyPrefabClown, transform.position, transform.rotation);
                    break;
                case EnemyType.HugiWugi:
                    tempGameObject = Instantiate(deadBodyPrefabHugiWugi, transform.position, transform.rotation);
                    break;
                case EnemyType.Bear:
                    GameFinished?.Invoke(this);
                    break;
                default:
                    tempGameObject = Instantiate(deadBodyPrefabBunny, transform.position, transform.rotation);
                    break;
            }

            if (tempGameObject != null)
            {
                tempGameObject.GetComponent<Body>().GivenHappinessLevelToPlayer = bonusToGivePlayer.givenHappinessToPlayer;
            }
            Destroy(gameObject);
        }
    }
}
