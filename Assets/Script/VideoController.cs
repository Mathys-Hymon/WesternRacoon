using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoController : MonoBehaviour
{
    public VideoClip[] videoClips;
    public int nextSceneIndex;
    [Range(0f,1f)]public float playerVolume=1f;
    private int currentClipIndex = 0;
    private VideoPlayer videoPlayer1;

    void Start()
    {
        videoPlayer1 = gameObject.AddComponent<VideoPlayer>();

        InitializeVideoPlayer(videoPlayer1);

        PlayNextClip();
    }

    void InitializeVideoPlayer(VideoPlayer player)
    {
        player.playOnAwake = false;
        player.renderMode = VideoRenderMode.CameraFarPlane;
        player.targetCamera = Camera.main;
        player.loopPointReached += VideoFinished;
        player.SetDirectAudioVolume(0,playerVolume);
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            LoadNextScene();
        }
    }

    void PlayNextClip()
    {
        if (currentClipIndex < videoClips.Length)
        {

                videoPlayer1.clip = videoClips[currentClipIndex];
                videoPlayer1.Prepare();
                videoPlayer1.Play();

            currentClipIndex++;
        }
        else
        {
            LoadNextScene();
        }
    }

    void VideoFinished(VideoPlayer vp)
    {
        PlayNextClip();
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }
}
