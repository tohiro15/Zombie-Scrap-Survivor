public interface IWeapon
{
    string WeaponName { get; }  // �������� ��� ����� ������
    void Equip();
    void Unequip();
    void Use();
}

public interface IInventory
{
    void AddWeapon(IWeapon weapon);  // ��������� ��������� IWeapon, � �� ����� Weapon
    IWeapon GetCurrentWeapon();  // ���������� IWeapon, � �� ���������� ����� Weapon
}

