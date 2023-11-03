using UnityEngine;

public class Currency : MonoBehaviour
{
    public static string DisplayCurrency(float gold)
    {
        if (gold < 1000)
        {
            return gold.ToString("F0");
        }
        else if (gold < 10000)
        {
            return string.Format("{0:0.##}K", gold / 1000);
        }
        else if (gold < 1000000)
        {
            return string.Format("{0:F0}K", gold / 1000);
        }
        else if (gold < 10000000)
        {
            return string.Format("{0:0.##}M", gold / 1000000);
        }
        else if (gold < 1000000000)
        {
            return string.Format("{0:F0}M", gold / 1000000);
        }
        else
        {
            return string.Format("{0:0.##}B", gold / 1000000000);
        }
    }
}
