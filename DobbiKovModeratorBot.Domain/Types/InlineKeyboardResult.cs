using System;
using System.Collections.Generic;
using System.Text;

namespace DobbiKovModeratorBot.Domain.Types
{
    public class InlineKeyboardResult<T>
    {
        public bool isSucces { get; private set; }
        public T result { get; private set; }

        public InlineKeyboardResult(bool isSucces, T result)
        {
            this.isSucces = isSucces;
            this.result = result;
        }
    }
}
