using ExpressionEvaluator.Evaluator.Expressions.Block;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lambda = System.Linq.Expressions;

namespace ExpressionEvaluator.Evaluator.Expressions.ForEach
{
    public class ForEachExpression : BlockUnaryExpression
    {
        #region Constructor
        public ForEachExpression(Expression e1)
            : base(e1)
        {
            LocalVariable = true;
            _acceptedType = AcceptedType.Array;
        }
        #endregion Constructor

        #region Properties
        public override string Name { get { return "ForEachExpression"; } }
        #endregion Properties

        #region Evaluate
        internal override Expression[] Evaluate(Expression[] values, out bool evaluated)
        {
            evaluated = false;
            object[] ret = null;
            if (values[0].ArrayValue != null)
            {
                evaluated = true;
                ret = values[0].ArrayValue[values[0].ArrayValue.Count() - 1] as object[];
                Parallel.For(0, values[0].ArrayValue.Count() - 1, (i, loopState) =>
                {
                    object obj = values[0].ArrayValue[i];
                    bool localevaluated = true;
                    object[] row = obj as object[];
                    if (row != null)
                    {
                        object o = base.Evaluate(row, out localevaluated);
                        if (localevaluated)
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
                        localevaluated = false;
                        loopState.Break();
                   }
                });
                evaluated = ret.Count(o => o == null) == 0;
            }
            return evaluated ? new Expression[] { new ConstExpression(ret) } : null;
        }
        #endregion Evaluate

        #region Lambda Compilation
        internal override lambda.Expression CompileArrayBlock(lambda.ParameterExpression paramArray1, lambda.LabelTarget fault)
        {
            lambda.ParameterExpression counter = lambda.Expression.Variable(typeof(int), "counter");
            lambda.ParameterExpression exc = lambda.Expression.Variable(typeof(bool), "exc");
            lambda.ParameterExpression row = lambda.Expression.Variable(typeof(object[]), "row");
            lambda.ParameterExpression ret = lambda.Expression.Variable(typeof(object[]), "ret");
            lambda.LabelTarget label = lambda.Expression.Label(typeof(object));
            lambda.LabelTarget loclalfault = lambda.Expression.Label();
            lambda.LabelTarget loclalsucces = lambda.Expression.Label();

            lambda.BlockExpression foorEachBlock = lambda.Expression.Block(
                new[] { counter, ret, exc },
                lambda.Expression.Assign(exc, lambda.Expression.Constant(false)),
                lambda.Expression.Assign(counter, lambda.Expression.Constant(0)),
                lambda.Expression.Assign(ret, lambda.Expression.Convert(lambda.Expression.ArrayIndex(paramArray1, lambda.Expression.Subtract(lambda.Expression.ArrayLength(paramArray1), lambda.Expression.Constant(1))), typeof(object[]))), // lambda.Expression.NewArrayBounds(typeof(object), lambda.Expression.ArrayLength(paramArray1))),
                lambda.Expression.Loop(lambda.Expression.IfThenElse(
                lambda.Expression.LessThan(counter, lambda.Expression.Subtract(lambda.Expression.ArrayLength(paramArray1), lambda.Expression.Constant(1))),
                lambda.Expression.Block(
                    new[] { row },
                    lambda.Expression.TryCatch(
                        lambda.Expression.Block(
                        lambda.Expression.Assign(row, lambda.Expression.Convert(lambda.Expression.ArrayIndex(paramArray1, counter), typeof(object[]))),
                        lambda.Expression.Assign(lambda.Expression.ArrayAccess(ret, counter), lambda.Expression.Convert(base.Compile(row, loclalfault), typeof(object))),
                        lambda.Expression.Goto(loclalsucces),
                        lambda.Expression.Label(loclalfault),
                        lambda.Expression.Assign(lambda.Expression.ArrayAccess(ret, counter), lambda.Expression.Constant(null)),
                        lambda.Expression.Label(loclalsucces),
                        ret)                                                
                        ,lambda.Expression.Catch(typeof(Exception), lambda.Expression.Block(
                            lambda.Expression.Assign(counter, lambda.Expression.Subtract(lambda.Expression.ArrayLength(paramArray1), lambda.Expression.Constant(1))),
                            lambda.Expression.Assign(exc, lambda.Expression.Constant(true)),
                            ret)))
                            ,lambda.Expression.PostIncrementAssign(counter)
                ),
                lambda.Expression.IfThenElse(exc, lambda.Expression.Goto(fault),
                lambda.Expression.Break(label, lambda.Expression.Convert(ret, typeof(object))))
                ),label)
            );
            return foorEachBlock;
        }
        #endregion Lambda Compilation
    }
}
