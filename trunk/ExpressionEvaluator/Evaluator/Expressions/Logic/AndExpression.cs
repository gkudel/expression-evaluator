﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionEvaluator.Evaluator.Expressions.Logic
{
    internal class AndExpression : BinaryExpression
    {
        #region Constructor
        public AndExpression(Expression e1, Expression e2)
            : base(e1, e2)
        {
        }
        #endregion Constructor

        #region Properties
        internal override string Name { get { return "AndExpression"; } }
        #endregion Properties

        #region Evaluate
        internal override Expression[] Evaluate(Expression[] values, out bool evaluated)
        {
            evaluated = false;
            if (values[0].BoolValue.HasValue && values[1].BoolValue.HasValue)
            {
                evaluated = true;
                return new Expression[] { new ConstExpression(values[0].BoolValue.Value && values[1].BoolValue.Value) } ;
            }
            return null;
        }
        #endregion Evaluate
    }
}
