using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Microgame Prefabs")]
    [SerializeField] private List<GameObject> microgamePrefabs;

    [Header("UI Global")]
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private TMPro.TextMeshProUGUI timerText;

    private int currentIndex = 0;
    private GameObject currentMicrogameObject;
    private IMicrogame currentMicrogame;

    //private float gameTimer = 8f;
    private bool isPlaying = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        LoadNextMicrogame();
    }

    private void Update()
    {
        if (!isPlaying) return;

        if (currentMicrogame.IsCompleted || currentMicrogame.IsFailed)
        {
            EndMicrogame();
        }
    }


    private void LoadNextMicrogame()
    {
        if (currentIndex >= microgamePrefabs.Count)
        {
            Debug.Log("microjuegos completados.");
            //pantalla de resultados
            return;
        }

        var prefab = microgamePrefabs[currentIndex];
        currentMicrogameObject = Instantiate(prefab);
        currentMicrogame = currentMicrogameObject.GetComponent<IMicrogame>();

        if (currentMicrogame == null)
        {
            Debug.LogError($"Prefab {prefab.name} no tiene IMicrogame.");
            return;
        }
        Debug.Log("Instanciando microjuego: " + prefab.name);


        //gameTimer = 5f;
        currentMicrogame.StartGame();
        isPlaying = true;
    }

    private IEnumerator EndMicrogameRoutine()
    {
        isPlaying = false;

        //finaliza microjuego
        currentMicrogame.EndGame();

        //espera el tiempo del zoom
        yield return new WaitForSeconds(2f);

        //destruye el microjuego actual
        Destroy(currentMicrogameObject);

        //activa camara global o de la escena
        if (mainCanvas != null)
            mainCanvas.enabled = true;

        Camera globalCam = Camera.main;
        if (globalCam != null)
            globalCam.enabled = true;

        Debug.Log("Mstrando pantalla de vida");

        yield return new WaitForSeconds(2f); //llama al sig microjuego
        currentIndex++;
        LoadNextMicrogame();
    }

    private void EndMicrogame()
    {
        StartCoroutine(EndMicrogameRoutine());
    }


}

