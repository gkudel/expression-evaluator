﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lambda = System.Linq.Expressions;

namespace ExpressionEvaluator.Evaluator.Expressions.Methods
{
    public class CheckExpression : UnaryExpression
    {
        #region Constructor
        public CheckExpression(Expression e1)
            : base(e1)
        {
            _acceptedType = AcceptedType.Bool;
        }
        #endregion Constructor

        #region Properties
        public override string Name { get { return "CheckExpression"; } }
        #endregion Properties

        #region Evaluate
        internal override Expression[] Evaluate(Expression[] values, out bool evaluated)
        {
            evaluated = false;
            if (values[0].BoolValue.HasValue)
            {
                evaluated = true;
                return new Expression[] { new ConstExpression(values[0].BoolValue.Value) };
            }
            else if (values[0].NumericValue.HasValue)
            {
                evaluated = true;
                return new Expression[] { new ConstExpression(values[0].NumericValue.Value > 0) };
            }
            else if (values[0].StringValue != null)
            {
                evaluated = true;
                return new Expression[] { new ConstExpression(values[0].StringValue.Length > 0) };
            }
            return null;
        }
        #endregion Evaluate

        #region Lambda Compilation
        internal override lambda.Expression CompileBoolBlock(lambda.ParameterExpression paramBool1, lambda.LabelTarget fault)
        {
            return paramBool1;
        }
        #endregion Lambda Compilation
    }
}
