// File: GoapAction.cs
using UnityEngine;
using System.Collections.Generic;

public abstract class GoapAction {
    public float Cost { get; protected set; } = 1f;
    protected Dictionary<string, object> preconditions = new();
    protected Dictionary<string, object> effects = new();
    private bool inProgress = false;
    public bool IsDone { get; protected set; } = false;
    public GameObject Target { get; protected set; }

    public IReadOnlyDictionary<string, object> Preconditions => preconditions;
    public IReadOnlyDictionary<string, object> Effects       => effects;
    public bool RequiresInRange => false;

    public void AddPrecondition(string key, object value) => preconditions[key] = value;
    public void AddEffect(string key, object value)       => effects[key] = value;

    public void ResetState() {
        inProgress = false;
        IsDone = false;
    }

    /// <summary>
    /// Kiểm tra điều kiện runtime (ví dụ tìm mục tiêu).
    /// </summary>
    public abstract bool CheckProceduralPrecondition(GameObject agent);

    /// <summary>
    /// Thực thi hành động. Trả true nếu thành công.
    /// </summary>
    public abstract bool Perform(GameObject agent);

    /// <summary>
    /// Cho phép override để tính cost dựa trên worldState.
    /// Mặc định return Cost.
    /// </summary>
    public virtual float GetCost(Dictionary<string, object> currentState) {
        return Cost;
    }
}
