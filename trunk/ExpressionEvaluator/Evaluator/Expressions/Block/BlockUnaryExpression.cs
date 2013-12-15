using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionEvaluator.Evaluator.Expressions.Block
{
    public class BlockUnaryExpression : UnaryExpression
    {
        #region Constructor
        public BlockUnaryExpression(Expression e1)
            : base(e1)
        {
            LocalVariable = false;
            InnerStack = true;
        }
        #endregion Constructor

        #region Properties
        public override string Name { get { return "BlockUnaryExpression"; } }
        #endregion Properties

        #region Evaluate
        internal override Expression[] Evaluate(Expression[] values, out bool evaluated)
        {
            throw new EvaluateException("Syntax Error");
        }
        #endregion Evaluate

        #region Push
        internal override void Push(Expression e)
        {
            if (e is RightBraceExpression)
            {
                Expression expression = ExpressionStack.Peek();
                if (!expression.InnerStack)
                {
                    InnerStackCompleted = true;
                }
                else if (expression.InnerStack)
                {
                    if (expression.InnerStackCompleted)
                    {
                        InnerStackCompleted = true;
                    }
                    else
                    {
                        expression.Push(e);
                    }
                }
            }
            else
            {
                base.Push(e);
            }
        }
        #endregion Push
    }
}
