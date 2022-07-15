using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using System;

public class TamashiStateManager : MonoBehaviour
{
    TamashiBaseState currentState;

    //States
    public IdleState idle = new IdleState();
    public ActionState action = new ActionState();

    //Animation
    public TamshiAnimController tamashiAnim { get; private set; }

    public bool isPlayedTurnedIntoABox;
    void Start()
    {
        //audioSource = GetComponent<AudioSource>();
        tamashiAnim = GetComponent<TamshiAnimController>();
        //rb = GetComponent<Rigidbody>();

        currentState = idle;
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(TamashiBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
   
    public void InstantiateVFX(ParticleSystem particle)
    {
        Instantiate(particle, transform);
    }

    public void Destroy(GameObject gameObject)
    {
        Destroy(gameObject);
    }

    //public void Attack()
    //{
    //    audioSource.PlayOneShot(attack);
    //}

}
