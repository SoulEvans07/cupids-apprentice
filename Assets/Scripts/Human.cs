﻿using System;
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
    private SpriteRenderer[] _renderers;
    private Vector2 _pos;
    public float speed = 0.8f;
    public int direction = 1;

    public Transform wallDetector;
    public LayerMask whatIsWall;

//    public List<GameObject> clothing = new List<GameObject>();

    public SpriteRenderer hairRenderer;
    private HumanDescription _description;
    
    void Start() {
        if(!wallDetector) throw new Exception(this.name + " is missing wall detector");
     
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderers = GetComponentsInChildren<SpriteRenderer>();
        
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
    }
    
    public void Flip(int dir) {
        dir = dir < 0 ? -1 : 1;
        if (this.direction != dir) {
            this.Flip();
        }
    }

    public void SetOrderLayer(int _base) {
        if(_renderers == null) _renderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer rendr in _renderers) {
            if(rendr.sortingLayerName != "FX")
            rendr.sortingOrder = rendr.sortingOrder + _base * 10;
        }
    }

    public void SetDescription(HumanDescription description) {
        _description = description;
        // size
        if(!_rigidbody) _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.transform.localScale = _description.scale;
        Fog fog = GetComponentInChildren<Fog>();
        if(fog) fog.fog.size *= _rigidbody.transform.localScale;
        
        // hair style
        hairRenderer.sprite = description.hairStyle.sprite;
        
        // hair color
        hairRenderer.color = description.hairColor;
    }


    void Randomize() {
        // speed
        this.speed *= (1 + Random.Range(-0.1f, 0.1f));
        
        //Vector2 scale = _rigidbody.transform.localScale;
        //_rigidbody.transform.localScale = new Vector2(scale.x + Random.Range(-0.05f, 0.05f) , scale.y + Random.Range(-0.2f, 0.1f));
         
        // color
//        Color c = new Color(Random.Range(0.2f, 1), Random.Range(0.2f, 1), Random.Range(0.2f, 1));
//        foreach (GameObject cloth in clothing) {
//            cloth.GetComponent<SpriteRenderer>().color = c;
//        }
        // direction
        if(Random.Range(-1, 1) < 0) this.Flip();
    }
}