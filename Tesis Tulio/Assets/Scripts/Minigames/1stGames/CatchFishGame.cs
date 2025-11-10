using UnityEngine;
using TMPro;
using System.Collections;

public class CatchFishGame : MonoBehaviour, IMicrogame
{
    [Header("Referencias del juego")]
    [SerializeField] private Collider2D hookZone; //marco para pescar
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Camera mainCamera;

    [Header("Configuracion")]
    [SerializeField] private float gameDuration = 6f;
    [SerializeField] private float zoomDelay = 1f; // tiempo entre panel y zoom
    [SerializeField] private float zoomDuration = 1f;
    [SerializeField] private float zoomAmount = 1.5f;

    [Header("Animación de paneles")]
    [SerializeField] private float panelZoomInScale = 1.2f;
    [SerializeField] private float panelZoomInDuration = 0.3f;
    [SerializeField] private float panelZoomOutDuration = 1f;

    private float timer;
    private bool completed;
    private bool failed;
    private bool gameRunning;

    public bool IsCompleted => completed;
    public bool IsFailed => failed;

    private void Awake()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    private void Start()
    {
        //StartGame(); // inicia automáticamente para probar el juego sin IMicrogame
    }

    public void StartGame()
    {
        Debug.Log(" Inicia CatchFishGame");
        gameRunning = true;
        timer = gameDuration;
    }

    public void EndGame()
    {
        gameRunning = false;

        // Determina panel a mostrar
        GameObject panelToAnimate = completed ? winPanel : losePanel;
        panelToAnimate.SetActive(true);

        // animación panel
        StartCoroutine(AnimatePanelZoom(panelToAnimate));
    }


    private void Update()
    {
        if (!gameRunning) return;

        //Temporizador
        timer -= Time.deltaTime;
        timerText.text = Mathf.Ceil(timer).ToString();

        //Input
        if (Input.GetKeyDown(KeyCode.Space))
            TryCatchFish();

        //time losed
        if (timer <= 0f && !completed)
        {
            failed = true;
            losePanel.SetActive(true);
            EndGame();
        }
    }

    private void TryCatchFish()
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(hookZone.bounds.center, hookZone.bounds.size, 0f);
        Debug.Log($" {hits.Length} colisiones detectadas.");

        foreach (var hit in hits)
        {
            FishMovement fish = hit.GetComponent<FishMovement>();
            if (fish != null)
            {
                if (fish.isTargetFish)
                {
                    completed = true;
                    winPanel.SetActive(true);
                    Debug.Log(" Pescado bueno");
                }
                else
                {
                    failed = true;
                    losePanel.SetActive(true);
                    Debug.Log("Pescado incorrecto");
                }
                EndGame();
                return;
            }
        }

        // Si no hubo pez tocado
        failed = true;
        losePanel.SetActive(true);
        Debug.Log("No se detectó ningún pez en hook");
        EndGame();
    }

    private IEnumerator ZoomOutAfterPanel()
    {
        //un momento antes del panel
        yield return new WaitForSeconds(zoomDelay);

        float startSize = mainCamera.orthographicSize;
        float endSize = startSize * zoomAmount;
        float t = 0f;

        while (t < zoomDuration)
        {
            mainCamera.orthographicSize = Mathf.Lerp(startSize, endSize, t / zoomDuration);
            t += Time.deltaTime;
            yield return null;
        }

        mainCamera.orthographicSize = endSize;

        // GameManager.Instance.OnMicrogameEnd(this);
        // FindObjectOfType<GameManager>().LoadNextMicrogame();
    }

    private IEnumerator AnimatePanelZoom(GameObject panel)
    {
        RectTransform rect = panel.GetComponent<RectTransform>();
        if (rect == null) yield break;

        Vector3 originalScale = Vector3.one;
        Vector3 targetScale = Vector3.one * panelZoomInScale;
        float t = 0f;

        // zoom in entra
        while (t < panelZoomInDuration)
        {
            rect.localScale = Vector3.Lerp(originalScale, targetScale, t / panelZoomInDuration);
            t += Time.deltaTime;
            yield return null;
        }
        rect.localScale = targetScale;

        yield return new WaitForSeconds(1f);

        // zoom out
        t = 0f;
        while (t < panelZoomOutDuration)
        {
            rect.localScale = Vector3.Lerp(targetScale, Vector3.zero, t / panelZoomOutDuration);
            t += Time.deltaTime;
            yield return null;
        }

        panel.SetActive(false);
        Debug.Log("Fin del microjuego");
        FindObjectOfType<GameManager>()?.StartCoroutine("EndMicrogameRoutine");

    }


    private void OnDrawGizmos()
    {
        if (hookZone != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(hookZone.bounds.center, hookZone.bounds.size);
        }
    }
}
