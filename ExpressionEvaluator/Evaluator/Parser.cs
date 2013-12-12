using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection;
using System;
using GcmCmnTools;
using System.Linq;
using ExpressionEvaluator.Evaluator.Expressions.Arithmetic;

namespace ExpressionEvaluator.Evaluator.Expressions
{
    public partial class Parser 
    {
        #region Members
        private LinkedList<Token>[,] M =
                new LinkedList<Token>[
                    System.Enum.GetValues(typeof(TokenKind)).Length,
                    System.Enum.GetValues(typeof(NonterminalSymbolKind)).Length];
        private Dictionary<TokenGroup, TokenGroup[]> previoustoken = new Dictionary<TokenGroup, TokenGroup[]>();
        private Expression es = new Expression()
        {
            InnerStack = true
        };

        private bool _calculationSucced = false;
        private string _expression = string.Empty;
        private static List<Parser> _expressions;
        #endregion Members

        #region Constructor
        static Parser()
        {
            _expressions = new List<Parser>();
        }

        private Parser(String expression)
        {
            DoParse(new Scanner().Scan(expression));
            _expression = expression;
        }
        #endregion

        #region GetInstance
        public static Parser GetInstance(string expression)
        {
            Parser p = _expressions.FirstOrDefault(e => e.Expression == expression);
            if(p == null)
            {
                p = new Parser(expression);
                _expressions.Add(p);
            }
            return p;
        }
        #endregion GetInstance

        #region Methods
        public string[] GetVariables()
        {
            return es.ExpressionStack.GetVariables();
        }
               
        private IEnumerator<string> Scan(IEnumerator<string> en)
        {
            while (true)
            {
                if (en.MoveNext())
                {
                    yield return en.Current.ToString();
                }
                else
                {
                    yield return "";
                }
            }
        }

        private void DoParse(IEnumerator<Token> input)
        {
            InitializeTab();

            Stack<Token> ts = new Stack<Token>();
            List<Token> tokens = new List<Token>();
            ts.Push(new BareToken(TokenKind.End));
            ts.Push(new NonterminalSymbol(NonterminalSymbolKind.C));
            input.MoveNext();
            Token c = input.Current;
            tokens.Add(c);

            Token X;
            while (true)
            {                
                X = ts.Pop();
                if (X.Kind == TokenKind.End)
                {
                    break;
                }
                else if (X.Kind == TokenKind.Translation)
                {
                    TranslationSymbol ns = (TranslationSymbol)X;
                    Expression e3 = null;
                    Expression e2 = null;
                    Expression e1 = null;
                    if (ns.KindTR != TranslationSymbolKind.ElseOperator)
                    {
                        e2 = es.ExpressionStack.Pop();
                    }

                    switch (ns.KindTR)
                    {
                        case TranslationSymbolKind.PlusOperator:
                        case TranslationSymbolKind.MinusOperator:
                        case TranslationSymbolKind.MultiplyOperator:
                        case TranslationSymbolKind.DivideOpearator:
                        case TranslationSymbolKind.PowerOperator:
                        case TranslationSymbolKind.GreaterOperator:
                        case TranslationSymbolKind.GreaterOrEqualOperator:
                        case TranslationSymbolKind.LessOperator:
                        case TranslationSymbolKind.LessOrEqualOperator:
                        case TranslationSymbolKind.EqualOperator:
                        case TranslationSymbolKind.NotEqualOperator:
                        case TranslationSymbolKind.AndOperator:
                        case TranslationSymbolKind.OrOperator:
                        case TranslationSymbolKind.MaxOperator:
                        case TranslationSymbolKind.MinOperator:
                        case TranslationSymbolKind.LogOperator:
                        case TranslationSymbolKind.RoundOperator:
                        case TranslationSymbolKind.BusulfanCalculationOperator:
                            e1 = es.ExpressionStack.Pop();
                            break;
                        case TranslationSymbolKind.AvgOperator:
                        case TranslationSymbolKind.StdOperator:
                        case TranslationSymbolKind.CVOperator:
                        case TranslationSymbolKind.AgeOfThePatientOperator:
                            e3 = e2;
                            e2 = es.ExpressionStack.Pop();
                            e1 = es.ExpressionStack.Pop();
                            break;
                    }

                    switch (ns.KindTR)
                    {
                        case TranslationSymbolKind.PlusOperator:
                           es.Push(BinaryExpression.trySimplify(
                                new PlusExpression(e1, e2), es.ExpressionStack));
                            break;

                        case TranslationSymbolKind.MinusOperator:
                           es.Push(BinaryExpression.trySimplify(
                                new MinusExpression(e1, e2), es.ExpressionStack));
                            break;

                        case TranslationSymbolKind.MultiplyOperator:
                           es.Push(BinaryExpression.trySimplify(
                                new MultiplyExpression(e1, e2), es.ExpressionStack));
                            break;

                        case TranslationSymbolKind.DivideOpearator:
                           es.Push(BinaryExpression.trySimplify(
                                new DivideExpression(e1, e2), es.ExpressionStack));
                            break;

                        case TranslationSymbolKind.PowerOperator:
                           es.Push(BinaryExpression.trySimplify(
                                new PowerExpression(e1, e2), es.ExpressionStack));
                            break;

                        case TranslationSymbolKind.SqrtOperator:
                           es.Push(UnaryExpression.trySimplify(
                                new SqrtExpression(e2), es.ExpressionStack));
                            break;

                        /*case TranslationSymbolKind.AvgOperator:
                           es.Push(ThreeArgumentsExpression.trySimplify(
                                new AvgExpression(e1, e2, e3), es.Stack));
                            break;
                         case TranslationSymbolKind.StdOperator:
                           es.Push(ThreeArgumentsExpression.trySimplify(
                                new StdExpression(e1, e2, e3), es.Stack));
                            break;

                        case TranslationSymbolKind.CVOperator:
                           es.Push(ThreeArgumentsExpression.trySimplify(
                                new CVExpression(e1, e2, e3), es.Stack));
                            break;
                        case TranslationSymbolKind.AgeOfThePatientOperator:
                           es.Push(ThreeArgumentsExpression.trySimplify(
                                new AgeOfThePatientExpression(e1, e2, e3), es.Stack));
                            break;*/
                        case TranslationSymbolKind.Log10Operator:
                           es.Push(UnaryExpression.trySimplify(
                                new Log10Expression(e2), es.ExpressionStack));
                            break;
                        case TranslationSymbolKind.SinOperator:
                           es.Push(UnaryExpression.trySimplify(
                                new SinExpression(e2), es.ExpressionStack));
                            break;
                        /*case TranslationSymbolKind.IsNullOperator:
                           es.Push(UnaryExpression.trySimplify(
                                new IsNullExpression(e2, (ILogic)operators[OperatorKind.Logic]), es.Stack));
                            break;
                        case TranslationSymbolKind.IsNotNullOperator:
                           es.Push(UnaryExpression.trySimplify(
                                new IsNotNullExpression(e2, (ILogic)operators[OperatorKind.Logic]), es.Stack));
                            break;*/
                        case TranslationSymbolKind.CosOperator:
                           es.Push(UnaryExpression.trySimplify(
                                new CosExpression(e2), es.ExpressionStack));
                            break;
                        case TranslationSymbolKind.TanOperator:
                           es.Push(UnaryExpression.trySimplify(
                                new TanExpression(e2), es.ExpressionStack));
                            break;
                        /*case TranslationSymbolKind.AbsOperator:
                           es.Push(UnaryExpression.trySimplify(
                                new AbsExpression(e2), es.Stack));
                            break;

                        case TranslationSymbolKind.AcosOperator:
                           es.Push(UnaryExpression.trySimplify(
                                new AcosExpression(e2), es.Stack));
                            break;

                        case TranslationSymbolKind.AsinOperator:
                           es.Push(UnaryExpression.trySimplify(
                                new AsinExpression(e2), es.Stack));
                            break;

                        case TranslationSymbolKind.AtanOperator:
                           es.Push(UnaryExpression.trySimplify(
                                new AtanExpression(e2), es.Stack));
                            break;

                        case TranslationSymbolKind.CeilingOperator:
                           es.Push(UnaryExpression.trySimplify(
                                new CeilingExpression(e2), es.Stack));
                            break;

                        case TranslationSymbolKind.CoshOperator:
                           es.Push(UnaryExpression.trySimplify(
                                new CoshExpression(e2), es.Stack));
                            break;

                        case TranslationSymbolKind.ExpOperator:
                           es.Push(UnaryExpression.trySimplify(
                                new ExpExpression(e2), es.Stack));
                            break;

                        case TranslationSymbolKind.FloorOperator:
                           es.Push(UnaryExpression.trySimplify(
                                new FloorExpression(e2), es.Stack));
                            break;

                        case TranslationSymbolKind.MaxOperator:
                           es.Push(BinaryExpression.trySimplify(
                                new MaxExpression(e1, e2), es.Stack));
                            break;

                        case TranslationSymbolKind.MinOperator:
                           es.Push(BinaryExpression.trySimplify(
                                new MinExpression(e1, e2), es.Stack));
                            break;

                        case TranslationSymbolKind.RoundOperator:
                           es.Push(BinaryExpression.trySimplify(
                                new RoundExpression(e1, e2), es.Stack));
                            break;

                        case TranslationSymbolKind.CountItemsOperator:
                            es.Push(UnaryExpression.trySimplify(
                                 new CountItemsExpression(e2), es.Stack));
                            break;

                        case TranslationSymbolKind.CheckOperator:
                            es.Push(UnaryExpression.trySimplify(
                                 new CheckExpression(e2), es.Stack));
                            break;

                        case TranslationSymbolKind.BusulfanCalculationOperator:
                           es.Push(BinaryExpression.trySimplify(
                                new BusulfanCalculationExpression(e1, e2, (IFunction)operators[OperatorKind.Function]), es.Stack));
                            break;

                        case TranslationSymbolKind.ConvertToDaysOperator:
                           es.Push(UnaryExpression.trySimplify(
                                new ConvertToDaysExpression(e2), es.Stack));
                            break;
                        case TranslationSymbolKind.SumColOperator:
                            es.Push(UnaryExpression.trySimplify(
                                new SumColExpression(e2), es.Stack));
                            break;
                        case TranslationSymbolKind.SumRowOperator:
                            es.Push(UnaryExpression.trySimplify(
                                new SumRowExpression(e2), es.Stack));
                            break;
                        case TranslationSymbolKind.AvgWksOperator:
                            es.Push(UnaryExpression.trySimplify(
                                new AvgWksExpression(e2), es.Stack));
                            break;
                        case TranslationSymbolKind.SumWksOperator:
                            es.Push(UnaryExpression.trySimplify(
                                new SumWksExpression(e2), es.Stack));
                            break;
                        case TranslationSymbolKind.CountSigOperator:
                            es.Push(UnaryExpression.trySimplify(
                                new CountSigExpression(e2), es.Stack));
                            break;
                        case TranslationSymbolKind.SumOperator:
                           es.Push(UnaryExpression.trySimplify(
                                new SumExpression(e2), es.Stack));
                            break;

                        case TranslationSymbolKind.LogOperator:
                           es.Push(BinaryExpression.trySimplify(
                                new LogExpression(e1, e2), es.Stack));
                            break;

                        case TranslationSymbolKind.SinhOperator:
                           es.Push(UnaryExpression.trySimplify(
                                new SinhExpression(e2), es.Stack));
                            break;

                        case TranslationSymbolKind.TanhOperator:
                           es.Push(UnaryExpression.trySimplify(
                                new TanhExpression(e2), es.Stack));
                            break;

                        case TranslationSymbolKind.TruncateOperator:
                           es.Push(UnaryExpression.trySimplify(
                                new TruncateExpression(e2), es.Stack));
                            break;

                        case TranslationSymbolKind.UnaryMinusOperator:
                           es.Push(UnaryExpression.trySimplify(
                                new UnaryMinusExpression(e2), es.Stack));
                            break;

                        case TranslationSymbolKind.IfOperator:
                           es.Push(UnaryExpression.trySimplify(
                                new IfExpression(e2, (IIf)operators[OperatorKind.If] ), es.Stack));
                            break;

                        case TranslationSymbolKind.ForEachOperator:                        
                           es.Push(UnaryExpression.trySimplify(
                                new ForEachExpression(noForEach++, e2, (IForEach)operators[OperatorKind.ForEach]), es.Stack));
                            break;

                        case TranslationSymbolKind.ElseOperator:
                           es.Push(new ElseExpression());
                            break;

                        case TranslationSymbolKind.GreaterOperator:
                           es.Push(BinaryExpression.trySimplify(
                                new GreterExpression(e1, e2, (ICompare)operators[OperatorKind.Compare]), es.Stack));
                            break;

                        case TranslationSymbolKind.GreaterOrEqualOperator:
                           es.Push(BinaryExpression.trySimplify(
                                new GreaterOrEqualExpression(e1, e2, (ICompare)operators[OperatorKind.Compare]), es.Stack));
                            break;

                        case TranslationSymbolKind.LessOperator:
                           es.Push(BinaryExpression.trySimplify(
                                new LessExpression(e1, e2, (ICompare)operators[OperatorKind.Compare]), es.Stack));
                            break;

                        case TranslationSymbolKind.LessOrEqualOperator:
                           es.Push(BinaryExpression.trySimplify(
                                new LessOrEqualExpression(e1, e2, (ICompare)operators[OperatorKind.Compare]), es.Stack));
                            break;

                        case TranslationSymbolKind.EqualOperator:
                           es.Push(BinaryExpression.trySimplify(
                                new EqualExpression(e1, e2, (ICompare)operators[OperatorKind.Compare]), es.Stack));
                            break;

                        case TranslationSymbolKind.NotEqualOperator:
                           es.Push(BinaryExpression.trySimplify(
                                new NotEqualExpression(e1, e2, (ICompare)operators[OperatorKind.Compare]), es.Stack));
                            break;

                        case TranslationSymbolKind.NegativeOperator:
                           es.Push(UnaryExpression.trySimplify(
                                new NegativExpression(e2, (ILogic)operators[OperatorKind.Logic]), es.Stack));
                            break;

                        case TranslationSymbolKind.AndOperator:
                           es.Push(BinaryExpression.trySimplify(
                                new AndExpression(e1, e2, (ILogic)operators[OperatorKind.Logic]), es.Stack));
                            break;

                        case TranslationSymbolKind.OrOperator:
                           es.Push(BinaryExpression.trySimplify(
                                new OrExpression(e1, e2, (ILogic)operators[OperatorKind.Logic]), es.Stack));
                            break;*/
                    }
                }
                else if (X.Kind == TokenKind.Nonterminal)
                {
                    LinkedList<Token> lst;
                    if ((lst = M[(int)c.Kind, (int)((NonterminalSymbol)X).KindNT])
                        != null)
                    {
                        LinkedListNode<Token> nd = lst.Last;
                        while (nd != null)
                        {
                            ts.Push(nd.Value);
                            nd = nd.Previous;
                        }
                    }
                    else
                    {
                        throw new ParseErrorException
                            ("Unexpected token: " + c.Kind + ".");
                    }
                }
                else 
                {                    
                    if (X.Kind == c.Kind)
                    {
                        switch (c.Kind)
                        {
                            case TokenKind.Double:
                                {
                                    ConstExpression constexpr = new ConstExpression(((DoubleToken)c).Value);
                                    es.Push(constexpr);
                                }
                                break;
                            case TokenKind.String:
                                {
                                    ConstExpression constexpr = new ConstExpression(((StringToken)c).Value);
                                    es.Push(constexpr);
                                }
                                break;
                            case TokenKind.DateTime:
                                {
                                    ConstExpression constexpr = new ConstExpression(((DateTimeToken)c).Value);
                                    es.Push(constexpr);
                                }
                                break;
                            case TokenKind.Variable:
                                VariableExpression variableexpr = new VariableExpression(((VariableToken)c).Name);
                                es.Push(variableexpr);
                                break;
                            case TokenKind.E:
                               es.Push(new ConstExpression(Math.E));
                                break;

                            case TokenKind.PI:
                               es.Push(new ConstExpression(Math.PI));
                                break;
                            /*case TokenKind.RightBrace:                        
                               es.Push(new RightBraceExpression());
                                break;
                            case TokenKind.LeftBrace:
                               es.Push(new LeftBraceExpression());
                                break;*/
                        }
                        input.MoveNext();
                        Token prev = c;
                        c = input.Current;
                        tokens.Add(c);
                        if (previoustoken.ContainsKey(c.Group))
                        {
                            bool find = false;
                            foreach (TokenGroup group in previoustoken[c.Group])
                            {
                                if (prev.Group == group)
                                {
                                    find = true;
                                    break;
                                }
                            }
                            if (!find)
                            {
                                throw new ParseErrorException
                                     ("UnExpected " + prev.Group + " - " + c.Group + ".");
                            }
                        }
                    }
                    else
                    {
                        throw new ParseErrorException
                            ("Expected " + X.Kind + ", got " + c.Kind + ".");
                    }
                }
            }
        }

        public object Evaluate(object[] values)        
        {
            values.AsEnumerable().SelectMany((o, index) => es.ExpressionStack.Where(e => e is VariableExpression && (e as VariableExpression).Ordinal == index).
                Select(e => e as VariableExpression), (o, e) => new { Value = o, Expression = e }).ToList().ForEach((ve) =>
            {
                ve.Expression.SetValue(ve.Value);
            });

            return es.Value;
        }
        #endregion Methods

        #region Properties
        public bool CalculationSucced
        {
            get { return _calculationSucced; }
        }

        public string Expression
        {
            get { return _expression; }
        }
        #endregion Properties
    }
}
