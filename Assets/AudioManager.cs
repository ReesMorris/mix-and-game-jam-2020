using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

  private AudioSource audioSource;
  private GameObject audioFolder;

  void Start() {
    audioFolder = GameObject.Find("Audio");
  }

  public void PlaySound(AudioClip clip, bool randomPitch) {
    if (clip) {
      GameObject item = Instantiate(new GameObject());
      AudioSource audioSource = item.AddComponent<AudioSource>();
      audioSource.clip = clip;
      if (randomPitch) audioSource.pitch = Random.Range(0.8f, 1.2f);
      audioSource.Play();
    } else {
      Debug.LogWarning("Audio clip not provided");
    }
  }
}
