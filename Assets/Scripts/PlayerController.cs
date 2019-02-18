using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour {
    public Camera _cam;

    public NavMeshAgent _agent;

    private void Start() {
        _cam = Camera.main;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 ray = _cam.ScreenToWorldPoint(Input.mousePosition);
            _agent.SetDestination(ray);
        }
    }
    
}