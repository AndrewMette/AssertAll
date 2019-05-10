using System;

namespace AssertAll.Models
{
    internal class AssertStatement
    {
        internal string TestName { get; set; }
        internal string File {get;set;}
        internal int LineNumber {get;set;}
        internal Action Action {get;set;}
    }
}
