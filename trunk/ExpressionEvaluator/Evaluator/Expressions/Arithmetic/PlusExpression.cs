using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using lambda = System.Linq.Expressions;

namespace ExpressionEvaluator.Evaluator.Expressions.Arithmetic
{
    public class PlusExpression : BinaryExpression
    {
        #region Members
        protected static MethodInfo _miConncatString;        
        #endregion Members

        #region Constructor
        static PlusExpression()
        {
            _miConncatString = typeof(string).GetMethod("Concat", new[] { typeof(string), typeof(string) });
        }

        public PlusExpression(Expression e1, Expression e2)
            : base (e1, e2)
        {
            _acceptedType = AcceptedType.Numeric | AcceptedType.String;
        }
        #endregion Constructor

        #region Properties
        public override string Name { get { return "PlusExpression"; } }
        #endregion Properties

        #region Evaluate
        internal override Expression[] Evaluate(Expression[] values, out bool evaluated)
        {
            evaluated = false;
            if (values[0].NumericValue.HasValue && values[1].NumericValue.HasValue)
            {
                evaluated = true;
                return new Expression[] { new ConstExpression(values[0].NumericValue.Value + values[1].NumericValue.Value) };
            } 
            else if (values[0].StringValue != null && values[1].StringValue != null)
            {
                evaluated = true;
                return new Expression[] { new ConstExpression(values[0].StringValue + values[1].StringValue) };
            }
            return null;
        }
        #endregion Evaluate

        #region Lambda Compilation
        internal override lambda.Expression CompileNumericBlock(lambda.ParameterExpression paramNumeric1, lambda.ParameterExpression paramNumeric2)
        {
            return lambda.Expression.Add(paramNumeric1, paramNumeric2);
        }

        internal override lambda.Expression CompileStringBlock(lambda.ParameterExpression paramString1, lambda.ParameterExpression paramString2)
        {
            return lambda.Expression.Call(_miConncatString, paramString1, paramString2 );
        }
        #endregion Lambda Compilation
    }
}
