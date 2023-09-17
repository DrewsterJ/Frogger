using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    public AudioSource grassMovementAudio;
    public AudioSource otherMovementAudio;
    public AudioSource waterHurtAudio;
    public AudioSource diedAudio;
    public AudioSource wonMusic;
    public AudioSource diedMusic;
    public AudioSource mainMenuMusic;
    public AudioSource pauseMenuMusic;
    public AudioSource scoreAudio;
    public List<AudioSource> gameplayMusic;

    public bool muted = false;
    public int activeSongIndex;
    
    // Start is called before the first frame update
    void Start()
    {
        activeSongIndex = Random.Range(0, gameplayMusic.Count);
    }

    public void PlayNextGameplaySong()
    {
        gameplayMusic[activeSongIndex].Stop();
        activeSongIndex += 1;
        if (activeSongIndex >= gameplayMusic.Count)
        {
            activeSongIndex = 0;
        }

        if (!GameManager.paused && GameManager.gameStarted)
        {
            gameplayMusic[activeSongIndex].Play();
        }
    }

    // Mutes gameplay music
    public void MuteMusic()
    {
        muted = true;
        foreach (var song in gameplayMusic)
        {
            song.volume = 0;
        }
        mainMenuMusic.volume = 0;
    }

    // Unmutes gameplay music
    public void UnmuteMusic()
    {
        muted = false;
        foreach (var song in gameplayMusic)
        {
            song.volume = 0.05f;
        }
        mainMenuMusic.volume = 0.03f;
    }
}
