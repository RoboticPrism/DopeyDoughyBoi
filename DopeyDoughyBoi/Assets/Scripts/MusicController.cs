using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour {

    public List<AudioSource> musicTracks = new List<AudioSource>();

    // Use this for initialization
    void Start () {
        foreach(AudioSource track in musicTracks)
        {
            track.Play();
            track.volume = 0;
        }
        musicTracks[0].volume = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChooseMusic(int trackNumber)
    {
        StopCoroutine("FadeSwap");
        StartCoroutine(FadeSwap(musicTracks[trackNumber]));
    }

    IEnumerator FadeSwap(AudioSource primarySouce)
    {
        List<AudioSource> otherSources = musicTracks;
        otherSources.Remove(primarySouce);
        while (primarySouce.volume < 1)
        {
            primarySouce.volume += Time.deltaTime;
            foreach (AudioSource otherSource in otherSources)
            {
                otherSource.volume -= Time.deltaTime;
            }
            yield return null;
        }

        
    }
}
