using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
   private static GameManager _instance;

   [SerializeField] private GameObject _sphere;

    [SerializeField]  private TextMeshProUGUI _textMeshPro;
    private static float _score;
   public static GameManager Instance
   {
      get
      {
         if(_instance == null)
            Debug.LogError("Null");
         return _instance;
      }
   }

   public float GetScore()
   {
      return _score;
   }

   public void AddScore(float size)
   {


      _score += size;
        _textMeshPro.text = _score.ToString() + "Kg";

    }


    private void Awake()
   {
      _instance = this;
      _score = 1;
   }
}
