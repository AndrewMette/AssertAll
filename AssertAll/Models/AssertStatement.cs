using System;
using System.Threading.Tasks;

namespace AssertAllNuget.Models
{
    internal class AssertStatement
    {
        internal string TestName { get; set; }
        internal string File {get;set;}
        internal int LineNumber {get;set;}
        internal Action Action {get;set;}
        internal Func<Task> Func { get; set; }
    }
}
