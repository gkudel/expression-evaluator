﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lambda = System.Linq.Expressions;

namespace ExpressionEvaluator.Evaluator.Expressions
{
    public class ConstExpression : Expression
    {
        #region Members
        private object _value;
        #endregion Members

        #region Constructor
        internal ConstExpression(object value)
        {
            this._value = value;
            this._evaluatedType = Expression.EvaluatedType.None;
            this._evaluable = true;
        }
        #endregion Constructor

        #region Properties
        public override string Name { get { return "ConstExpression"; } }
        internal override int ArgumentsCount { get { return 0; } }
        internal override object Value { get { return _value; } }
        internal override bool Valuable { get { return true; } }
        #endregion Properties

        #region Lambda Compile 
        internal override lambda.Expression ParametrExpression(lambda.ParameterExpression values)
        {
            return lambda.Expression.Convert(lambda.Expression.Constant(Value), typeof(object));
        }
        #endregion Lambda Compile 
    }
}
