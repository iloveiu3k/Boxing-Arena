using UnityEngine;

public static class AIUtils
{
    /// <summary>
    /// Trả về true nếu Animator của player đang ở state wind‑up (ví dụ bắt đầu animation punch).
    /// Bạn cần gán tag "WindUp" cho các state wind‑up trong Animator.
    /// </summary>
    public static bool PlayerIsWindingUp(Transform boxer,bool isBlock)
    {
        if (boxer == null) return false;
        Animator anim = boxer.GetComponentInChildren<Animator>();
        if (anim == null) return false;
        if(isBlock) return false;
        // Lấy thông tin state hiện tại ở layer 0
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(1);
        // Kiểm tra nếu state đó có tag "WindUp"
        return info.IsTag("Punch");
    }


    /// <summary>
    /// Dự đoán vị trí player sau một khoảng delta (dùng velocity).
    /// </summary>
    public static Vector3 PredictPlayerPosition(Transform player, Vector3 lastPos, float deltaTime, float factor)
    {
        Vector3 vel = (player.position - lastPos) / deltaTime;
        return player.position + vel * factor;
    }
    public static bool CanActionNode(float level)
    {
        float randomValue = UnityEngine.Random.Range(0f, 10f);
        Debug.Log(randomValue);
        return randomValue <= level;    
    }
}
