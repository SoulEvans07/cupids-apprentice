using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Experimental.U2D.IK;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Human : MonoBehaviour {
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;
    private IKManager2D _ikManager;
    private List<LimbSolver2D> _limbs = new List<LimbSolver2D>();
    private Vector2 _pos;
    public float speed = 0.8f;
    public int direction = 1;

    public Transform wallDetector;
    public LayerMask whatIsWall;

    public List<GameObject> clothing = new List<GameObject>();
    
    void Start() {
        if(!wallDetector) throw new Exception(this.name + " is missing wall detector");
     
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _ikManager = GetComponent<IKManager2D>();
        if (_ikManager) {
            foreach (Solver2D solver in _ikManager.solvers) {
                _limbs.Add(solver.GetComponent<LimbSolver2D>());
            }
        }

        this.Randomize();
    }

    void FixedUpdate() {
        _pos = this._rigidbody.position;
        
        // Change direction if wall is in front of us
        if (Physics2D.OverlapCircle(this.wallDetector.position, 0.1f, this.whatIsWall)) {
            this.Flip();
        }

        // Move
        _rigidbody.position = new Vector3(_pos.x + Time.deltaTime * this.direction * this.speed, _pos.y);
    }

    private void OnMouseUp() {
        Debug.Log(this.name);
    }

    void Flip() {
        this.direction *= -1;
        Vector3 lScale = _rigidbody.transform.localScale;
        _rigidbody.transform.localScale = new Vector3(-lScale.x, lScale.y, lScale.z);
//        this.wallDetector.localPosition = new Vector2(wallDetector.localPosition.x * -1, wallDetector.localPosition.y);

//        foreach (LimbSolver2D limb in _limbs) {
//            limb.flip = !limb.flip;
//        }
    }

    void Randomize() {
        // speed
        this.speed *= (1 + Random.Range(-0.1f, 0.1f));
        // size
        Vector2 scale = _rigidbody.transform.localScale;
        _rigidbody.transform.localScale = new Vector2(scale.x + Random.Range(-0.05f, 0.05f) , scale.y + Random.Range(-0.2f, 0.1f));
        Fog fog = GetComponentInChildren<Fog>();
        if(fog) fog.fog.size *= _rigidbody.transform.localScale; 
        // color
        Color c = new Color(Random.Range(0.2f, 1), Random.Range(0.2f, 1), Random.Range(0.2f, 1));
        foreach (GameObject cloth in clothing) {
            cloth.GetComponent<SpriteRenderer>().color = c;
        }
        // direction
        if(Random.Range(-1, 1) < 0) this.Flip();
    }
}