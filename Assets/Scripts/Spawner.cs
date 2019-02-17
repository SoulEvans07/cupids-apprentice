using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject humanObject;
    protected List<GameObject> instanceList = new List<GameObject>();
    public List<Transform> spawnPoints;
    public List<Vector2> spawnVectors = new List<Vector2>();
    public GameObject spawnParent;

    public virtual List<GameObject> Spawn(List<HumanDescription> descriptions) {
        foreach (Transform point in spawnPoints) {
            spawnVectors.Add(point.position);
        }

        this.Generate(descriptions);
        return instanceList;
    }

    protected void Generate(List<HumanDescription> descriptions) {
        if (spawnVectors.Count > 0) {
            for (int i = 0; i < descriptions.Count; i++) {
                Vector3 pos = spawnVectors[Random.Range(0, spawnVectors.Count)];
                GameObject inst = Instantiate(humanObject, pos, Quaternion.identity);
                inst.transform.parent = spawnParent.transform;
                inst.GetComponent<Human>().SetOrderLayer(i);
                inst.GetComponent<Human>().SetDescription(descriptions[i]);
                instanceList.Add(inst);
            }
        } else {
            Debug.Log("shame");
        }
    }
}