using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {
    public int humanCount;

    public DescriptionDefaults defaults;
    private List<HumanDescription> _descriptions = new List<HumanDescription>();

    public Spawner spawner;
    private List<GameObject> _humans;
    

    private void Start() {
        // generate descriptions
        for(int i = 0; i < humanCount; i++)
            _descriptions.Add(new HumanDescription(defaults));
        // generate humans from descriptions
        // give them to a spawner
        _humans = spawner.Spawn(_descriptions);

        // pick 2 as targets

        // start timer
    }
}