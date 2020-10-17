using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace AssertAllTests.ExtensionMethods
{
    internal static class CollectionContainsExtensions
    {
        private const int NO_MATCH_FOUND = -1;
        private const int MATCHES_COLLISIONS_FOUND = -2;

        internal static void ArrayHasDiscreteStringsContaining(this Assert source, string[] substringsExpected, string[] stringsActual, bool matchCase = false, string message = null)
        {
            int[] matchIndexes = new int[substringsExpected.Length];
            Array.Fill(matchIndexes, NO_MATCH_FOUND);

            for (int expectedSubstringIndex = 0; expectedSubstringIndex < substringsExpected.Length; expectedSubstringIndex++)
            {
                for (int actualStringIndex = 0; actualStringIndex < stringsActual.Length; actualStringIndex++)
                {
                    string actual = stringsActual[actualStringIndex];
                    string expected = substringsExpected[expectedSubstringIndex];
                    if (actual.Contains(expected, matchCase ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase))
                    {
                        if (matchIndexes[expectedSubstringIndex] == NO_MATCH_FOUND && !matchIndexes.Contains(actualStringIndex))
                        {
                            matchIndexes[expectedSubstringIndex] = actualStringIndex;
                        }
                        else
                        {
                            matchIndexes[expectedSubstringIndex] = MATCHES_COLLISIONS_FOUND;
                        }
                    }
                }
            }

            string failureMessage = "";
            for (int index = 0; index < matchIndexes.Length; index++) {
                if (matchIndexes[index] == NO_MATCH_FOUND)
                {
                    failureMessage += $"\r\nNo string found containing substring '{substringsExpected[index]}'";
                }
                if (matchIndexes[index] == MATCHES_COLLISIONS_FOUND)
                {
                    failureMessage += $"\r\nSubstring '{substringsExpected[index]}' either appeared repeatedly or in the same element as another substring";
                }
            }

            if (failureMessage.Length > 0)
            {
                throw new AssertFailedException($"Assert.ArrayHasDiscreteStringsContaining failed.{failureMessage}\r\n{message}");
            }
        }

        internal static void ArrayLacksStringContaining(this Assert source, string substringExpected, string[] arrayActual, bool matchCase = false, string message = null)
        {
            bool substringFound = false;
            foreach (string actual in arrayActual)
            {
                if (actual.Contains(substringExpected, matchCase ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase))
                {
                    substringFound = true;
                    break;
                }
            }

            if (substringFound)
            {
                throw new AssertFailedException($"Assert.ArrayLacksStringContaining failed. {substringExpected} was a substring of an element in the array. {message}");
            }
        }
    }
}
