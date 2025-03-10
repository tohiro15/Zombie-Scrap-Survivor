using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    private IWeapon weapon;

    private void Start()
    {
        weapon = GetComponent<IWeapon>();  // Получаем компонент, реализующий интерфейс IWeapon
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IInventory playerInventory = other.GetComponent<IInventory>();
            if (playerInventory != null && weapon != null)
            {
                playerInventory.AddWeapon(weapon);  // Добавляем оружие в инвентарь игрока
                Destroy(gameObject);  // Уничтожаем объект оружия (пикап)
            }
        }
    }
}
