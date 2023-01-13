using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;
public class SphereController : MonoBehaviour
{
    private GameManager _gameManager;
    [SerializeField] private GameObject _forcefieldSphere;
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] public GameObject _plane;
    [SerializeField] private Rigidbody _sphereRb;
    [SerializeField] private AudioClip _popSound;
    [SerializeField] private AudioSource _audio;

    private List <GameObject> _currentCollisions = new List <GameObject> ();
    private Vector3 _previousPosition;
    private bool _checkingIsfStuck;
   


    // Start is called before the first frame update
    void Start()
    {
        _checkingIsfStuck = false;
    }
    void FixedUpdate()
    {
        if (!_checkingIsfStuck)
        {
            _previousPosition = _sphereRb.transform.position;
            StartCoroutine(Wait2SecAndGetPosition(_previousPosition));
        }

    }
    IEnumerator Wait2SecAndGetPosition(Vector3 previousPosition)
    {
        _checkingIsfStuck = true;
        yield return new WaitForSeconds(2);
        if (previousPosition == _sphereRb.transform.position)
        {
            foreach (GameObject occlusion in _currentCollisions)
            {
                Destroy(occlusion);
            }
        }

        _checkingIsfStuck = false;
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Stickable") && collision.gameObject.GetComponent<Weight>().weight <= GameManager.Instance.GetScore() + 1.1)
        {
            collision.transform.parent = transform;
            _audio.PlayOneShot(_popSound);


            Destroy(collision.gameObject.GetComponent<BoxCollider>());
            _sphereRb.mass += collision.gameObject.GetComponent<Weight>().weight / 100;

            if (collision.gameObject.GetComponent<Weight>().weight < 100)
            {
                GameManager.Instance.AddScore(collision.gameObject.GetComponent<Weight>().weight / 2 + UnityEngine.Random.Range(-0.10f, 0.10f));

                _forcefieldSphere.transform.localScale = new Vector3(_forcefieldSphere.transform.localScale.x + 0.05f * collision.gameObject.GetComponent<Weight>().weight / 2 + UnityEngine.Random.Range(-0.10f, 0.10f), _forcefieldSphere.transform.localScale.y + 0.05f * collision.gameObject.GetComponent<Weight>().weight / 2 + UnityEngine.Random.Range(-0.10f, 0.10f), _forcefieldSphere.transform.localScale.z + 0.05f * collision.gameObject.GetComponent<Weight>().weight / 2 + UnityEngine.Random.Range(-0.10f, 0.10f));

                if (_camera.GetComponentInChildren<CinemachineFramingTransposer>().m_CameraDistance <= 1500)


                    _camera.GetComponentInChildren<CinemachineFramingTransposer>().m_CameraDistance += 1f * collision.gameObject.GetComponent<Weight>().weight / 10 + UnityEngine.Random.Range(-0.10f, 0.10f);
            }
            else if (collision.gameObject.GetComponent<Weight>().weight > 5000)
            {
                Destroy(_plane);
                _sphereRb.velocity = Vector3.down;
                _sphereRb.transform.localScale = Vector3.one;
            }
           


            else

            {
                GameManager.Instance.AddScore(collision.gameObject.GetComponent<Weight>().weight / 10 + UnityEngine.Random.Range(-0.10f, 0.10f));

                _forcefieldSphere.transform.localScale = new Vector3(_forcefieldSphere.transform.localScale.x + 0.05f * collision.gameObject.GetComponent<Weight>().weight / 100 + UnityEngine.Random.Range(-0.10f, 0.10f), _forcefieldSphere.transform.localScale.y + 0.05f * collision.gameObject.GetComponent<Weight>().weight / 100 + UnityEngine.Random.Range(-0.10f, 0.10f), _forcefieldSphere.transform.localScale.z + 0.05f * collision.gameObject.GetComponent<Weight>().weight / 100 + UnityEngine.Random.Range(-0.10f, 0.10f));

                if(_camera.GetComponentInChildren<CinemachineFramingTransposer>().m_CameraDistance <= 1500)
                _camera.GetComponentInChildren<CinemachineFramingTransposer>().m_CameraDistance += 1f * collision.gameObject.GetComponent<Weight>().weight / 100 + UnityEngine.Random.Range(-0.10f, 0.10f);

            }
        }
        else
        {
            if(collision.gameObject.GetComponent<Weight>().weight <= 250)
            _currentCollisions.Add(collision.gameObject);
        }
       

    }

    private void OnCollisionExit(Collision other)
    {
        if (_currentCollisions.Contains(other.gameObject))
        {
            _currentCollisions.Remove(other.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.CompareTag("Goal"))
        {
            GameManager.Instance?.EndOfTHeGame();
        }
    }
}
