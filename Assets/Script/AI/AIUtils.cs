using UnityEngine;

public static class AIUtils
{
    public static bool PlayerIsWindingUp(Transform boxer,bool isBlock)
    {
        if (boxer == null) return false;
        Animator anim = boxer.GetComponentInChildren<Animator>();
        if (anim == null) return false;
        if(isBlock) return false;
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(1);
        return info.IsTag("Punch");
    }



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
