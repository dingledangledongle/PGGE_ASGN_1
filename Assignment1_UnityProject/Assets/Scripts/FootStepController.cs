using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepController : MonoBehaviour
{
    AudioClip[] currentGroundFootSteps; //this array is being updated in Player.cs in DetectGroundType()
    public AudioSource playerAudioSource;
    public Animator playerAnimator;
    [SerializeField]
    AudioClip[] concreteFootSteps, dirtFootSteps, metalFootSteps
    , sandFootSteps, woodFootSteps;
    public LayerMask groundMask;

    float minWalkVol = 0.3f;
    float maxWalkVol = 0.5f;
    float minWalkPitch = 0.5f;
    float maxWalkPitch = 1f;

    float minRunVol = 0.7f;
    float maxRunVol = 1f;
    float minRunPitch = 1f;
    float maxRunPitch = 2f;

    void WalkFootStep() //this method is called from an animation event for walking
    {
        DetectGroundType();
        int randIndex = Random.Range(0, currentGroundFootSteps.Length);

        //sets up the volume and pitch of the walking soundclip
        float randVol = Random.Range(minWalkVol, maxWalkVol);
        float randPitch = Random.Range(minWalkPitch, maxWalkPitch);
        playerAudioSource.volume = randVol;
        playerAudioSource.pitch = randPitch;

        //play a single instance of a random sound in the array
        playerAudioSource.PlayOneShot(currentGroundFootSteps[randIndex]);
    }

    void RunFootStep()//this method is called from an animation event for running
    {
        DetectGroundType();
        int randIndex = Random.Range(0, currentGroundFootSteps.Length);

        //sets up the volume and pitch of the running soundclip
        float randVol = Random.Range(minRunVol, maxRunVol);
        float randPitch = Random.Range(minRunPitch, maxRunPitch);
        playerAudioSource.volume = randVol;
        playerAudioSource.pitch = randPitch;

        //play a single instance of a random sound in the array
        playerAudioSource.PlayOneShot(currentGroundFootSteps[randIndex]);
    }

    void DetectGroundType()
    {
        //works but not very consistent
        Physics.Raycast(transform.position, -transform.up, out RaycastHit hit, 1, groundMask); //cast ray towards the ground to detect the ground type
        Debug.DrawRay(transform.position, -transform.up * 1, Color.cyan,0.1f);

        switch (hit.collider.tag) //switches the sound array depending on the ground type
        {
            case "Concrete":
                currentGroundFootSteps = concreteFootSteps;
                break;
            case "Dirt":
                currentGroundFootSteps = dirtFootSteps;
                break;
            case "Metal":
                currentGroundFootSteps = metalFootSteps;
                break;
            case "Sand":
                currentGroundFootSteps = sandFootSteps;
                break;
            case "Wood":
                currentGroundFootSteps = woodFootSteps;
                break;
            default:
                currentGroundFootSteps = concreteFootSteps;
                break;
        }
    }
}
