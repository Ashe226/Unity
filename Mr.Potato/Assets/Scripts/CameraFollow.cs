using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour

{
    public Transform playerTran;  //代表英雄的transform
    public float maxDistanceX = 2;  //当主角和摄像机之间的位置超过2时摄像机跟着移动 X方向
    public float maxDistanceY = 2;  //当主角和摄像机之间的位置超过2时摄像机跟着移动 Y方向
    public float xSpeed = 2;
    public float ySpeed = 2;
    public Vector2 maxXandY = new Vector2(8,8);
    public Vector2 minXandY = new Vector2(-8,8);
    
    // Start is called before the first frame update

    private bool NeedMoveX()
    {
      return Mathf.Abs(transform.position.x - playerTran.position.x) > maxDistanceX;
    }
    
    private bool NeedMoveY()
    {
        
        return Mathf.Abs(transform.position.y - playerTran.position.y) > maxDistanceY;
       
    }
    void Start()
    {
        
    }

    private void Awake()
    {
        playerTran = GameObject.FindGameObjectWithTag("Player").transform;  //另一个找到主角的方法
        //playerTran = GameObject.Find("Hero").transform; //找到主角的位置

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TrackPlayer()  //用途：跟踪
    {
        Debug.Log(NeedMoveY());
        float newX = transform.position.x;//存放当前摄像机的位置
        float newY = transform.position.y;//存放当前摄像机的位置

        if (NeedMoveX())
        newX = Mathf.Lerp(transform.position.x, playerTran.position.x,
                xSpeed * Time.deltaTime); // time.deltatime = 0.02s
        
        if (NeedMoveY())
            newY = Mathf.Lerp(transform.position.y, playerTran.position.y,
                ySpeed * Time.deltaTime); // time.deltatime = 0.02s
        
        newX = Mathf.Clamp(newX, minXandY.x, maxXandY.x); //将第一个值控制在最大最小之间
        newY = Mathf.Clamp(newY, minXandY.y, maxXandY.y); //将第一个值控制在最大最小之间
            
            transform.position = new Vector3(newX, newY, transform.position.z);
        
        
    }
    private void FixedUpdate()
    {
        TrackPlayer();
    }
}
