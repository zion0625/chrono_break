using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [Header("Move")]
    [SerializeField]private int speed;
    private float axis;
    
    [Header("Jump")]
    [SerializeField] private float jumpPower;

    private int comboCount;

    private Animator anim;
    private Rigidbody2D rigid;
    private SpriteRenderer spr;
    private static readonly int isJumping = Animator.StringToHash("isJumping");

    void Start() {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    void Update() {
        axis = Input.GetAxisRaw("Horizontal");
        anim.SetInteger("speed", (int)axis);
        
        if(Input.GetButton("Horizontal"))
            spr.flipX = axis == -1;
        
        if (Input.GetButtonDown("Jump") && !anim.GetBool(isJumping)) {
            rigid.velocity = new Vector2(rigid.velocity.x, jumpPower);
            anim.SetBool(isJumping, true);
        }

        if (Input.GetMouseButtonDown(0) && !anim.GetBool("attacking")) {
            if (comboCount >= 3)
                comboCount = 0;
            comboCount++;
            anim.SetTrigger("atk");
            anim.SetBool("attacking", true);
            anim.SetFloat("comboCount", comboCount);
        }
    }

    private void FixedUpdate() {
        rigid.AddForce(Vector2.right * axis, ForceMode2D.Impulse);
        //rigid.velocity = new Vector2(Mathf.Clamp(rigid.velocity.x, -speed, speed) ,rigid.velocity.y);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        ContactPoint2D contact = other.contacts[0];
        Vector2 normal = contact.normal;
        if (other.gameObject.CompareTag("Ground") && Mathf.Abs(normal.x) < normal.y)
        {
            anim.SetBool(isJumping, false);
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
        }
    }
    
    public void OnFinishedAttack()
    {
        anim.SetBool("attacking", false);
    }
}
