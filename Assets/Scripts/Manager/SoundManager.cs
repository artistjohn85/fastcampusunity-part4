using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] audioClips;
    private AudioSource[] audioSources; // cache

    public void SetInit()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSources = new AudioSource[audioClips.Length];
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i] = gameObject.AddComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySound(int index, bool isSkip)
    {
        if (!isSkip)
        {
            if (audioSources[index].isPlaying)
                return;
        }

        if (audioClips.Length > index)
        {
            audioSources[index].clip = audioClips[index];
            audioSources[index].Play();
        }
    }

    public void Fadeout(int index)
    {
        StartCoroutine(C_Fadeout(index));
    }

    private IEnumerator C_Fadeout(int index)
    {
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;
            if (time > 3f)
                break;

            audioSources[index].volume -= 0.005f;

            yield return null;
        }
    }
}