using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class theMatrix : MonoBehaviour {

	//Define the matrix
    public static int width = 6; // X-axis
    public static int depth = 6; // Z-axis
    public static int height = 14; // Y-axis + 1 to make room for spawner

    public static Transform[,,] matrix = new Transform[width,height,depth];

    // round vector components to whole numbers
    public static Vector3 roundVec3(Vector3 v)
    {
        return new Vector3(Mathf.Round(v.x), Mathf.Round(v.y), Mathf.Round(v.z));
    }

    // determine if a coordinate is within the borders of the matrix
    public static bool insideBorder(Vector3 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < width  // check x boundaries
            && (int)pos.y > 0                         // falling, so check if y is above the ground
            && (int)pos.z >= 0 && (int)pos.z < depth); // check z boundaries
    }

    // Delete all the blocks in a single layer
    public static void deleteLayer (int y)
    {
        // loop through each cell on the x/z plane & destroy the block there
        for (int x = 0; x < width; ++x)
        {
            for (int z = 0; z < depth; ++z)
            {
                // destroy the block at that position
                Destroy(matrix[x, y, z].gameObject);
                // update the matrix to make that cell empty
                matrix[x, y, z] = null;
            }
        }
    }

    // Shift all blocks in a specified layer down one layer
    public static void decreaseLayer (int y)
    {
        for (int x = 0; x < width; ++x)
        {
            for (int z = 0; z < depth; ++z)
            {
                if (matrix[x, y, z] != null)
                {
                    // Move the block down 1 layer in the matrix
                    matrix[x, y-1, z] = matrix[x, y,z];
                    // update the cell we just moved from to empty in the matrix
                    matrix[x,y,z] = null;
                    // move the actual block down
                    matrix[x, y-1, z].position += new Vector3(0,-1,0);
                }
            }
        }
    }

    // lower all the layers above a specified layer
    public static void decreaseLayersAbove (int y)
    {
        // loop through each layer above the deleted layer
        for (int i = y; i < height; ++i)
        {
            // shift the layer down 1
            decreaseLayer(i);
        }
    }

    // check if a layer has blocks in every cell
    public static bool isLayerFull (int y)
    {
        // loop through each cell in the layer & check if it's empty
        for (int x = 0; x < width; ++x)
        {
            for (int z = 0; z < depth; ++z)
            {
                if (matrix[x, y, z] == null)
                {
                    return false;
                }
            }
        }
        return true;
    }

    // Perform the full action of deleting a layer
    public static void deleteFullLayers ()
    {
        for (int y = 0; y < height; ++y)
        {
            if (isLayerFull(y)) // if the layer is filled
            {
                deleteLayer(y);             // delete that layer
                decreaseLayersAbove(y + 1); // shift all the layers above it down

                // go through this layer again to catch the layer that was just shifted down here
                --y; 
            }
        }
    }
}
