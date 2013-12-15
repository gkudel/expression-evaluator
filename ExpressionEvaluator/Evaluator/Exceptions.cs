namespace ExpressionEvaluator.Evaluator.Expressions
{
    /// <summary>
    /// This expression signifies a malformed expression.
    /// </summary>
    public class ParseErrorException : System.Exception
    {
        #region Constructror
        /// <summary>
        /// Constructs new exception of this type with the associated description string.
        /// <param name="message">Description string</param>
        /// </summary>
        public ParseErrorException(string message) : base(message) { }
        #endregion
    }

    class RequestedVariableEvaluationException : System.Exception
    {
        #region Constructror
        public RequestedVariableEvaluationException(string message) : base(message) { }
        #endregion
    }

    class TokenTypeEvaluationException : System.Exception
    {
        #region Constructror
        public TokenTypeEvaluationException(string message) : base(message) { }
        #endregion
    }

    class NoFieldInfoException : System.Exception
    {
        #region Constructror
        public NoFieldInfoException(string message) : base(message) { }
        #endregion
    }

    /// <summary>
    /// This expression signifies that a bad number 
    /// of arguments has been passed to the evaluate method.
    /// </summary>
    public class BadNumberOfArgsException : System.Exception
    {
        #region Constructror
        /// <summary>
        /// Constructs new exception of this type with the associated description string.
        /// <param name="message">Description string</param>
        /// </summary>
        public BadNumberOfArgsException(string message) : base(message) { }
        #endregion
    }

    /// <summary>
    /// This expression signifies that a request has been made 
    /// to evaluate an expression that has not yet been compiled.
    /// </summary>    
    public class NotCompiledException : System.Exception
    {
        #region Constructror
        /// <summary>
        /// Constructs new exception of this type with the associated description string.
        /// <param name="message">Description string</param>
        /// </summary>
        public NotCompiledException(string message) : base(message) { }
        #endregion
    }


    /// <summary>
    /// This expression signifies that a bad number 
    /// of arguments has been passed to the evaluate method.
    /// </summary>
    public class EvaluateException : System.Exception
    {
        #region Constructror
        /// <summary>
        /// Constructs new exception of this type with the associated description string.
        /// <param name="message">Description string</param>
        /// </summary>
        public EvaluateException(string message) : base(message) { }
        #endregion
    }

}
