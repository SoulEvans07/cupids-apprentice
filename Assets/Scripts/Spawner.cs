using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject humanObject;
    private List<GameObject> instanceList = new List<GameObject>();
    public List<Transform> spawnPoints;
    public List<Vector2> spawnVectors = new List<Vector2>();
    public GameObject spawnParent;
    public int amount = 10;

    void Start() {
        foreach (Transform point in spawnPoints) {
            spawnVectors.Add(point.position);
        }

        this.Generate();
    }

    protected void Generate() {
        if (spawnVectors.Count > 0) {
            for (int i = 0; i < amount; i++) {
                Vector3 pos = spawnVectors[Random.Range(0, spawnVectors.Count)];
                GameObject inst = Instantiate(humanObject, pos, Quaternion.identity);
                inst.transform.parent = spawnParent.transform;
                instanceList.Add(inst);
            }
        }
    }
}