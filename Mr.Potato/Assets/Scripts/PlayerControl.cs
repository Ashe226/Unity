using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame updat
    private Rigidbody2D heroBody;
    public float moveForce = 100;
    public float jumpForce=800;
    public int MaxSpeed = 5;
    private float fInput = 0.0f;
    private bool bFaceRight = true;
    private bool bGrounded = false; //定义私有变量
    Transform mGroundCheck;  //为射线检查需要创建mGroundCheck
    //标识是否着地的全局变量
    
    
    
    void Start()
    {
        heroBody = GetComponent<Rigidbody2D>();
        //首先在初始化时需要把GroundCheck找到，所以需要搜索到“GroundCheck”的名字
        mGroundCheck = transform.Find("GroundCheck");
    }

    // Update is called once per frame
    void Update()
    {
        //移动的速度
        fInput = Input.GetAxis("Horizontal");
        
        //转身
        if (fInput < 0 && bFaceRight)
            flip();
        else if (fInput > 0 && !bFaceRight)
            flip();
 
        //射线检测
        //linecast首先从（英雄位置）向（GroundCheck）检查，起点-终点
        //赋予一个布尔变量
        bGrounded = Physics2D.Linecast(transform.position, mGroundCheck.position,
            1 << LayerMask.NameToLayer("Ground"));
        

    }

    private void FixedUpdate()
    {
        //移动的速度
        if (Mathf.Abs(heroBody.velocity.x) < MaxSpeed)
            heroBody.AddForce(fInput * moveForce * Vector2.right);

        if (Mathf.Abs(heroBody.velocity.x) > MaxSpeed)
            heroBody.velocity = new Vector2(Mathf.Sign(heroBody.velocity.x) * MaxSpeed, heroBody.velocity.y);
        
        bool bJump = false;
        
        if (bGrounded)
        {
            bJump = Input.GetKeyDown(KeyCode.Space);
            Vector2 upForce = new Vector2(0, 1);
            if (bJump)
            heroBody.AddForce(upForce * jumpForce); //给了一个力
        }


    }

    void flip()
    {
        
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        bFaceRight = !bFaceRight;
    }
}
