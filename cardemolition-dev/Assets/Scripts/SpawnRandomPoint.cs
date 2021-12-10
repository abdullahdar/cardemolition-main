using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRandomPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 center;
    public Vector3 size;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }
}
