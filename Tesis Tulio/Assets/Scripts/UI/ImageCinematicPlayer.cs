using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageCinematicPlayer : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image cinematicImage;

    [Header("Cinemática")]
    [SerializeField] private List<Sprite> frames;
    [SerializeField] private float frameDuration = 2f;

    [Header("Flujo")]
    [SerializeField] private string nextScene;

    private void Start()
    {
        //FadeController.Instance.FadeIn();
        StartCoroutine(PlayCinematic());
    }

    private IEnumerator PlayCinematic()
    {
        foreach (Sprite frame in frames)
        {
            cinematicImage.sprite = frame;
            yield return new WaitForSeconds(frameDuration);
        }

        FadeController.Instance.FadeAndLoadScene(nextScene);
    }
}


