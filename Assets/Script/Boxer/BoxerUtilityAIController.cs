using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[RequireComponent(typeof(BoxingAI))]
public class UtilityAIController : MonoBehaviour
{
    private GameObject _opponent;
    public float decisionInterval = 0.3f;    // mỗi 0.3s tính lại behavior
    private float _nextDecisionTime;
    private GoapPlanner _planner = new();
    private Queue<GoapAction> _currentPlan;
    private GoapAction _currentAction;
    private HashSet<GoapAction> _availableActions = new();
    private BoxingAI _ai;
    private GameManager _gameManager;
    private Boxer _boxer;
    void Awake()
    {
        _ai = GetComponent<BoxingAI>();
    }
    void Start()
    {
        _boxer = GetComponent<Boxer>();
        _gameManager = GameManager.Instance;
        _ai = _boxer.BoxingAI;
        _boxer.BoxerFocus.OnChangeFocus += OnChangeFocus;

    }
    // void Update()
    // {
    //     if (Time.time < _nextDecisionTime) return;

    //     // Nếu hết plan hoặc plan chưa được tính
    //     if (_currentPlan == null || _currentPlan.Count == 0)
    //     {
    //         var world = GetWorldState();
    //         var goal = GetGoalState();
    //         _currentPlan = _planner.Plan(this.gameObject, _availableActions, world, goal);
    //         if (_currentPlan == null)
    //         {
    //             Debug.LogWarning("GOAP: No plan found");
    //             _nextDecisionTime = Time.time + decisionInterval;
    //             return;
    //         }
    //         _currentAction = _currentPlan.Dequeue();
    //     }

    //     if (_currentAction != null)
    //     {
    //         if (!_currentAction.IsDone)
    //         {
    //             if (!_currentAction.CheckProceduralPrecondition(this.gameObject))
    //             {
    //                 // Precondition runtime fail → hủy plan
    //                 _currentPlan = null;
    //                 return;
    //             }
    //             _currentAction.Perform(this.gameObject);
    //         }
    //         if (_currentAction.IsDone)
    //         {
    //             // Chuyển sang action kế
    //             _currentAction = _currentPlan.Count > 0 ? _currentPlan.Dequeue() : null;
    //             _nextDecisionTime = Time.time + decisionInterval;
    //         }
    //     }
    // }


    // private Dictionary<string, object> GetWorldState()
    // {
    //     float dist = _ai.DistanceTo(_opponent.transform);
    //     return new Dictionary<string, object>
    //     {
    //         ["dist"] = dist,
    //         ["playerIsWindingUp"] = AIUtils.PlayerIsWindingUp(_opponent.transform, _boxer.BoxerAttack.GetIsBlock())
    //     };
    // }

    // private Dictionary<string, object> GetGoalState()
    // {
    //     // Ví dụ goal: đã tấn công 1 lần
    //     return new Dictionary<string, object>
    //     {
    //         ["hasBlocked"] = true,
    //         // ["hasAttacked"] = true
    //     };
    // }
    void Update()
    {
        if (Time.time < _nextDecisionTime) return;

        // 1. Tính scores cho mỗi behavior
        float dist = _ai.DistanceTo(_opponent.transform);
        var scores = new Dictionary<BehaviorType, float>
        {
            // Block: chỉ khi Player wind-up, weight theo difficulty
            [BehaviorType.Block]    = AIUtils.PlayerIsWindingUp(_opponent.transform,_boxer.BoxerAttack.GetIsBlock())
                                      ? 3f : 0f,


            // Retreat: khi quá gần, score cao
            [BehaviorType.Retreat]  = dist < _ai.minSafeDistance
                                      ?  2f : 0f,

            // Approach: khi quá xa tầm đánh
            [BehaviorType.Approach] = dist > _ai.attackRange
                                      ?  2f : 0f,

            // Attack: khi đã vào tầm đánh
            [BehaviorType.Attack]   = dist <= _ai.attackRange
                                      ?  3f : 0f,

            // Idle: luôn có 1 điểm cơ bản
            [BehaviorType.Idle] = 1f
        };

        // 2. Thêm noise nhỏ
        var keys = scores.Keys.ToList();
        foreach (var k in keys)
            scores[k] += UnityEngine.Random.Range(0f, 2f);

        // 3. Chọn behavior có score cao nhất
        var best = scores.Aggregate((a, b) => a.Value > b.Value ? a : b).Key;

        // 4. Thực thi
        ExecuteBehavior(best);

        // 5. Delay lần quyết định kế
        _nextDecisionTime = Time.time + decisionInterval;
    }

    private void ExecuteBehavior(BehaviorType beh)
    {
        switch (beh)
        {
            case BehaviorType.Block:
                _ai.Block();
                break;
            case BehaviorType.Retreat:
                _ai.Retreat();
                break;
            case BehaviorType.Approach:
                _ai.Approach();
                break;
            case BehaviorType.Attack:
                _ai.Attack();
                break;
            case BehaviorType.Idle:
                _ai.Idle();
                break;
        }
    }
    private void OnChangeFocus(GameObject focus)
    {
        _opponent = focus;
        _availableActions.Clear();
        _availableActions.Add(new BlockAction(_opponent, _ai));
        _availableActions.Add(new RetreatAction(_opponent, _ai));
        _availableActions.Add(new ApproachAction(_opponent, _ai));
        _availableActions.Add(new AttackAction(_opponent, _ai));
        _availableActions.Add(new IdleAction(_ai));
    }
}