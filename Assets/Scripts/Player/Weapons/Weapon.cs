using UnityEngine;

public class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] private string _weaponName;  // ��� ������

    public string WeaponName => _weaponName;  // ��������� �������� ��� ��������� �����

    public void Equip()
    {
        // ������ ��� ���������� ������
        Debug.Log($"{_weaponName} equipped!");
    }

    public void Unequip()
    {
        // ������ ��� ������ ������
        Debug.Log($"{_weaponName} unequipped!");
    }

    public void Use()
    {
        // ������ ������������� ������
        Debug.Log($"{_weaponName} used!");
    }
}
