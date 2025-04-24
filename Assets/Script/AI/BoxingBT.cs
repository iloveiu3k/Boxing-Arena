// using UnityEngine;
// using System.Collections.Generic;
// public class BoxingBT : MonoBehaviour
// {
//     [Header("AI & Target")]
//     public BoxingAI _boxingAI;   
//     public GameObject _boxerOppenent;
//     private BTNode _root;
//     private Boxer _boxer;
//     private GameManager _gameManager;
//     void Start()
//     {
//         _boxer = GetComponent<Boxer> ();
//         _boxerOppenent = _boxer.BoxerFocus.GetBoxerFocus();
//         _boxingAI = _boxer.BoxingAI;
//         _gameManager = GameManager.Instance;
//         _boxer.BoxerFocus.OnChangeFocus += OnBoxerFocusChanged;
//         // 1. Block khi player chuẩn bị đấm
//         var blockSeq = new Sequence(new List<BTNode> {
//             new ConditionNode  ( () => AIUtils.PlayerIsWindingUp(_boxerOppenent.transform,!_boxer.BoxerAttack.GetIsBlock(), _gameManager.GetDifficultLevel())
//                                         && _boxingAI.CanActionNode()),
//             new ActionNode     ( () => { _boxingAI.Block(); return NodeState.Success; })
//         });

//         // 2. Attack khi vào phạm vi
//         var attackSeq = new Sequence(new List<BTNode> {
//             new ConditionNode  ( () => AIUtils.DistanceBetween(_boxerOppenent.transform, transform) <= _boxingAI.attackRange ),
//             new ActionNode     ( () => { _boxingAI.Attack(); return NodeState.Success; })
//         });

//         // 3. Retreat khi quá gần
//         var retreatSeq = new Sequence(new List<BTNode> {
//             new ConditionNode  ( () => _boxingAI.DistanceTo(_boxerOppenent.transform) < _boxingAI.minSafeDistance ),
//             new ActionNode     ( () => { _boxingAI.Retreat(); return NodeState.Success; })
//         });

//         // 4. Approach khi quá xa
//         var approachSeq = new Sequence(new List<BTNode> {
//             new ConditionNode  ( () => _boxingAI.DistanceTo(_boxerOppenent.transform) > _boxingAI.optimalDistance ),
//             new ActionNode     ( () => { _boxingAI.Approach(); return NodeState.Success; })
//         });

//         // 5. Default: Idle
//         var idleAction = new ActionNode(() => { _boxingAI.Idle(); return NodeState.Success; });

//         // Root selector sẽ thử lần lượt block → attack → retreat → approach → idle
//         _root = new Selector(new List<BTNode> {
//             blockSeq, attackSeq, retreatSeq, approachSeq, idleAction
//         });
//     }

//     void Update()
//     {
//         if(_boxerOppenent == null)
//         {
//             _boxerOppenent = _boxer.BoxerFocus.GetBoxerFocus();
//         }
//         _root.Tick();
//     }

//     private void OnBoxerFocusChanged()
//     {
//         _boxerOppenent = _boxer.BoxerFocus.GetBoxerFocus();
//     }
// }
