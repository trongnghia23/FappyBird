using UnityEngine;
using System.Collections;

public class ParallaxObjectFader : NghiaMono
{
    [Header("Layers & Fade")]
    [SerializeField] private GameObject[] layers;      // Các layer nền
    [SerializeField] private float fadeDuration = 1f;

    [Header("Score milestones")]
    [SerializeField] private int[] scoreMilestones;  // Mốc score để đổi layer

    private int currentIndex = 0;
    private bool isFading = false;
   
    /*─────────────────────────────────── INITIALISE ───────────────────────────────────*/
    protected override void Start()
    {
        // Tìm GameManager

        // Ẩn tất cả layer
        foreach (var obj in layers) { SetAlpha(obj, 0f); obj.SetActive(false); }

        // Bật layer đầu
        layers[0].SetActive(true);
        SetAlpha(layers[0], 1f);
    }

    /*──────────────────────────────────── UPDATE ──────────────────────────────────────*/
    void Update()
    {
        int score = GameManager.Instance.Score;   // Giả sử GameManager có thuộc tính Score (public int Score {get;} )

        // Đạt mốc score tiếp theo?
        if (!isFading &&
            currentIndex + 1 < scoreMilestones.Length &&
            score >= scoreMilestones[currentIndex + 1])
        {
            StartCoroutine(FadeLayers(currentIndex, currentIndex + 1));
            currentIndex++;
        }
    }

    /*─────────────────────────────────── FADE COROUTINE ───────────────────────────────*/
    IEnumerator FadeLayers(int fromIdx, int toIdx)
    {
        isFading = true;
        float t = 0f;

        GameObject fromObj = layers[fromIdx];
        GameObject toObj = layers[toIdx];

        toObj.SetActive(true);
        SetAlpha(toObj, 0f);

        while (t < fadeDuration)
        {
            float a = t / fadeDuration;
            SetAlpha(fromObj, 1f - a);
            SetAlpha(toObj, a);
            t += Time.deltaTime;
            yield return null;
        }

        SetAlpha(fromObj, 0f);
        SetAlpha(toObj, 1f);
        fromObj.SetActive(false);
        isFading = false;
    }

    /*─────────────────────────────────── HELPER ───────────────────────────────────────*/
    void SetAlpha(GameObject obj, float alpha)
    {
        var rend = obj.GetComponent<Renderer>();
        if (rend != null && rend.material.HasProperty("_Color"))
        {
            Color c = rend.material.color;
            c.a = alpha;
            rend.material.color = c;
        }
        else
        {
            Debug.LogWarning($"{obj.name} lacks _Color property.");
        }
    }
}
