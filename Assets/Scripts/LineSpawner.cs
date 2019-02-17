using System;
using System.Collections.Generic;
using UnityEngine;

public class LineSpawner : Spawner {
    public List<LinePairs> pairs;
    public int scale = 5;

    public override List<GameObject> Spawn(List<HumanDescription> descriptions) {
        Debug.Log(pairs);
        foreach (LinePairs pair in pairs) {
            Vector2 v = pair.b.position - pair.a.position;
            v = v / scale;
            for (int i = 0; i < scale; i++) {
                spawnVectors.Add((Vector2) pair.a.position + (i * v));
            }
        }

        this.Generate(descriptions);
        return instanceList;
    }

    [Serializable]
    public class LinePairs {
        public Transform a;
        public Transform b;
    }
}