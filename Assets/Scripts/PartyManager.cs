using System;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    [SerializeField] 
    private List<Character> members = new List<Character>();
    public List<Character> Members { get { return members; } }

    [SerializeField] 
    private List<Character> selectChars = new List<Character>();
    public List<Character> SelectChars
    {
        get { return selectChars; }
    }

    public static PartyManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
