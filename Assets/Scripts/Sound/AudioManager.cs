using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    [SerializeField] AudioSource[] jumpSounds;
    [SerializeField] AudioSource[] itemSounds;
    [SerializeField] AudioSource[] pickSounds;
    [SerializeField] AudioSource[] uiSounds;

    public void PlayJump()
    {
        jumpSounds[Random.Range(0, 3)].Play();
    }

    public void PlayUseItem()
    {
        itemSounds[Random.Range(0, 3)].Play();
    }

    public void PlayPick()
    {
        pickSounds[Random.Range(0, 3)].Play();
    }

    public void PlayUI()
    {
        uiSounds[Random.Range(0, 3)].Play();
    }
}
