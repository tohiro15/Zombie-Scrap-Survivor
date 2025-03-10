using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] private string _weaponName;  // Имя оружия

    public string WeaponName => _weaponName;  // Реализуем свойство для получения имени

    public void Equip()
    {
        // Логика для экипировки оружия
        Debug.Log($"{_weaponName} equipped!");
    }

    public void Unequip()
    {
        // Логика для снятия оружия
        Debug.Log($"{_weaponName} unequipped!");
    }

    public void Use()
    {
        // Логика использования оружия
        Debug.Log($"{_weaponName} used!");
    }
}
