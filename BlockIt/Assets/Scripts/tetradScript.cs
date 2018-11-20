using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tetradScript : MonoBehaviour {

    float lastFall = 0;

	// Use this for initialization
	void Start () 
    {
        if (!isValidMatrixPos())
        {
            Debug.Log("GAME OVER");
            Application.Quit();
        }
	}
	
	// Update is called once per frame
	void Update () 
    {

        // DISPLAY THE POSITION
        //Debug.Log("x: " + (int)transform.position.x + " | y: " + (int)transform.position.y + " | z: " + (int)transform.position.z);



		// LEFT
        if (Input.GetAxis("Horizontal") < 0) // left
        {   // move left 1 cell
            transform.position += new Vector3(0, 0, 1);

            // if it's a valid spot
            if (isValidMatrixPos())
            {   // save the move
                updateTheMatrix();
            }
            else
            {   // move it back
                transform.position += new Vector3(0, 0, -1);
            }
        }

        // RIGHT
        else if (Input.GetAxis("Horizontal") > 0) // right
        {   // move right 1 cell
            transform.position += new Vector3(0, 0, -1);

            // if it's a valid spot
            if (isValidMatrixPos())
            {   // save the move
                updateTheMatrix();
            }
            else
            {   // move it back
                transform.position += new Vector3(0, 0, 1);
            }
        }

        // FORWARD
        else if (Input.GetAxis("Vertical") > 0) // down
        {   // move forwad 1 cell
            transform.position += new Vector3(1, 0, 0);

            // if it's a valid spot
            if (isValidMatrixPos())
            {   // save the move
                updateTheMatrix();
            }
            else
            {   // move it back
                transform.position += new Vector3(-1, 0, 0);
            }
        }

        // BACKWARD
        else if (Input.GetAxis("Vertical") > 0) // up
        {   // move backward 1 cell
            transform.position += new Vector3(-1, 0, 0);

            // if it's a valid spot
            if (isValidMatrixPos())
            {   // save the move
                updateTheMatrix();
            }
            else
            {   // move it back
                transform.position += new Vector3(1, 0, 0);
            }
        }

        // ROTATE
        else if (Input.GetButtonDown("Fire1")) // rotate button
        {   // rotate the tetrad
            transform.Rotate(0, 0, -90);

            // if it's a valid spot
            if (isValidMatrixPos())
            {   // save the move
                updateTheMatrix();
            }
            else
            {   // rotate it back
                transform.Rotate(0, 0, 90);
            }
        }

        // FALL
        else if (Time.time - lastFall >= 1) // change the 1 to adjust fall speed
        {   // move down 1 cell
            transform.position += new Vector3(0, -1,0);

            // if it's a valid spot
            if (isValidMatrixPos())
            {   // save the move
                updateTheMatrix();
            }
            else
            {   // move it back
                transform.position += new Vector3(1, 0, 0);
                // clear any layers that are filled
                theMatrix.deleteFullLayers();
                // spawn the next tetrad
                FindObjectOfType<spawnController>().spawnTetrad();
                // disable this tetrad
                //enabled = false;
                Debug.Log("Deactivate");

            }
            // update timer variable
            lastFall = Time.time;
        }
	}

    // check if a cell in the matix is open for a block to move into
    bool isValidMatrixPos()
    {
        // go through each block in the tetrad
        foreach (Transform block in transform)
        {
            // save the block's position (rounded to whole numbers)
            Vector3 v = theMatrix.roundVec3(block.position);

            // if that position is not inside the play area
            if (!theMatrix.insideBorder(v))
            {   // it's invalid
                return false;
            }

            // if there's already a block in that cell, and it's not part of this tetrad
            if (theMatrix.matrix[(int)v.x, (int)v.y, (int)v.z] != null
                && theMatrix.matrix[(int)v.x, (int)v.y, (int)v.z] != transform)
            {   // it's invalid
                return false;
            }
        }
        // otherwise, it's a valid spot
        return true;
    }

    void updateTheMatrix ()
    {   // go through each cell in the play area
        for (int y = 0; y < theMatrix.height; ++y)
            for (int x = 0; x < theMatrix.width; ++x)
                for (int z = 0; z < theMatrix.depth; ++z)
                {   
                    // if there's a block in that cell
                    if (theMatrix.matrix[z, y, z] != null)
                    {   // if the block is part of this tetrad


                        // DISPLAY THE POSITION
                        Debug.Log("x: " + x + " | y: " + y + " | z: " + z);

                        if (theMatrix.matrix[x, y, z].parent == transform)
                        {   // clear the block's spot in the matrix
                            theMatrix.matrix[x, y, z] = null;
                        }
                    }
                }

        foreach (Transform block in transform)
        {
            // save the block's position (rounded to whole numbers)
            Vector3 v = theMatrix.roundVec3(block.position);
            // add the block back into the matrix ??????????????????????????????
            theMatrix.matrix[(int)v.x, (int)v.y, (int)v.z] = block;
        }
    }


}
