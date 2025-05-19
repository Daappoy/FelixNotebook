using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public Equipment[] equipment; // Array to hold different equipment types
    public int currentEquipmentIndex = 0;
    public GameObject ShootIncicator;
    public GameObject healIndicator;
    public GameObject DividerIndicator;
    public GameObject HealthMultiplierIndicator; // Add new indicator

    void Start()
    {
        ShootIncicator.SetActive(false);
        healIndicator.SetActive(false);
        DividerIndicator.SetActive(false); // Add this line
        HealthMultiplierIndicator.SetActive(false); // Add this line

        equipment[1].gameObject.SetActive(false);
        equipment[2].gameObject.SetActive(false);
        equipment[3].gameObject.SetActive(false); // Add this line
        Equip(currentEquipmentIndex);
    }

    void Update()
    {
        HandleEquipmentSwitching();
        if (equipment[0].gameObject.activeSelf)
        {
            // Equipment at index 0 is active
            ShootIncicator.SetActive(true);
        }
        else
        {
            ShootIncicator.SetActive(false);
        }

        if (equipment[1].gameObject.activeSelf)
        {
            // Equipment at index 1 is active
            healIndicator.SetActive(true);
        }
        else
        {
            healIndicator.SetActive(false);
        }

        if (equipment[2].gameObject.activeSelf)
        {
            // Equipment at index 2 is active
            DividerIndicator.SetActive(true);
        }
        else
        {
            DividerIndicator.SetActive(false);
        }

        if (equipment[3].gameObject.activeSelf)
        {
            // Equipment at index 3 is active
            HealthMultiplierIndicator.SetActive(true);
        }
        else
        {
            HealthMultiplierIndicator.SetActive(false);
        }
    }

    void HandleEquipmentSwitching()
    {
        if (Time.timeScale == 1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchEquipment(0);
            if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchEquipment(1);
            if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchEquipment(2);
            if (Input.GetKeyDown(KeyCode.Alpha4)) SwitchEquipment(3);
        }
    }

    void SwitchEquipment(int index)
    {
        if (index != currentEquipmentIndex)
        {
            equipment[currentEquipmentIndex].gameObject.SetActive(false);
            currentEquipmentIndex = index;
            Equip(currentEquipmentIndex);
        }
    }

    void Equip(int index)
    {
        equipment[index].gameObject.SetActive(true);
    }
}