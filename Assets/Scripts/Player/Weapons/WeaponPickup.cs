using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    private IWeapon weapon;

    private void Start()
    {
        weapon = GetComponent<IWeapon>();  // �������� ���������, ����������� ��������� IWeapon
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IInventory playerInventory = other.GetComponent<IInventory>();
            if (playerInventory != null && weapon != null)
            {
                playerInventory.AddWeapon(weapon);  // ��������� ������ � ��������� ������
                Destroy(gameObject);  // ���������� ������ ������ (�����)
            }
        }
    }
}
