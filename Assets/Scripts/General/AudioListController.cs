using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListController : MonoBehaviour
{
    [SerializeField] public AudioSource effectsSource, musicSource;
    [System.Serializable]
    public class Effects
    {
        public AudioClip audioclip;
    }
    [System.Serializable]
    public class Music
    {
        public AudioClip audioclip;
    }

    public Effects[] effects;
    public Music[] music;
}
