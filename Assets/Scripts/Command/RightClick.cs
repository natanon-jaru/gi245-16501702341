using System;
using UnityEngine;

public class RightClick : MonoBehaviour
{
    public static RightClick instance;

    private Camera cam;

    [SerializeField] LayerMask layerMask;

    private LeftClick leftClick;

    private void Awake()
    {
        leftClick = GetComponent<LeftClick>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
        cam = Camera.main;
        layerMask = LayerMask.GetMask("Ground", "Character", "Building");
    }

    // Update is called once per frame
    void Update()
    {
        //mouse-up
        if (Input.GetMouseButtonUp(1))
        {
            TryCommand(Input.mousePosition);
        }
    }

    private void CommandToWalk(RaycastHit hit, Character c)
    {
        if(c != null)
            c.WalkToPosition(hit.point);
    }

    private void TryCommand(Vector2 screenPos)
    {
        Ray ray = cam.ScreenPointToRay(screenPos);
        RaycastHit hit;
        
        //if we left-click something
        if (Physics.Raycast(ray, out hit, 1000, layerMask))
        {
            switch (hit.collider.tag)
            {
                case"Ground":
                    CommandToWalk(hit, leftClick.CurChar);
                    break;
            }
        }
    }
}
