using BlazorMonaco.Bridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeanBlazorDevEnvShare.Data
{
    public class StringSync
    {
        public event EventHandler<CodeChangeEventArgs> CodeChangeEvent;

        private string code;

        public string Code
        {
            get
            {
                return code;
            }

            /*set
            {
                code = value;
                RaiseUpdateCodeEvent(new CodeChangeEventArgs());
            }*/
        }

        public void SetCode(String _code, Position pos)
        {
            code = _code;
            RaiseUpdateCodeEvent(new CodeChangeEventArgs(pos));
        }

        protected virtual void RaiseUpdateCodeEvent(CodeChangeEventArgs e)
        {
            // Make a temporary copy of the event to avoid possibility of
            // a race condition if the last subscriber unsubscribes
            // immediately after the null check and before the event is raised.
            EventHandler<CodeChangeEventArgs> raiseEvent = CodeChangeEvent;

            // Event will be null if there are no subscribers
            if (raiseEvent != null)
            {
                // Call to raise the event.
                raiseEvent(this, e);
            }
        }
    }

    public class CodeChangeEventArgs : EventArgs
    {
        public CodeChangeEventArgs(Position pos)
        {
            Pos = pos;
        }

        public Position Pos { get; set; }
    }
}
