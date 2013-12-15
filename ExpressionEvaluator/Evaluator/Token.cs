using System;
using GcmCmnTools;

namespace ExpressionEvaluator.Evaluator.Expressions
{
    internal enum TokenKind
    {
        /* terminal symbols*/
        Double,
        String,
        DateTime,
        Plus,
        Minus,    
        Multiply,
        Divide,
        Power,
        Variable,
        LeftBracket,
        RightBracket,
        LeftBrace,
        RightBrace,
        Comma,
        If,
        Else,
        Greater,
        GreaterOrEqual,
        Less,
        LessOrEqual,
        Equal,
        NotEqual,
        Negative,
        And,
        Or,
        IsNull,
        IsNotNull,
        E,
        PI,

        /*terminal symbols: functions*/
        Sqrt,
        Avg,
        Std,
        CV,
        Log10,
        Log,
        Cos,
        Sin,
        Tan,
        Abs,
        Acos,
        Asin,
        Atan,
        Ceiling,
        Cosh,
        Exp,
        Floor,
        Max,
        Min,
        Sinh,
        Tanh,
        Truncate,
        Round,
        CountItems,
        Check,
        AgeOfThePatient,
        Sum,
        BusulfanCalculation,
        ConvertToDays,
        ForEach,
        Sum_Col,
        Sum_Row,
        AvgWks,
        SumWks,
        Count_Sig,

        /*translation symbol*/
        Translation,

        /*nonterminal symbols*/
        Nonterminal,

        /*end of stream*/
        End
    }

    internal enum TokenGroup
    {
        Unknow,
        Variable,
        Const,
        Minus,
        BianaryOperator,
        LeftUnaryOperator,
        Function,
        LeftBracket,
        RightBracket,
        LeftBrace,
        RightBrace,
        Comma,
        If,
        Else, 
        NullOperator,
        ForEach
    }

    internal enum NonterminalSymbolKind
    {
        C,
        Co,
        D,
        Do,
        E,
        Eo,
        T,
        To,
        F,
        Fo,
        G
    }

    internal enum TranslationSymbolKind
    {
        PlusOperator,
        MinusOperator,
        UnaryMinusOperator,
        MultiplyOperator,
        DivideOpearator,
        PowerOperator,
        IfOperator,
        ElseOperator,
        GreaterOperator,
        GreaterOrEqualOperator,
        LessOperator,
        LessOrEqualOperator,
        EqualOperator,
        NotEqualOperator,
        NegativeOperator,
        AndOperator,
        OrOperator,


        IsNullOperator,
        IsNotNullOperator,
        SqrtOperator,
        AvgOperator,
        StdOperator,
        CVOperator,
        Log10Operator,
        LogOperator,
        CosOperator,
        SinOperator,
        TanOperator,
        AbsOperator,
        AcosOperator,
        AsinOperator,
        AtanOperator,
        CeilingOperator,
        CoshOperator,
        ExpOperator,
        FloorOperator,
        MaxOperator,
        MinOperator,
        SinhOperator,
        TanhOperator,
        TruncateOperator,
        RoundOperator,
        CountItemsOperator,
        CheckOperator, 
        AgeOfThePatientOperator,
        SumOperator,
        BusulfanCalculationOperator,
        ConvertToDaysOperator,
        ForEachOperator,
        SumRowOperator,
        SumColOperator,
        AvgWksOperator,
        SumWksOperator,
        CountSigOperator
    }

    abstract internal class Token
    {
        public abstract TokenKind Kind { get; }
        public abstract TokenGroup Group { get; }
        public override string ToString() { return Kind.ToString(); }
    }

    internal class BareToken : Token
    {
        public BareToken(TokenKind tk) { this.tk = tk; } 
        public BareToken(TokenKind tk, TokenGroup tp) { this.tk = tk; this.tp = tp; } 
        public override TokenKind Kind { get { return tk; } }
        public override TokenGroup Group { get { return tp; } }
        private TokenKind tk;
        private TokenGroup tp;
        public override string ToString() { return tk.ToString(); }
    }

    internal class DoubleToken : Token
    {
        public DoubleToken(double value) { this.Value = value; } 
        public override TokenKind Kind { get { return TokenKind.Double; } }
        public override TokenGroup Group { get { return TokenGroup.Const; } }
        public readonly double Value;
        public override string ToString()
        {
            return Value.ToString();
        }
    }

    internal class StringToken : Token
    {
        public StringToken(string value) { this.Value = value; }
        public override TokenKind Kind { get { return TokenKind.String; } }
        public override TokenGroup Group { get { return TokenGroup.Const; } }
        public readonly string Value;
        public override string ToString()
        {
            return Value;
        }
    }

    internal class DateTimeToken : Token
    {
        public DateTimeToken(DateTime value) { this.Value = value; }
        public override TokenKind Kind { get { return TokenKind.DateTime; } }
        public override TokenGroup Group { get { return TokenGroup.Const; } }
        public readonly DateTime Value;
        public override string ToString()
        {
            return CmnTools.ConvertDateTimeToString(Value);
        }
    }

    internal class VariableToken : Token
    {
        public VariableToken(string name)
        {
            this.Name = name;
        } 
        public override TokenKind Kind { get { return TokenKind.Variable; } }
        public override TokenGroup Group { get { return TokenGroup.Variable; } }
        public readonly string Name;
        public override string ToString()
        {
            return Name;
        }
    }

    internal class NonterminalSymbol : Token
    {
        public NonterminalSymbol(NonterminalSymbolKind ntk) { this.ntk = ntk; }
        public override TokenKind Kind { get { return TokenKind.Nonterminal; } }
        public override TokenGroup Group { get { throw new TokenTypeEvaluationException("NonterminalSymbol doasn't have any type"); } }
        public NonterminalSymbolKind KindNT { get { return ntk; } }
        private NonterminalSymbolKind ntk;
        public override string ToString()
        {
            return base.ToString() + "-" + ntk.ToString();
        }
    }

    internal class TranslationSymbol : Token
    {
        public TranslationSymbol(TranslationSymbolKind tsk) { this.tsk = tsk; }
        public override TokenKind Kind { get { return TokenKind.Translation; } }
        public override TokenGroup Group { get { throw new TokenTypeEvaluationException("NonterminalSymbol doasn't have any type"); } }
        public TranslationSymbolKind KindTR { get { return tsk; } }
        private TranslationSymbolKind tsk;
        public override string ToString()
        {
            return base.ToString() + "-" + tsk.ToString();
        }
    }
}
