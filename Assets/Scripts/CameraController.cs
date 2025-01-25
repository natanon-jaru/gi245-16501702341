using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    
    [Header("Move")] //แบ่งหัวข้อ
    [SerializeField] private float moveSpeed;

    [SerializeField] private Transform corner1;
    [SerializeField] private Transform corner2;
    
    [SerializeField] private float zInput;
    [SerializeField] private float xInput;

    [Header("Zoom")] 
    [SerializeField] private float zoomModifier;
    
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
        Zoom();
        MoveByMouse();
    }

    private void MoveByKB()
    {
        xInput = Input.GetAxis("Horizontal"); //เอามาจาก Project Setting ตรง Input Manager
        zInput = Input.GetAxis("Vertical"); //เอามาจาก Project Setting ตรง Input Manager
        
        Vector3 dir = (transform.forward * zInput) + (transform.right * xInput);
        
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.position = Clamp(corner1.position, corner2.position);
    }
    
    private Vector3 Clamp(Vector3 lowerLeft, Vector3 topRight)
    {
        Vector3 pos = new Vector3(Mathf.Clamp(transform.position.x, lowerLeft.x, topRight.x),
            transform.position.y,
            Mathf.Clamp(transform.position.z, lowerLeft.z, topRight.z));

        return pos;
    }

    private void Zoom()
    {
        zoomModifier = Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetKey(KeyCode.Z)) //GetKey,GetMouseButtondown = กดแช่
            zoomModifier = -0.1f;
        if (Input.GetKey(KeyCode.X))
            zoomModifier = 0.1f;
        
        cam.orthographicSize += zoomModifier;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 4, 10); //กำหนดการซูมกล้องให้ไม่เกินเท่าไร
    }

    private void MoveByMouse()
    {
        if (Input.mousePosition.x >= Screen.width)
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.World);
        
        if (Input.mousePosition.x <= 0f)
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
        
        if(Input.mousePosition.y >= Screen.height)
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.World);
        
        if(Input.mousePosition.y <= 0f)
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime, Space.World);
    }
}
