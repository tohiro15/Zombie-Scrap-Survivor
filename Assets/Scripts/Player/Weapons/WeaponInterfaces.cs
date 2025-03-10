public interface IWeapon
{
    string WeaponName { get; }  // Свойство для имени оружия
    void Equip();
    void Unequip();
    void Use();
}

public interface IInventory
{
    void AddWeapon(IWeapon weapon);  // Принимаем интерфейс IWeapon, а не класс Weapon
    IWeapon GetCurrentWeapon();  // Возвращаем IWeapon, а не конкретный класс Weapon
}

