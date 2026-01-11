using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    [Header("Pizarrón")]
    public Image instructionImage;     // La imagen que cambia
    public Animator instructionAnimator; // FadeIn / FadeOut

    [Header("Teacher")]
    public Animator teacherAnimator;   // Animator del teacher

    [Header("Datos del tutorial")]
    public Sprite[] tutorialImages;    // PNGs del pizarrón

    int currentIndex = 0;

    // Llama a esto para mostrar un paso
    public void ShowStep(int index, int gesture)
    {
        if (index < 0 || index >= tutorialImages.Length) return;

        currentIndex = index;
        StartCoroutine(ChangeStepRoutine(gesture));
    }

    System.Collections.IEnumerator ChangeStepRoutine(int gesture)
    {
        // Fade OUT del pizarrón
        instructionAnimator.Play("FadeOut");
        yield return new WaitForSeconds(0.3f);

        // Cambia imagen
        instructionImage.sprite = tutorialImages[currentIndex];

        // Cambia gesto del teacher
        teacherAnimator.SetInteger("Gesture", gesture);

        // Fade IN del pizarrón
        instructionAnimator.Play("FadeIn");
    }
}
