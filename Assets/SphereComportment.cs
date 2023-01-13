using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class SphereComportment : MonoBehaviour
{
    private GameManager _gameManager;
    [SerializeField] private GameObject _forcefieldSphere;
    [SerializeField] private Rigidbody _enemyRb;
    [SerializeField] private BoxCollider _enemyCollider;
    [SerializeField] private BotMovement _sphereMovment;


    [SerializeField] private Weight _weight;
    private GameObject _botTarget;



    // Start is called before the first frame update
    void Start()
    {

        _enemyRb.constraints = RigidbodyConstraints.FreezeRotation;


    }






    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameManager.Instance.GetScore() <= _weight.weight)
        {
            _sphereMovment.AddTarget(collision.gameObject);
            _enemyCollider.enabled = false;
        }
        else if (collision.gameObject.CompareTag("Stickable") && collision.gameObject.GetComponent<Weight>().weight <= _weight.weight)
        {
            _sphereMovment.AddTarget(collision.gameObject);
            _enemyCollider.enabled = false;
        }
        else 
        {
            _sphereMovment.MoveRandomly();

        }



    }





}

