using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopManager : MonoBehaviour
{
    public delegate void onGeneratePoop(int poopToSpawn);

    public onGeneratePoop generatePoop;

    public PoopSpawner spawner;

    public int spawnNumber;
    
    void Start()
    {
        generatePoop += spawner.spawnPoop;
        Spawn();
    }

    void Spawn()
    {
        var poopToSpawn = spawnNumber;
        generatePoop?.Invoke(poopToSpawn);
    }
}
