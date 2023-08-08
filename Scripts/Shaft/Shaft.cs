using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Shaft : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private ShaftMiner minerPrefab;
    [SerializeField] private Deposit depositPrefab;

    [Header("Locations")]
    [SerializeField] private Transform miningLocation;
    [SerializeField] private Transform depositLocation;
    [SerializeField] private Transform depositInstantiationPos;

    [Header("Manager")]
    [SerializeField] private Transform managerPos;
    [SerializeField] private GameObject managerPrefab;

    public Transform MiningLocation => miningLocation;
    public Transform DepositLocation => depositLocation;
    public List<ShaftMiner> Miners => miners;
    public Deposit CurrentDeposit { get; set; }
    public int ShaftID { get; set; }

    private GameObject minersContainer;
    private List<ShaftMiner> miners;
    private ShaftManagerLocation shaftManagerLocation;

    private void Start()
    {
        shaftManagerLocation = GetComponent<ShaftManagerLocation>();
        miners = new List<ShaftMiner>();
        minersContainer = new GameObject("Miners");
        CreateMiner();
        CreateDeposit();
    }

    public void CreateMiner()
    {
        ShaftMiner newMiner = Instantiate(minerPrefab, depositLocation.position, Quaternion.identity);
        newMiner.CurrentShaft = this;
        newMiner.transform.SetParent(minersContainer.transform);
        newMiner.MoveMiner(miningLocation.position);

        if(miners.Count > 0)
        {
            newMiner.CollectCapacity = miners[0].CollectCapacity;
            newMiner.CollectPerSecond = miners[0].CollectPerSecond;
            newMiner.MoveSpeed = miners[0].MoveSpeed;
        }

        miners.Add(newMiner);
    }
    public void CreateManager()
    {
        GameObject shaftManager = Instantiate(managerPrefab, managerPos.position, Quaternion.identity);
        MineManager mineManager = shaftManager.GetComponent<MineManager>();
        mineManager.SetupManager(shaftManagerLocation);
        shaftManagerLocation.MineManager = mineManager;
    }
    private void CreateDeposit()
    {
        CurrentDeposit = Instantiate(depositPrefab,depositInstantiationPos.position, Quaternion.identity);
        CurrentDeposit.transform.SetParent(depositInstantiationPos);
    }
    private void ShaftBoost(Shaft shaft, ShaftManagerLocation shaftManager)
    {
        if(shaft == this)
        {
            switch (shaftManager.Manager.BoostType)
            {
                case BoostType.Movement:
                    foreach(ShaftMiner miner in miners)
                    {
                        ManagersController.Instance.RunMovementBoost(miner, 
                            shaftManager.Manager.boostDuration, shaftManager.Manager.boostValue);
                    }
                    break;
                    case BoostType.Loading:
                    foreach (ShaftMiner miner in miners)
                    {
                        ManagersController.Instance.RunLoadingBoost(miner,
                            shaftManager.Manager.boostDuration, shaftManager.Manager.boostValue);
                    }
                    break;
            }
        }
    }
    private void OnEnable()
    {
        ShaftManagerLocation.OnBoost += ShaftBoost;
    }
    private void OnDisable()
    {
        ShaftManagerLocation.OnBoost -= ShaftBoost;
    }


}