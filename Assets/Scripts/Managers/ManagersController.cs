using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManagersController : Singleton<ManagersController>
{
    [SerializeField] private ManagerCard managerCardPrefab;
    [SerializeField] private int initialManagerCost = 100;
    [SerializeField] private int managerCostMultiplier = 3;

    [Header("Assign Manager UI")]
    [SerializeField] private Image managerIcon;
    [SerializeField] private Image boostIcon;
    [SerializeField] private TextMeshProUGUI managerName;
    [SerializeField] private TextMeshProUGUI managerLevel;
    [SerializeField] private TextMeshProUGUI boostEffect;
    [SerializeField] private TextMeshProUGUI boostDescription;

    [SerializeField] private TextMeshProUGUI managerPanelTitle;
    [SerializeField] private Transform managersContainer;
    [SerializeField] private GameObject managerPanel;
    [SerializeField] private GameObject assignedManagerPanel;
    [SerializeField] private List<Manager> managerList;
    public BaseManagerLocation CurrentManagerLocation { get; set; }
    public int NewManagerCost { get; set; }

    private List<ManagerCard> assignedManagerCards;

    private Camera cam;
    private void Start()
    {
        cam = Camera.main;
        assignedManagerCards = new List<ManagerCard>();
        NewManagerCost = initialManagerCost;
    }
    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            if (Physics2D.Raycast(mousePosition, Vector2.zero))
            {
                RaycastHit2D hitInfo = Physics2D.Raycast(mousePosition, Vector2.zero);
                if (hitInfo.transform.GetComponent<MineManager>() != null)
                {
                    CurrentManagerLocation = hitInfo.transform.GetComponent<MineManager>().Location;
                    OpenPanel(true);
                }
            }
        }
    }

    #region Boosts
    public void RunMovementBoost(BaseMiner miner, float duration, float value)
    {
        StartCoroutine(IEMovementBoost(miner, duration, value));
    }
    public void RunLoadingBoost(BaseMiner miner, float duration, float value)
    {
        StartCoroutine(IELoadingBoost(miner, duration, value));
    }

    private IEnumerator IEMovementBoost(BaseMiner miner, float duration, float value)
    {
        float startSpeed = miner.MoveSpeed;
        miner.MoveSpeed *= value;
        yield return new WaitForSeconds(duration);
        miner.MoveSpeed = startSpeed;
    }
    private IEnumerator IELoadingBoost(BaseMiner miner, float duration, float value)
    {
        float startValue = miner.CollectPerSecond;
        miner.CollectPerSecond *= value;
        yield return new WaitForSeconds(duration);
        miner.CollectPerSecond = startValue;
    }
    #endregion

    public void UnassignManager()
    {
        RestoreManagerCard(CurrentManagerLocation.Manager);
        CurrentManagerLocation.Manager = null;
        UpdateAssignManagerInfo(CurrentManagerLocation);
    }
    public void HireManager()
    {
        if(GoldManager.Instance.CurrentGold >= NewManagerCost && managerList.Count > 0) 
        {
            //create a new card
            ManagerCard card = Instantiate(managerCardPrefab, managersContainer);

            //Get random manager
            int randomIndex = Random.Range(0, managerList.Count);
            Manager randomManager = managerList[randomIndex];
            card.SetupManagerCard(randomManager);
            //managerList.RemoveAt(randomIndex);
            GoldManager.Instance.RemoveGold(NewManagerCost);
            NewManagerCost *= managerCostMultiplier;
        }
    }
    public void UpdateAssignManagerInfo(BaseManagerLocation managerLocation)
    {
        if(managerLocation.Manager != null)
        {
            managerIcon.sprite = managerLocation.Manager.managerIcon;
            boostIcon.sprite = managerLocation.Manager.boostIcon;
            managerName.text = managerLocation.Manager.managerName;
            managerLevel.text = managerLocation.Manager.ManagerLevel.ToString();
            boostEffect.text = managerLocation.Manager.boostDuration.ToString();
            boostDescription.text = managerLocation.Manager.boostDescription;
            managerLocation.UpdateBoostIcon();
            assignedManagerPanel.SetActive(true);
        }
        else
        {
            managerIcon.sprite = null;
            boostIcon.sprite = null;
            managerName.text = null;
            managerLevel.text = null;
            boostEffect.text = null;
            boostDescription.text = null;
            assignedManagerPanel.SetActive(false);
        }
    }
    public void AddAssignedManagerCard(ManagerCard card)
    {
        assignedManagerCards.Add(card);
    }
    public void OpenPanel(bool value)
    {
        managerPanel.SetActive(value);
        if (value)
        {
            managerPanelTitle.text = CurrentManagerLocation.LocationTitle;
            UpdateAssignManagerInfo(CurrentManagerLocation);
        }
    }
    private void RestoreManagerCard(Manager manager)
    {
        ManagerCard managerCard = null;
        for(int i = 0; i < assignedManagerCards.Count; i++) 
        {
            if (assignedManagerCards[i].Manager.managerName == manager.managerName)
            {
                assignedManagerCards[i].gameObject.SetActive(true);
                managerCard = assignedManagerCards[i];
            }
        }

        if(managerCard != null)
        {
            assignedManagerCards.Remove(managerCard);
        }
    }
}
