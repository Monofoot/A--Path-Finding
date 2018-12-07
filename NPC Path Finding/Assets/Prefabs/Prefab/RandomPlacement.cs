using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlacement : MonoBehaviour {

    public GameObject tree1;
    public GameObject tree2;
    public GameObject tree3;
    public GameObject tree4;
    List<GameObject> treeList = new List<GameObject>();

    public GameObject mountain;

    void SpawnTrees(int x)
    {
        int treePopulationSize = x;
        treeList.Add(tree1);
        treeList.Add(tree2);
        treeList.Add(tree3);
        treeList.Add(tree4);

        for (int i = 0; i < treePopulationSize; i++)
        {
            int treePrefabIndex = UnityEngine.Random.Range(0, 4);
            // Set the position of the new fish to a random range between x, y and z.
            Vector3 treePos = new Vector3(Random.Range(-30.0f, 30.0f), 0.0f, Random.Range(-30.0f, 30.0f));
            GameObject tree = Instantiate(treeList[treePrefabIndex], treePos, Quaternion.identity);
            // The fish also need random degrees of rotation along the y and z axis.
            // The prefab for the fish requires the x axis to be rotated by -90 degress at
            // instantiation.
            tree.transform.Rotate(0.0f, Random.Range(0.0f, 180.0f), 0.0f);
        }
    }

    void SpawnMountain(int x)
    {
        int mountainPopulationSize = x;

        for (int i = 0; i < mountainPopulationSize; i++)
        {
            // Keep the y axis of fish pellets as 0. The idea is that they fall from the top of the scene to the bottom.
            Vector3 mountainPos = new Vector3(Random.Range(-30.0f, 30.0f), 0.0f, Random.Range(-30.0f, 30.0f));
            GameObject Algae = Instantiate(mountain, mountainPos, Quaternion.identity);

            // Give the pellets some random rotation, just to make them seem alive. This will be updated in the update function as they
            // swivel to the bottom of the tank.
            mountain.transform.Rotate(0.0f, Random.Range(0.0f, 180.0f), 0.0f);
        }
    }

    // Use this for initialization
    void Start () {
        SpawnTrees(30);
        SpawnMountain(3);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
