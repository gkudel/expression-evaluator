using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lambda = System.Linq.Expressions;

namespace ExpressionEvaluator.Evaluator.Expressions
{
    public class BinaryExpression : Expression
    {
        #region Members
        private Expression e1, e2;
        #endregion Members

        #region Constructor
        public BinaryExpression(Expression e1, Expression e2)
        {
            this.e1 = e1;
            this.e2 = e2;
            _evaluable = e1.Evaluable && e2.Evaluable;
        }
        #endregion Constructor

        #region Properties
        internal override int ArgumentsCount { get { return 2; } }
        internal override object Value
        {
            get
            {
                return Evaluate(new Expression[] { e1, e2 })[0].Value;
            }
        }
        #endregion Properties

        #region Try Simplify
        public static Expression trySimplify(BinaryExpression e, Expression es)
        {
            if (e.Evaluable)
            {
                return new ConstExpression(e.Value);
            }
            else
            {
                es.Push(e.e1);
                es.Push(e.e2);
                return e;
            }
        }
        #endregion Try Simplify

        #region Lambda Compilation
        internal override lambda.Expression Compile(lambda.Expression[] param, lambda.LabelTarget fault)
        {
            lambda.ParameterExpression param1 = lambda.Expression.Parameter(typeof(object));
            lambda.ParameterExpression param2 = lambda.Expression.Parameter(typeof(object));
            lambda.ParameterExpression evaluated = lambda.Expression.Variable(typeof(bool));
            lambda.ParameterExpression ret = lambda.Expression.Variable(typeof(object));
            List<lambda.Expression> expressions = new List<lambda.Expression>();

            expressions.Add(lambda.Expression.Assign(evaluated, lambda.Expression.Constant(false)));
            expressions.Add(lambda.Expression.Assign(param1, param[0]));
            expressions.Add(lambda.Expression.Assign(param2, param[1]));
            if ((_acceptedType & AcceptedType.Numeric) == AcceptedType.Numeric)
            {
                lambda.ParameterExpression paramNumeric1 = lambda.Expression.Parameter(typeof(double));
                lambda.ParameterExpression paramNumeric2 = lambda.Expression.Parameter(typeof(double));
                lambda.BlockExpression numericBlock = lambda.Expression.Block(
                    new[] { paramNumeric1, paramNumeric2 },
                    lambda.Expression.Assign(paramNumeric1, lambda.Expression.Convert(lambda.Expression.Call(_miChangeType, param1, lambda.Expression.Constant(typeof(double))), typeof(double))),
                    lambda.Expression.Assign(paramNumeric2, lambda.Expression.Convert(lambda.Expression.Call(_miChangeType, param2, lambda.Expression.Constant(typeof(double))), typeof(double))),
                    lambda.Expression.Assign(evaluated, lambda.Expression.Constant(true)),
                    lambda.Expression.Assign(ret, lambda.Expression.Convert(CompileNumericBlock(paramNumeric1, paramNumeric2), typeof(object)))
                );

                expressions.Add(lambda.Expression.TryCatch(numericBlock, lambda.Expression.Catch(typeof(Exception),
                    lambda.Expression.Assign(ret, lambda.Expression.Convert(lambda.Expression.Constant(null), typeof(object)))
                )));
            }
            if ((_acceptedType & AcceptedType.String) == AcceptedType.String)
            {
                lambda.ParameterExpression paramString1 = lambda.Expression.Parameter(typeof(string));
                lambda.ParameterExpression paramString2 = lambda.Expression.Parameter(typeof(string));
                lambda.BlockExpression stringBlock = lambda.Expression.Block(
                    new[] { paramString1, paramString2 },
                    lambda.Expression.Assign(paramString1, lambda.Expression.Convert(lambda.Expression.Call(_miChangeType, param1, lambda.Expression.Constant(typeof(string))), typeof(string))),
                    lambda.Expression.Assign(paramString2, lambda.Expression.Convert(lambda.Expression.Call(_miChangeType, param2, lambda.Expression.Constant(typeof(string))), typeof(string))),
                    lambda.Expression.Assign(evaluated, lambda.Expression.Constant(true)),
                    lambda.Expression.Assign(ret, lambda.Expression.Convert(CompileStringBlock(paramString1, paramString2), typeof(object)))
                );

                expressions.Add(lambda.Expression.IfThen(lambda.Expression.Equal(evaluated, lambda.Expression.Constant(false)), 
                    lambda.Expression.TryCatch(stringBlock, lambda.Expression.Catch(typeof(Exception),
                    lambda.Expression.Assign(ret, lambda.Expression.Convert(lambda.Expression.Constant(null), typeof(object)))
                ))));
            }
            expressions.Add(lambda.Expression.IfThenElse(evaluated, ret, lambda.Expression.Goto(fault)));
            expressions.Add(ret);
            lambda.BlockExpression block = lambda.Expression.Block(
                new[] { param1, param2, evaluated, ret },
                expressions.ToArray()                
            );
            return block;
        }

        internal virtual lambda.Expression CompileNumericBlock(lambda.ParameterExpression paramNumeric1, lambda.ParameterExpression paramNumeric2)
        {
            throw new EvaluateException("Syntax Error");
        }

        internal virtual lambda.Expression CompileStringBlock(lambda.ParameterExpression paramString1, lambda.ParameterExpression paramString2)
        {
            throw new EvaluateException("Syntax Error");
        }
        #endregion Lambda Compilation
    }
}
