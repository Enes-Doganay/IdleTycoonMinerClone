using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaftUpgrade : BaseUpgrade
{
    protected override void RunUpgrade()
    {
        if (shaft != null)
        {
            if(CurrentLevel % 10 == 0)
            {
                shaft.CreateMiner();
            }
            if(CurrentLevel == 10)
            {
                shaft.CreateManager();
            }
            foreach(ShaftMiner miner in shaft.Miners)
            {
                miner.CollectCapacity *= (int)collectCapacityMultiplier;
                miner.CollectPerSecond *= collectPerSecondMultiplier;

                if(CurrentLevel % 10 == 0)
                {
                    miner.MoveSpeed *= moveSpeedMultiplier;
                }
            }
        }
    }
}
