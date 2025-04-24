using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeAreaFitter : MonoBehaviour
{
    RectTransform _rt;

    void Awake()
    {
        _rt = GetComponent<RectTransform>();
        ApplySafeArea();
    }

#if UNITY_EDITOR
    void Update() => ApplySafeArea();
#endif

    void ApplySafeArea()
    {
        Rect safe = Screen.safeArea;

        // Tính anchor (0–1) từ pixel safe area
        Vector2 anchorMin = safe.position;
        Vector2 anchorMax = safe.position + safe.size;
        anchorMin.x /= Screen.width;   anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;   anchorMax.y /= Screen.height;

        // Gán cho RectTransform của panel
        _rt.anchorMin = anchorMin;
        _rt.anchorMax = anchorMax;
        _rt.offsetMin = _rt.offsetMax = Vector2.zero;
    }
}
