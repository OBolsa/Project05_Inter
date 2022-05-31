using System.Collections.Generic;

namespace BehaviourTree
{
    public class BTSequence : BTNode
    {
        public BTSequence() : base() { }
        public BTSequence(List<BTNode> children) : base(children) { }

        public override BTNodeState Evaluate()
        {
            bool anyChildIsRunning = false;

            foreach (BTNode node in children)
            {
                switch (node.Evaluate())
                {
                    case BTNodeState.FAILURE:
                        state = BTNodeState.FAILURE;
                        break;
                    case BTNodeState.SUCCESS:
                        continue;
                    case BTNodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        state = BTNodeState.SUCCESS;
                        return state;
                }
            }

            state = anyChildIsRunning ? BTNodeState.RUNNING : BTNodeState.SUCCESS;
            return state;
        }
    }
}