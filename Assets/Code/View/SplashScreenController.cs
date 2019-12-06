using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SplashScreenController : MonoBehaviour
{
    [SerializeField] private VideoPlayer _videoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        _videoPlayer.loopPointReached += playableDirector => SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(1);
        }
    }
}
