using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    [SerializeField]private float speed;
    [SerializeField] private float jumpPower;
    private float axis;
    private Rigidbody2D rigid;
    void Start() {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update() {
        axis = Input.GetAxisRaw("Horizontal");
        
        if (Input.GetButtonDown("Jump")) {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);
        }
    }

    private void FixedUpdate() {
        rigid.velocity = new Vector2(axis * speed, rigid.velocity.y);
    }
}
