using System;
using EnemyScripts;
using UnityEngine;

namespace PlayerScripts
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float damageAmount;
        [SerializeField] private Shooting.BulletType bulletType;

        private MoneyAndPointInformation moneyAndPointInformation;
        private EnemyAI enemyAI;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Equals("Enemy") && other.gameObject.GetComponent<EnemyAI>() != null)
            {
                enemyAI = other.gameObject.GetComponent<EnemyAI>();
                enemyAI.LowerHealth(damageAmount);
                if (!enemyAI.CheckIsDead())
                {
                    Destroy(gameObject);
                    return;
                }
                moneyAndPointInformation = enemyAI.GetMoneyAndPointInformation();
                GameManager.Instance.CurrentMoney += moneyAndPointInformation.givenMoneyToThePlayer;
                GameManager.Instance.CurrentPoints += moneyAndPointInformation.givenPointsToThePlayer;
                enemyAI.DestroyEnemy();
                Destroy(gameObject);
            }
        }
    }
}
