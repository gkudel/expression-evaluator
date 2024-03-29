﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lambda = System.Linq.Expressions;

namespace ExpressionEvaluator.Evaluator.Expressions.Compare
{
    public class GreterExpression : BinaryExpression
    {
        #region Constructor
        public GreterExpression(Expression e1, Expression e2)
            : base(e1, e2)
        {
        }
        #endregion Constructor

        #region Properties
        public override string Name { get { return "GreterExpression"; } }
        #endregion Properties

        #region Evaluate
        internal override Expression[] Evaluate(Expression[] values, out bool evaluated)
        {
            evaluated = false;
            if (values[0].NumericValue.HasValue && values[1].NumericValue.HasValue)
            {
                evaluated = true;
                return new Expression[] { new ConstExpression(values[0].NumericValue.Value > values[1].NumericValue.Value) };
            }
            else if (values[0].DataTimeValue.HasValue && values[1].DataTimeValue.HasValue)
            {
                evaluated = true;
                return new Expression[] { new ConstExpression(values[0].DataTimeValue.Value.CompareTo(values[1].DataTimeValue.Value) > 0 ) }; 
            }
            return null;
        }
        #endregion Evaluate

        #region Lambda Compilation
        internal override lambda.Expression CompileNumericBlock(lambda.ParameterExpression paramNumeric1, lambda.ParameterExpression paramNumeric2)
        {
            return lambda.Expression.GreaterThan(paramNumeric1, paramNumeric2);
        }
        #endregion Lambda Compilation
    }
}
