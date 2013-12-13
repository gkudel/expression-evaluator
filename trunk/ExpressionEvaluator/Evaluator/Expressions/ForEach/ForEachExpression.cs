using ExpressionEvaluator.Evaluator.Expressions.Block;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionEvaluator.Evaluator.Expressions.ForEach
{
    internal class ForEachExpression : BlockUnaryExpression
    {
        #region Constructor
        public ForEachExpression(Expression e1)
            : base(e1)
        {
            LocalVariable = true;
        }
        #endregion Constructor

        #region Properties
        internal override string Name { get { return "ForEachExpression"; } }
        #endregion Properties

        #region Evaluate
        internal override Expression[] Evaluate(Expression[] values, out bool evaluated)
        {
            evaluated = false;
            object[] ret = null;
            if (values[0].ArrayValue != null)
            {
                evaluated = true;
                ret = new object[values[0].ArrayValue.Count()];
                Parallel.For(0, values[0].ArrayValue.Count(), (i,  loopState) =>
                {
                    bool localEvaluated = true;
                    object obj = values[0].ArrayValue[i];
                    object[] row = obj as object[];
                    if (row != null)
                    {
                        SetValues(row);
                        object o = base.Evaluate(out localEvaluated);
                        if (localEvaluated)
                        {
                            ret[i] = o;
                        }
                        else
                        {                            
                            loopState.Break();
                        }
                    }
                    else
                    {
                        localEvaluated = false;
                        loopState.Break();
                    }
                });
                evaluated = ret.Count(o => o == null) == 0;
            }
            return evaluated ? new Expression[] { new ConstExpression(ret) } : null;
        }
        #endregion Evaluate

    }
}
