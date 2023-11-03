using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BaseManagerLocation : MonoBehaviour
{
    [SerializeField] private string locationTitle;
    [SerializeField] private Image managerIcon;
    [SerializeField] private Image managerBoostIcon;
    public string LocationTitle => locationTitle;
    public Manager Manager { get; set; }
    public MineManager MineManager { get; set; }
    public virtual void RunBoost()
    {

    }

    public void UpdateBoostIcon()
    {
        if (managerBoostIcon != null)
        {
            managerIcon.sprite = Manager.managerIcon;
            managerBoostIcon.sprite = Manager.boostIcon;
        }
        else
        {
            MineManager.BoostImage.sprite = Manager.boostIcon;
        }
    }
}
