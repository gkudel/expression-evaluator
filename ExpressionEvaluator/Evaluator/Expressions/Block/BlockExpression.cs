using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEvaluator.Evaluator.Expressions.Block
{
    public class BlockExpression : Expression
    {
        #region Constructor
        public BlockExpression()
            : base()
        {
            LocalVariable = false;
            InnerStack = true;
        }
        #endregion Constructor

        #region Properties
        public override string Name { get { return "BlockExpression"; } }
        #endregion Properties

        #region Evaluate
        internal override Expression[] Evaluate(Expression[] values, out bool evaluated)
        {
            evaluated = true;
            object o = base.Evaluate(null, out evaluated);
            if (evaluated)
            {
                return new Expression[] { new ConstExpression(o) };
            }
            return null;
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
