using UnityEngine;

public class PlayerInventory : MonoBehaviour, IInventory
{
    private IWeapon _currentWeapon;  // ���������� IWeapon, � �� Weapon

    public void AddWeapon(IWeapon weapon)  // ����� ��������� IWeapon
    {
        if (_currentWeapon != null)
        {
            _currentWeapon.Unequip();  // ��������� ������� ������, ���� ��� ����
        }

        _currentWeapon = weapon;
        _currentWeapon.Equip();  // ��������� ����� ������
    }

    public IWeapon GetCurrentWeapon()  // ���������� IWeapon
    {
        return _currentWeapon;
    }
}
