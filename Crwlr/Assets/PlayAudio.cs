using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    void PlaySound(AudioClip clip) {
        AudioSource.PlayClipAtPoint(clip, transform.position, 1);
    }

    void PlaySoundRandomVolume(AudioClip clip) {
        AudioSource.PlayClipAtPoint(clip, transform.position, Random.Range(0.5f,1.0f));
    }
}
