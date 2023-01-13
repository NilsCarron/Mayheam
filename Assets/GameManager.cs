using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   private static GameManager _instance;

   [SerializeField] private GameObject _sphere;
    private float _timePlayed;

    [SerializeField]  private TextMeshProUGUI _textMeshPro;
    [SerializeField] private TextMeshProUGUI _Ending;
    bool end;

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

    public void EndOfTHeGame()
    {
        end = true;
        _Ending.text = "You finished the level in " + _timePlayed + " Seconds! The High Score is " ;
        
        if((PlayerPrefs.GetFloat("highScore") < _timePlayed))
        {
            _Ending.text = "You finished the level in " + _timePlayed + "Seconds! The high score is still at " + PlayerPrefs.GetFloat("highScore");

        }
        else
        {
            _Ending.text = "You finished the level in " + _timePlayed + " Seconds! This is the new high score, congratulation!";
            PlayerPrefs.SetFloat("highScore", _timePlayed);

        }
        PlayerPrefs.Save();
        StartCoroutine(LoadEndOfGame());


    }

    IEnumerator LoadEndOfGame()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        if(!end)
        _timePlayed += Time.deltaTime;


    }


    public float GetScore()
   {
      return _score;
   }

    

   public void AddScore(float size)
   {


      _score += size;
        _textMeshPro.text = _score.ToString() + " Kg";

    }


    private void Awake()
   {
      _instance = this;
      _score = 1;
        end = false;
   }
}
