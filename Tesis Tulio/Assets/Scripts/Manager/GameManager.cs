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

    private float gameTimer = 7f;
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

        gameTimer -= Time.deltaTime;
        timerText.text = Mathf.Ceil(gameTimer).ToString();

        if (gameTimer <= 0 || currentMicrogame.IsCompleted || currentMicrogame.IsFailed)
        {
            EndMicrogame();
        }
    }

    private void LoadNextMicrogame()
    {
        if (currentIndex >= microgamePrefabs.Count)
        {
            Debug.Log("microjuegos completados.");
            //pantalla de resultados.
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

        gameTimer = 5f;
        currentMicrogame.StartGame();
        isPlaying = true;
    }

    private void EndMicrogame()
    {
        isPlaying = false;
        currentMicrogame.EndGame();
        Destroy(currentMicrogameObject);
        currentIndex++;
        Invoke(nameof(LoadNextMicrogame), 0.5f);
    }
}

