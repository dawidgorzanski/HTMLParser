using System;
using System.Collections.Generic;
using System.Text;

namespace HTMLParser.Parser.Common
{
    /// <summary>
    /// Handles transitions between states.
    /// </summary>
    internal class TransitionManager
    {
        public ParsingStates CurrentState { get; private set; }

        public TransitionManager()
        {
            CurrentState = ParsingStates.Value;
        }

        public TransitionManager(ParsingStates parsingState)
        {
            CurrentState = parsingState;
        }

        public ParsingStates Move(CharacterTypes characterType, out AdditionalOperations[] additionalOperations)
        {
            Transition transition = new Transition(CurrentState, characterType);
            ParsingStates nextState = ParsingRules.StatesTable[transition];
            ParsingStateChange parsingStateChange = new ParsingStateChange(CurrentState, nextState);

            additionalOperations = null;
            if (ParsingRules.AdditionalParsingOperations.ContainsKey(parsingStateChange))
                additionalOperations = ParsingRules.AdditionalParsingOperations[parsingStateChange];

            CurrentState = nextState;
            return CurrentState;
        }
    }
}
