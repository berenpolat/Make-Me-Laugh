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
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private EnemyType enemyType;
        [SerializeField] private float damageAmount;
        [SerializeField] private float maxHp;
        [SerializeField] private float givenPointsToThePlayer;
        [SerializeField] private float givenMoneyToThePlayer;
        
        //TODO: Add animation control for attack status

        private float currentHp;

        public float CurrentHp
        {
            get => currentHp;
            set => currentHp = value;
        }
    }
}
