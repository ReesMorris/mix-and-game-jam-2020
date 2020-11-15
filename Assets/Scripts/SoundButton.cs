using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour {

  public enum Types { Music, Sound };
  public Types type;
  public Image iconImage;
  public Sprite mutedIcon;
  public Sprite unmutedIcon;

  private AudioManager audioManager;
  private bool isMuted;

  void Start() {
    audioManager = GameObject.Find("GameManager").GetComponent<AudioManager>();
  }

  public void OnClick() {
    isMuted = !isMuted;
    if (type == Types.Music) audioManager.MuteMusic(isMuted);
    if (type == Types.Sound) audioManager.MuteSFX(isMuted);

    iconImage.sprite = isMuted ? mutedIcon : unmutedIcon;
  }
}
