using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoop : MonoBehaviour
{
    [Header("Poop GameObject"), SerializeField]
    GameObject poopObject;

    [Header("Spawn Boundaries"), SerializeField]
    float x1, z1, x2, z2, y;

    [Header("Object Rotation"), SerializeField]
    float xRotation;

    [Header("Detection Layers"), SerializeField]
    LayerMask detectionLayer;

    [Range(5, 15), SerializeField]
    int spawnRandomNumber;

    int numberToSpawn;

    public void PoopSpawn() {
        numberToSpawn = Random.Range(4, spawnRandomNumber);
        for (int i = 0; i < numberToSpawn; i++) {
            Vector3 randomPosition = GetRandomPosition();
            if (IsPositionClear(randomPosition)) {
                Instantiate(poopObject, randomPosition, Quaternion.Euler(xRotation, 0, 0));
            }
        }
    }

    public int GivePoopNumber() {
        Debug.Log("Number to Spawn" + numberToSpawn);
        return numberToSpawn;
    }

    Vector3 GetRandomPosition() {
        float x = Random.Range(x1, x2);
        float z = Random.Range(z1, z2);
        return new Vector3(x, y, z);
    }

    bool IsPositionClear(Vector3 position) {
        float checkRadius = 0.5f;
        Collider[] colliders = Physics.OverlapSphere(position, checkRadius, detectionLayer);
        /*for (int i = 0; i < detectionLayer.Length; i++) {
            colliders = Physics.OverlapSphere(position, checkRadius, detectionLayer);
        }*/
        return colliders.Length == 0;
    }
}
