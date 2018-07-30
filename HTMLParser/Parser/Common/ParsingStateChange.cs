using System;
using System.Collections.Generic;
using System.Text;

namespace HTMLParser.Parser.Common
{
    /// <summary>
    /// Represents move between two states.
    /// </summary>
    internal class ParsingStateChange
    {
        private readonly ParsingStates StartState;
        private readonly ParsingStates EndState;

        public ParsingStateChange(ParsingStates startState, ParsingStates endState)
        {
            StartState = startState;
            EndState = endState;
        }

        public override int GetHashCode()
        {
            return 17 + 31 * StartState.GetHashCode() + 31 * EndState.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            ParsingStateChange other = obj as ParsingStateChange;
            return other != null && this.StartState == other.StartState && this.EndState == other.EndState;
        }
    }
}
