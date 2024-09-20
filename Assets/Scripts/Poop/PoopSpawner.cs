using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoopSpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public static int hh = 1;

    [Header("Axis Point"), SerializeField]
    private float x1, x2, y1, y2, z1, z2;

    [Header("Rotation"), SerializeField]
    private float xRotation, yRotation, zRotation;

    [Header("Layer to Detect"), SerializeField]
    private LayerMask detectionLayer;  // Specify the layer(s) to check for collisions.

    public void spawnPoop(int poopToSpawn)
    {
        Debug.Log(poopToSpawn);
        //Instantiate(objectToSpawn, transform.position, Quaternion.identity);

        for (int i = 0; i < poopToSpawn; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            if (IsPositionClear(randomPosition))
            {
                Instantiate(objectToSpawn, randomPosition, Quaternion.Euler(xRotation, yRotation, zRotation));
            }
            else
            {
                i--;
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        float x = Random.Range(x1, x2);
        float y = Random.Range(y1, y2);
        float z = Random.Range(z1, z2);
        return new Vector3(x, y, z);
    }

    bool IsPositionClear(Vector3 position)
    {

        /*RaycastHit hit;
        if (Physics.Raycast(position, Vector3.down, out hit))
        {
            return false;
        }
        else
        {
            return true;
        }*/
        //
        /*float checkRadius = 0.5f; 
        Collider[] colliders = Physics.OverlapSphere(position, checkRadius);
        return colliders.Length == 0;*/

        float checkRadius = 0.5f;  // The radius of the sphere to check.

        // Use Physics.OverlapSphere with a LayerMask to detect specific colliders
        Collider[] colliders = Physics.OverlapSphere(position, checkRadius, detectionLayer);

        // Return true if no colliders are detected on the specified layer(s)
        return colliders.Length == 0;
    }
}
