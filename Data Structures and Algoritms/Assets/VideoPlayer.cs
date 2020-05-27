using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(VideoPlayer))]
public class VideoPlayer : MonoBehaviour
{
    UnityEngine.Video.VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<UnityEngine.Video.VideoPlayer>();
        Invoke("Iniciar", 2);
    }

    public void Iniciar()
    {
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "aubrey.mp4");
        videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
