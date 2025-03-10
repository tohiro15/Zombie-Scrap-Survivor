using UnityEngine;

public class PlayerInventory : MonoBehaviour, IInventory
{
    private IWeapon _currentWeapon;  // Используем IWeapon, а не Weapon

    public void AddWeapon(IWeapon weapon)  // Метод принимает IWeapon
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.Unequip();  // Отключаем текущее оружие, если оно есть
        }

        _currentWeapon = weapon;
        _currentWeapon.Equip();  // Экипируем новое оружие
    }

    public IWeapon GetCurrentWeapon()  // Возвращаем IWeapon
    {
        return _currentWeapon;
    }
}
