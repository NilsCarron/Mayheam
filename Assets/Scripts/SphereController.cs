using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class SphereController : MonoBehaviour
{
    private GameManager _gameManager;
    [SerializeField] private Transform _cameraTransform;
    [SerializeField] private float _rollSpeed;
    [SerializeField] private GameObject _forcefieldSphere;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] public GameObject _plane;


    // Start is called before the first frame update
    void Start()
    {




    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Stickable") && collision.gameObject.GetComponent<Weight>().weight <= GameManager.Instance.GetScore() + 1)
        {
            collision.transform.parent = transform;
            Destroy(collision.gameObject.GetComponent<BoxCollider>());


            GameManager.Instance.AddScore(collision.gameObject.GetComponent<Weight>().weight / 2 + UnityEngine.Random.Range(-0.10f, 0.10f));
            if (collision.gameObject.GetComponent<Weight>().weight < 100)
            {
                _forcefieldSphere.transform.localScale = new Vector3(_forcefieldSphere.transform.localScale.x + 0.05f * collision.gameObject.GetComponent<Weight>().weight / 2 + UnityEngine.Random.Range(-0.10f, 0.10f), _forcefieldSphere.transform.localScale.y + 0.05f * collision.gameObject.GetComponent<Weight>().weight / 2 + UnityEngine.Random.Range(-0.10f, 0.10f), _forcefieldSphere.transform.localScale.z + 0.05f * collision.gameObject.GetComponent<Weight>().weight / 2 + UnityEngine.Random.Range(-0.10f, 0.10f));

                _camera.GetComponentInChildren<CinemachineFramingTransposer>().m_CameraDistance += 1f * collision.gameObject.GetComponent<Weight>().weight / 10 + UnityEngine.Random.Range(-0.10f, 0.10f);
            }
            else if (collision.gameObject.GetComponent<Weight>().weight >= 5000)
            {
                Destroy(_plane);
            }


            else

            {
                _forcefieldSphere.transform.localScale = new Vector3(_forcefieldSphere.transform.localScale.x + 0.05f * collision.gameObject.GetComponent<Weight>().weight / 100 + UnityEngine.Random.Range(-0.10f, 0.10f), _forcefieldSphere.transform.localScale.y + 0.05f * collision.gameObject.GetComponent<Weight>().weight / 100 + UnityEngine.Random.Range(-0.10f, 0.10f), _forcefieldSphere.transform.localScale.z + 0.05f * collision.gameObject.GetComponent<Weight>().weight / 100 + UnityEngine.Random.Range(-0.10f, 0.10f));

                _camera.GetComponentInChildren<CinemachineFramingTransposer>().m_CameraDistance += 1f * collision.gameObject.GetComponent<Weight>().weight / 100 + UnityEngine.Random.Range(-0.10f, 0.10f);

            }
        }

        }
    }
