using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SoundMenu : MonoBehaviour
{
    private AudioSource a_source;
    public AudioClip[] a_clips;
    public bool soundplay = false;

    void Start()
    {
        a_source = gameObject.AddComponent<AudioSource>();
    }
    public void PlayRandomSound()
    {
        a_source.volume = 0.2f;
        if (soundplay == false)
        {
            a_source.Stop();
            int selection = Random.Range(0, a_clips.Length);
            a_source.PlayOneShot(a_clips[selection]);
            soundplay = true;
        }
        else if (soundplay)
        {
            a_source.Stop();
            int selection = Random.Range(0, a_clips.Length);
            a_source.PlayOneShot(a_clips[selection]);
            soundplay = false;
        }
    }
}