using System;
using System.Collections.Generic;
using System.Text;

namespace HTMLParser.Parser.Common
{
    /// <summary>
    /// Contains current state and next character. Used as a key for states machine dictionary.
    /// </summary>
    internal class Transition
    {
        private readonly ParsingStates CurrentState;
        private readonly CharacterTypes CharacterType;

        public Transition(ParsingStates currentState, CharacterTypes characterType)
        {
            CurrentState = currentState;
            CharacterType = characterType;
        }

        public override int GetHashCode()
        {
            return 17 + 31 * CurrentState.GetHashCode() + 31 * CharacterType.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Transition other = obj as Transition;
            return other != null && this.CurrentState == other.CurrentState && this.CharacterType == other.CharacterType;
        }
    }
}
