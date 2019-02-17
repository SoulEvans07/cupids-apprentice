using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour {
    private Rigidbody2D _rigidbody;
    public float speed = 0.8f;
    
    void Start() {
        this._rigidbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        Vector3 pos = this._rigidbody.position;
        this._rigidbody.position = new Vector3(pos.x + Time.deltaTime * this.speed, pos.y);
    }
}