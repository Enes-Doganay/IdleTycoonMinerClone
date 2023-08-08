using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    [SerializeField] private GameObject cloudPrefab;
    [SerializeField] private Transform spawnPosition;
    private void Start()
    {
        SpawnCloud();
        Debug.Log(Currency.DisplayCurrency(1000));
        Debug.Log(Currency.DisplayCurrency(3000));
        Debug.Log(Currency.DisplayCurrency(10000));
        Debug.Log(Currency.DisplayCurrency(19000));
        Debug.Log(Currency.DisplayCurrency(190000));
        Debug.Log(Currency.DisplayCurrency(1900000));
        Debug.Log(Currency.DisplayCurrency(19000000));
        Debug.Log(Currency.DisplayCurrency(190000000));
        Debug.Log(Currency.DisplayCurrency(1000000000));
    }
    private void SpawnCloud()
    {
        GameObject newCloud = Instantiate(cloudPrefab, spawnPosition.position, Quaternion.identity);
        Clouds cloud = newCloud.GetComponent<Clouds>();
        cloud.SpawnPosition = spawnPosition.position; 
    }
}
