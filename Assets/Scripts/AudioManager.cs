using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] bgm;
    public AudioSource[] BGM => bgm;

    [SerializeField] private AudioSource[] sfx;
    public AudioSource[] SFX => sfx;

    [SerializeField] private AudioMixer audioMixer;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        if (instance == this)
            SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StopAllBGM();

        switch (scene.name)
        {
            case "MainMenu":
                PlayBGM(0);
                break;
            case "VillageScene":
                PlayBGM(1);
                break;
            case "Dungeon":
                PlayBGM(2);
                break;
        }
    }

    private void StopAllBGM()
    {
        for (int i = 0; i < bgm.Length; i++)
            bgm[i].Stop();
    }

    public void PlayBGM(int i)
    {
        if (i >= 0 && i < bgm.Length && !bgm[i].isPlaying)
        {
            StopAllBGM();
            bgm[i].Play();
        }
    }

    public void PlaySFX(int i)
    {
        if (i >= 0 && i < sfx.Length && !sfx[i].isPlaying)
            sfx[i].Play();
    }
}