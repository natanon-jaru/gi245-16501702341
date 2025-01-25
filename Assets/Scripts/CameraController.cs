using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    
    [Header("Move")] //แบ่งหัวข้อ
    [SerializeField] private float moveSpeed;
    
    [SerializeField] private float zInput;
    [SerializeField] private float xInput;
    
    public static CameraController instance;

    public void Awake() //เปิดการใช้ก่อน Start
    {
        instance = this;
        cam = Camera.main; //เพื่อประหยัดเวลา เวลาต้องการเรียกใช้ Camera.Main ก็พิมพ์แค่ cam เพื่อใช้ได้เลย
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveSpeed = 50;
    }

    // Update is called once per frame
    void Update()
    {
        MoveByKB();
    }

    private void MoveByKB()
    {
        xInput = Input.GetAxis("Horizontal"); //เอามาจาก Project Setting ตรง Input Manager
        zInput = Input.GetAxis("Vertical");
        
        Vector3 dir = (transform.forward * zInput) + (transform.right * xInput);
        
        transform.position += dir * moveSpeed * Time.deltaTime;
    }
}
