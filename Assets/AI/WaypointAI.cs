using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointAI : MonoBehaviour
{
    public Color color = new Color(1, 0, 0, 0.5f);
    public string tag = "Enemy";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = color;
        Gizmos.DrawCube(transform.position, new Vector3(1, 1, 1));
    }
}
