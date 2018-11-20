using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnController : MonoBehaviour {

	// Groups
    public GameObject[] tetrads;

    void Start ()
    {
        spawnTetrad();
    }

    public void spawnTetrad ()
    {
        // Random Index
        int i = Random.Range(0, tetrads.Length);

        // Spawn a tetrad at the current position
        Instantiate(tetrads[i], transform.position, Quaternion.identity);
    }
}
