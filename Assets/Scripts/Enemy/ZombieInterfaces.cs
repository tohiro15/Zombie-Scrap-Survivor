using UnityEngine;

    public interface IMovable
    {
        void Move(Vector3 direction);
    }

    public interface IAttackable
    {
        void Attack(IDamageable target);
    }
