using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

  public AudioSource music;

  private AudioSource audioSource;
  private GameObject audioFolder;
  private bool muteSFX;

  void Start() {
    audioFolder = GameObject.Find("Audio");
  }

  public void MuteMusic(bool muteMusic) {
    music.volume = muteMusic ? 0 : 1;
  }
  public void MuteSFX(bool muteSoundEffects) {
    muteSFX = muteSoundEffects;
  }

  public void PlaySound(AudioClip clip, bool randomPitch) {
    if (!muteSFX) {
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
}
