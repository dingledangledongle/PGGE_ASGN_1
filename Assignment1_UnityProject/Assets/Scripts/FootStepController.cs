using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepController : MonoBehaviour
{
    public AudioClip[] footStepAudioArray;
    public AudioSource playerAudioSource;
    public Animator playerAnimator;

    float minWalkVol = 0.3f;
    float maxWalkVol = 0.5f;
    float minWalkPitch = 0.5f;
    float maxWalkPitch = 1f;

    float minRunVol = 0.7f;
    float maxRunVol = 1f;
    float minRunPitch = 1f;
    float maxRunPitch = 2f;

    void WalkFootStep()
    {
        //Debug.DrawRay(transform.position + transform.up + transform.up, transform.forward * 5, Color.magenta,0.1f);
        int randIndex = Random.Range(0, footStepAudioArray.Length);
        float randVol = Random.Range(minWalkVol, maxWalkVol);
        float randPitch = Random.Range(minWalkPitch, maxWalkPitch);

        playerAudioSource.volume = randVol;
        playerAudioSource.pitch = randPitch;
        playerAudioSource.PlayOneShot(footStepAudioArray[randIndex]);
    }

    void RunFootStep()
    {
        int randIndex = Random.Range(0, footStepAudioArray.Length);
        float randVol = Random.Range(minRunVol, maxRunVol);
        float randPitch = Random.Range(minRunPitch, maxRunPitch);

        playerAudioSource.volume = randVol;
        playerAudioSource.pitch = randPitch;
        playerAudioSource.PlayOneShot(footStepAudioArray[randIndex]);
    }
}
