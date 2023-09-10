using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    public AudioSource grassMovementAudio;
    public AudioSource otherMovementAudio;
    public AudioSource otherHurtAudio;
    public AudioSource waterHurtAudio;
    public AudioSource diedAudio;
    public AudioSource wonMusic;
    public AudioSource diedMusic;
    public AudioSource mainMenuMusic;
    public AudioSource pauseMenuMusic;
    public AudioSource scoreAudio;
    public List<AudioSource> gameplayMusic;

    public int activeSongIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        activeSongIndex = Random.Range(0, gameplayMusic.Count);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
