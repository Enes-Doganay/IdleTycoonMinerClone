using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarehouseUpgrade : BaseUpgrade
{
    protected override void RunUpgrade()
    {
        if(CurrentLevel %10 == 0)
        {
            warehouse.AddMiner();
        }

        foreach(WarehouseMiner miner in warehouse.Miners)
        {
            miner.CollectCapacity *= (int)collectCapacityMultiplier;
            miner.CollectPerSecond *= (int)collectPerSecondMultiplier;

            if(CurrentLevel % 10 == 0)
            {
                miner.MoveSpeed *= moveSpeedMultiplier;
            }
        }
    }
}
