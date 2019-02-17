using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Human : MonoBehaviour {
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _renderer;
    private Vector2 _pos;
    public float speed = 0.8f;
    public int direction = 1;

    public Transform wallDetector;
    public LayerMask whatIsWall;
    
    void Start() {
        if(!wallDetector) throw new Exception(this.name + " is missing wall detector");
     
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        
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

    void Flip() {
        this.direction *= -1;
        this.wallDetector.localPosition = new Vector2(wallDetector.localPosition.x * -1, wallDetector.localPosition.y);
    }

    void Randomize() {
        // speed
        this.speed *= (1 + Random.Range(-0.1f, 0.1f));
        // direction
        if(Random.Range(-1, 1) < 0) this.Flip();
        // size
        Vector2 scale = _rigidbody.transform.localScale;
        _rigidbody.transform.localScale = new Vector2(scale.x + Random.Range(-0.05f, 0.05f) , scale.y + Random.Range(-0.2f, 0.1f));
        Fog fog = GetComponentInChildren<Fog>();
        if(fog) fog.fog.size *= _rigidbody.transform.localScale; 
        // color
        _renderer.color = new Color(Random.Range(0.2f, 1), Random.Range(0.2f, 1), Random.Range(0.2f, 1));
    }
}