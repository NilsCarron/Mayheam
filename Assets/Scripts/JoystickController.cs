using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField]
    public GameObject joystickBack;
    public GameObject joystick;
    public GameObject sphere;
    [SerializeField] private Rigidbody _sphereRb;

    
    private RectTransform _transformBack;
    private RectTransform _transformJoystick;
    private Transform _sphereTransform;
    private float _radius;
    private float _speed;
    
    private Vector3 _vectorMove;
    private bool _isTouching = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _transformBack=joystickBack.GetComponent<RectTransform>();
        _transformJoystick = joystick.GetComponent<RectTransform>();
        //_transformBack = transform.Find("JoystickBack").GetComponent<RectTransform>();
        //_transformJoystick = transform .Find("JoystickBack/Joystick").GetComponent<RectTransform>();
        _sphereTransform = sphere.transform;
        _radius = _transformBack.rect.width * 0.5f;
        

    }
    void FixedUpdate()
    {
        if (_isTouching)
        {
            _speed  = GameManager.Instance.GetScore() +1 ;
            //_sphereTransform.position += _vectorMove;
            _sphereRb.AddForce(_vectorMove);
            
        }

    }

    void OnTouch(Vector2 vecTouch)
    {
        
        Vector2 vec = new Vector2(vecTouch.x - _transformBack.position.x, vecTouch.y - _transformBack.position.y);

        
        vec = Vector2.ClampMagnitude(vec, _radius);
        _transformJoystick.localPosition = vec;

        float fSqr = (_transformBack.position - _transformJoystick.position).sqrMagnitude / (_radius * _radius);
        
        Vector2 vecNormal = vec;

        _vectorMove = new Vector3(vecNormal.x * _speed * Time.deltaTime * fSqr, 0f,
            vecNormal.y * _speed * Time.deltaTime * fSqr);
       // _sphereTransform.eulerAngles = new Vector3(0f, Mathf.Atan2(vecNormal.x, vecNormal.y) * Mathf.Rad2Deg, 0f);
        
        


    }

    public void OnDrag(PointerEventData eventData)
    {
        OnTouch(eventData.position);
        _isTouching = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        joystickBack.transform.position = eventData.position;

        OnTouch(eventData.position);
        
        _isTouching = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _transformJoystick.localPosition = Vector3.zero;
        _isTouching = false;
    }

 
}
