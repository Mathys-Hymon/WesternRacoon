using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField] private AudioClip jumpSfx, doubleJumpSfx, walkSfx, rollSfx, shootSfx, deathSfx;
    //private float volume;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(SoundFX sound)
    {
        switch (sound)
        {
            case SoundFX.Jump:
                _audioSource.PlayOneShot(jumpSfx);
                break;
            case SoundFX.DoubleJump :
                _audioSource.PlayOneShot(doubleJumpSfx);
                break;
            // case SoundFX.Walk:
            //     _audioSource.PlayOneShot(walkSfx);
            //     break;
             case SoundFX.Roll:
                 _audioSource.PlayOneShot(rollSfx);
                break;
            case SoundFX.Shoot:
                _audioSource.PlayOneShot(shootSfx);
                break;
            case SoundFX.Death:
                _audioSource.PlayOneShot(deathSfx);
                break;
        }
    }

}

public enum SoundFX
{
    Jump,
    DoubleJump,
    // Walk,
    Roll,
    Shoot,
    Death
}

