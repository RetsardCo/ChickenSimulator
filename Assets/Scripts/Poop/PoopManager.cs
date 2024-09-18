using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopManager : MonoBehaviour
{
    public delegate void onGeneratePoop(int poopToSpawn);

    public onGeneratePoop generatePoop;

    public PoopSpawner spawner;

    [Range(5, 15), SerializeField]
    int spawnRandomNumber;

    int poopNumber;
    
    void Start()
    {
        generatePoop += spawner.spawnPoop;
        //Spawn();
    }

    public void Spawn()
    {
        var poopToSpawn = PoopNumbers();
        Debug.Log(poopNumber);
        generatePoop?.Invoke(poopToSpawn);
    }

    public int Poop() {
        return poopNumber;
    }

    int PoopNumbers() {
        int i = Random.Range(4, spawnRandomNumber);
        poopNumber = i;
        return poopNumber;
    }
}
