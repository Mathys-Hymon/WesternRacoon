using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;
    [SerializeField] private AudioClip jumpSfx, doubleJumpSfx, walkSfx, rollSfx, shootSfx, deathSfx, timeFreezeSfx;

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
             case SoundFX.Roll:
                 _audioSource.PlayOneShot(rollSfx);
                break;
            case SoundFX.Shoot:
                _audioSource.PlayOneShot(shootSfx);
                break;
            case SoundFX.Death:
                _audioSource.PlayOneShot(deathSfx);
                break;
            case SoundFX.TimeFreeze:
                _audioSource.PlayOneShot(timeFreezeSfx);
                break;
        }
    }

}

public enum SoundFX
{
    Jump,
    DoubleJump,
    Roll,
    Shoot,
    Death,
    TimeFreeze
}

