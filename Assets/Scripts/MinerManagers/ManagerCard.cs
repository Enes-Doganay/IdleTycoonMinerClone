using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManagerCard : MonoBehaviour
{
    [SerializeField] private Image managerIcon;
    [SerializeField] private Image boostIcon;
    [SerializeField] private TextMeshProUGUI managerName;
    [SerializeField] private TextMeshProUGUI managerLevel;
    [SerializeField] private TextMeshProUGUI boostEffect;
    [SerializeField] private TextMeshProUGUI boostDescription;

    public Manager Manager { get; set; }
    public BaseManagerLocation ManagerLocation;
    public void SetupManagerCard(Manager manager)
    {
        Manager = manager;
        managerIcon.sprite = manager.managerIcon;
        boostIcon.sprite = manager.boostIcon;
        managerName.text = manager.managerName;
        managerLevel.text = manager.ManagerLevel.ToString();
        managerLevel.color = manager.levelColor;
        boostEffect.text = manager.boostDuration.ToString();
        boostDescription.text = manager.boostDescription;

    }
    public void AssignManager()
    {
        ManagerLocation = ManagersController.Instance.CurrentManagerLocation;
        ManagersController.Instance.AddAssignedManagerCard(this);
        SetManagerInfoToManagerLocation();
    }

    private void SetManagerInfoToManagerLocation()
    {
        if(ManagerLocation.Manager == null)
        {
            ManagerLocation.Manager = Manager;
            ManagersController.Instance.UpdateAssignManagerInfo(ManagerLocation);
            gameObject.SetActive(false);
        }
    }
}
