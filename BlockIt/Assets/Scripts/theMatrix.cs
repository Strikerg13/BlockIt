using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class theMatrix : MonoBehaviour {

	//Define the matrix
    public static int width = 6; // X-axis
    public static int depth = 6; // Z-axis
    public static int height = 13; // Y-axis

    public static Transform[,,] matrix = new Transform[width,height,depth];

    // round vector components to whole numbers
    public static Vector3 roundVec3(Vector3 v)
    {
        return new Vector3(Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(v.z));
    }

    // determine if a coordinate is within the borders of the matrix
    public static bool insideBorder(Vector3 pos)
    {
        
    }
}
