using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll
{
    public partial class AssertAll
    {
        public partial class Strings
        {
            public static void DoesNotMatch(string value, Regex pattern, string message = null)
            {
                if (message == null)
                {
                    RegisterAction(() => StringAssert.DoesNotMatch(value, pattern));
                }
                else
                {
                    RegisterAction(() => StringAssert.DoesNotMatch(value, pattern, message));
                }
            }

            public static void EndsWith(string value, string substring, string message = null)
            {
                if (message == null)
                {
                    RegisterAction(() => StringAssert.EndsWith(value, substring));
                }
                else
                {
                    RegisterAction(() => StringAssert.EndsWith(value, substring, message));
                }
            }

            public static void Matches(string value, Regex pattern, string message = null)
            {
                if (message == null)
                {
                    RegisterAction(() => StringAssert.Matches(value, pattern));
                }
                else
                {
                    RegisterAction(() => StringAssert.Matches(value, pattern, message));
                }
            }

            public static void Contains(string value, string substring, string message = null)
            {
                if (message == null)
                {
                    RegisterAction(() => StringAssert.Contains(value, substring));
                }
                else
                {
                    RegisterAction(() => StringAssert.Contains(value, substring, message));
                }
            }

            public static void StartsWith(string value, string substring, string message = null)
            {
                if (message == null)
                {
                    RegisterAction(() => StringAssert.StartsWith(value, substring));
                }
                else
                {
                    RegisterAction(() => StringAssert.StartsWith(value, substring, message));
                }
            }


            #region Singleton Instance For Extension Methods 
            private static Strings _that;
            public static Strings That => _that ?? (_that = new Strings());
            #endregion
        }
    }
}
