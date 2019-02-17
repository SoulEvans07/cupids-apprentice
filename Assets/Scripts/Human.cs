using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Human : MonoBehaviour {
    private Rigidbody2D _rigidbody;
    private Vector2 _pos;
    public float speed = 0.8f;
    public int direction = 1;

    public Transform wallDetector;
    public LayerMask whatIsWall;
    
    void Start() {
        _rigidbody = GetComponent<Rigidbody2D>();
        
        if(Random.Range(-1, 1) < 0) this.Flip();
        this.speed *= (1 + Random.Range(-0.1f, 0.1f));
        if(!wallDetector) throw new Exception(this.name + " is missing wall detector");
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
}