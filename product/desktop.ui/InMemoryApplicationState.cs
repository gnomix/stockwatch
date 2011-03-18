using System;
using System.Collections.Generic;
using gorilla.utility;

namespace solidware.financials.windows.ui
{
    public class InMemoryApplicationState : ApplicationState
    {
        IDictionary<Type, object> database = new Dictionary<Type, object>();

        public Token PullOut<Token>()
        {
            return database[typeof (Token)].downcast_to<Token>();
        }

        public void PushIn<Token>(Token token)
        {
            database[typeof (Token)] = token;
        }

        public bool HasBeenPushedIn<Token>()
        {
            return database.ContainsKey(typeof(Token));
        }
    }
}