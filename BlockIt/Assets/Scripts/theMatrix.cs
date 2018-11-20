using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class theMatrix : MonoBehaviour {

	//Define the matrix
    public static int width = 6; // X-axis
    public static int depth = 6; // Z-axis
    public static int height = 13; // Y-axis

    public static Transform[,,] matrix = new Transform[width,height,depth];


}
