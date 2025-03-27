using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private GameObject doubleRingMarker;
    public GameObject DoubleRingMarker { get { return doubleRingMarker; } }

    [SerializeField] private GameObject[] magicVFX;
    public GameObject[] MagicVFX { get { return magicVFX; } }
    
    [SerializeField]
    private MagicData[] magicData;
    public MagicData[] MagicData {get { return magicData; } }

    public static VFXManager instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadMagic(int id, Vector3 posA, float time)
    {
        //Load Magic
        if (magicVFX[id] == null)
        {
            return;
        }
        GameObject objLoad = Instantiate(MagicVFX[id], posA + new Vector3(0,1.4f,0), Quaternion.identity);
        Destroy(objLoad, time);
        
    }

    public void ShootMagic(int id, Vector3 posA, Vector3 posB, float time)
    {
        //Shoot Magic
        if (magicVFX[id] == null)
        {
            return;
        }
        GameObject objShoot = Instantiate(magicVFX[id], posA+ new Vector3(0,1.4f,0), Quaternion.identity);
        objShoot.transform.position = Vector3.LerpUnclamped(posA, posB+ new Vector3(0,1f,0), time);
        Destroy(objShoot, time);
    }
}
