using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lambda = System.Linq.Expressions;

namespace ExpressionEvaluator.Evaluator.Expressions
{
    public class UnaryExpression : Expression
    {
        #region Members
        private Expression e1;
        #endregion Members

        #region Constructor
        public UnaryExpression(Expression e1)
        {
            this.e1 = e1;
            _evaluable = e1.Evaluable;
        }
        #endregion Constructor

        #region Properties
        internal override int ArgumentsCount { get { return 1; } }
        internal override object Value
        {
            get
            {
                return Evaluate(new Expression[] { e1 })[0].Value;
            }
        }
        #endregion Properties

        #region Try Simplify
        public static Expression trySimplify(UnaryExpression e, Expression es)
        {
            if (e.Evaluable)
            {
                return new ConstExpression(e.Value);
            }
            else
            {
                es.Push(e.e1);
                return e;
            }
        }
        #endregion Try Simplify

        #region Lambda Compilation
        internal override lambda.Expression Compile(lambda.Expression[] param, lambda.LabelTarget fault)
        {
            lambda.ParameterExpression param1 = lambda.Expression.Parameter(typeof(object));
            lambda.ParameterExpression evaluated = lambda.Expression.Variable(typeof(bool));
            lambda.ParameterExpression ret = lambda.Expression.Variable(typeof(object));
            List<lambda.Expression> expressions = new List<lambda.Expression>();

            expressions.Add(lambda.Expression.Assign(evaluated, lambda.Expression.Constant(false)));
            expressions.Add(lambda.Expression.Assign(param1, param[0]));
            if ((_acceptedType & AcceptedType.Numeric) == AcceptedType.Numeric)
            {
                lambda.ParameterExpression paramNumeric1 = lambda.Expression.Parameter(typeof(double));
                lambda.BlockExpression numericBlock = lambda.Expression.Block(
                    new[] { paramNumeric1 },
                    lambda.Expression.Assign(paramNumeric1, lambda.Expression.Convert(lambda.Expression.Call(_miChangeType, param1, lambda.Expression.Constant(typeof(double))), typeof(double))),
                    lambda.Expression.Assign(evaluated, lambda.Expression.Constant(true)),
                    lambda.Expression.Assign(ret, lambda.Expression.Convert(CompileNumericBlock(paramNumeric1, fault), typeof(object)))
                );
                expressions.Add(lambda.Expression.TryCatch(numericBlock, lambda.Expression.Catch(typeof(Exception),
                    lambda.Expression.Assign(ret, lambda.Expression.Convert(lambda.Expression.Constant(null), typeof(object)))
                )));
            }
            if ((_acceptedType & AcceptedType.String) == AcceptedType.String)
            {
                lambda.ParameterExpression paramString1 = lambda.Expression.Parameter(typeof(string));
                lambda.BlockExpression stringBlock = lambda.Expression.Block(
                    new[] { paramString1 },
                    lambda.Expression.Assign(paramString1, lambda.Expression.Convert(lambda.Expression.Call(_miChangeType, param1, lambda.Expression.Constant(typeof(string))), typeof(string))),
                    lambda.Expression.Assign(evaluated, lambda.Expression.Constant(true)),
                    lambda.Expression.Assign(ret, lambda.Expression.Convert(CompileStringBlock(paramString1, fault), typeof(object)))
                );

                expressions.Add(lambda.Expression.IfThen(lambda.Expression.Equal(evaluated, lambda.Expression.Constant(false)),
                    lambda.Expression.TryCatch(stringBlock, lambda.Expression.Catch(typeof(Exception),
                    lambda.Expression.Assign(ret, lambda.Expression.Convert(lambda.Expression.Constant(null), typeof(object)))
                ))));
            }
            if ((_acceptedType & AcceptedType.Bool) == AcceptedType.Bool)
            {
                lambda.ParameterExpression paramBool1 = lambda.Expression.Parameter(typeof(bool));
                lambda.BlockExpression boolBlock = lambda.Expression.Block(
                    new[] { paramBool1 },
                    lambda.Expression.Assign(paramBool1, lambda.Expression.Convert(lambda.Expression.Call(_miChangeType, param1, lambda.Expression.Constant(typeof(bool))), typeof(bool))),
                    lambda.Expression.Assign(evaluated, lambda.Expression.Constant(true)),
                    lambda.Expression.Assign(ret, lambda.Expression.Convert(CompileBoolBlock(paramBool1, fault), typeof(object)))
                );

                expressions.Add(lambda.Expression.IfThen(lambda.Expression.Equal(evaluated, lambda.Expression.Constant(false)),
                   lambda.Expression.TryCatch(boolBlock, lambda.Expression.Catch(typeof(Exception),
                    lambda.Expression.Assign(ret, lambda.Expression.Convert(lambda.Expression.Constant(null), typeof(object)))
                ))));
            }
            if ((_acceptedType & AcceptedType.Array) == AcceptedType.Array)
            {
                lambda.ParameterExpression paramArray1 = lambda.Expression.Parameter(typeof(object[]));
                lambda.BlockExpression arrayBlock = lambda.Expression.Block(
                    new[] { paramArray1 },
                    lambda.Expression.Assign(paramArray1, lambda.Expression.Convert(lambda.Expression.Call(_miChangeType, param1, lambda.Expression.Constant(typeof(object[]))), typeof(object[]))),
                    lambda.Expression.Assign(evaluated, lambda.Expression.Constant(true)),
                    lambda.Expression.Assign(ret, lambda.Expression.Convert(CompileArrayBlock(paramArray1, fault), typeof(object)))
                );

                expressions.Add(lambda.Expression.IfThen(lambda.Expression.Equal(evaluated, lambda.Expression.Constant(false)),
                    lambda.Expression.TryCatch(arrayBlock, lambda.Expression.Catch(typeof(Exception),
                    lambda.Expression.Assign(ret, lambda.Expression.Convert(lambda.Expression.Constant(null), typeof(object)))
                ))));
            }
            expressions.Add(lambda.Expression.IfThenElse(evaluated, ret, lambda.Expression.Goto(fault)));
            expressions.Add(ret);
            lambda.BlockExpression block = lambda.Expression.Block(
                new[] { param1, evaluated, ret },
                expressions.ToArray()
            );
            return block;
        }

        internal virtual lambda.Expression CompileNumericBlock(lambda.ParameterExpression paramNumeric1, lambda.LabelTarget fault)
        {
            throw new EvaluateException("Syntax Error");
        }

        internal virtual lambda.Expression CompileStringBlock(lambda.ParameterExpression paramString1, lambda.LabelTarget fault)
        {
            throw new EvaluateException("Syntax Error");
        }

        internal virtual lambda.Expression CompileBoolBlock(lambda.ParameterExpression paramBool1, lambda.LabelTarget fault)
        {
            throw new EvaluateException("Syntax Error");
        }

        internal virtual lambda.Expression CompileArrayBlock(lambda.ParameterExpression paramArray1, lambda.LabelTarget fault)
        {
            throw new EvaluateException("Syntax Error");
        }
        #endregion Lambda Compilation
    }
}