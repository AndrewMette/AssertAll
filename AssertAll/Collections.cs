using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AssertAll
{
    public partial class AssertAll
    {
        public partial class Collections
        {
            public static void AreEqual(ICollection expected, ICollection actual, string message = null)
            {
                if (message == null)
                {
                    RegisterAction(() => CollectionAssert.AreEqual(expected, actual));
                }
                else
                {
                    RegisterAction(() => CollectionAssert.AreEqual(expected, actual, message));
                }
            }

            public static void AreNotEqual(ICollection expected, ICollection actual, string message = null)
            {
                if (message == null)
                {
                    RegisterAction(() => CollectionAssert.AreNotEqual(expected, actual));
                }
                else
                {
                    RegisterAction(() => CollectionAssert.AreNotEqual(expected, actual, message));
                }
            }

            public static void AllItemsAreInstancesOfType(ICollection collection, Type type, string message = null)
            {
                if (message == null)
                {
                    RegisterAction(() => CollectionAssert.AllItemsAreInstancesOfType(collection, type));
                }
                else
                {
                    RegisterAction(() => CollectionAssert.AllItemsAreInstancesOfType(collection, type, message));
                }
            }
            
            public static void AllItemsAreNotNull(ICollection collection, string message = null)
            {
                if (message == null)
                {
                    RegisterAction(() => CollectionAssert.AllItemsAreNotNull(collection));
                }
                else
                {
                    RegisterAction(() => CollectionAssert.AllItemsAreNotNull(collection, message));
                }
            }

            public static void AllItemsAreUnique(ICollection collection, string message = null)
            {
                if (message == null)
                {
                    RegisterAction(() => CollectionAssert.AllItemsAreUnique(collection));
                }
                else
                {
                    RegisterAction(() => CollectionAssert.AllItemsAreUnique(collection, message));
                }
            }
            
            public static void AreEquivalent(ICollection expected, ICollection actual, string message = null)
            {
                if (message == null)
                {
                    RegisterAction(() => CollectionAssert.AreEquivalent(expected, actual));
                }
                else
                {
                    RegisterAction(() => CollectionAssert.AreEquivalent(expected, actual, message));
                }
            }

            public static void AreNotEquivalent(ICollection expected, ICollection actual, string message = null)
            {
                if (message == null)
                {
                    RegisterAction(() => CollectionAssert.AreNotEquivalent(expected, actual));
                }
                else
                {
                    RegisterAction(() => CollectionAssert.AreNotEquivalent(expected, actual, message));
                }
            }
            
            public static void Contains(ICollection collection, object element, string message = null)
            {
                if (message == null)
                {
                    RegisterAction(() => CollectionAssert.Contains(collection, element));
                }
                else
                {
                    RegisterAction(() => CollectionAssert.Contains(collection, element, message));
                }
            }

            public static void DoesNotContain(ICollection collection, object element, string message = null)
            {
                if (message == null)
                {
                    RegisterAction(() => CollectionAssert.DoesNotContain(collection, element));
                }
                else
                {
                    RegisterAction(() => CollectionAssert.DoesNotContain(collection, element, message));
                }
            }
            
            public static void IsSubsetOf(ICollection subset, ICollection superset, string message = null)
            {
                if (message == null)
                {
                    RegisterAction(() => CollectionAssert.IsSubsetOf(subset, superset));
                }
                else
                {
                    RegisterAction(() => CollectionAssert.IsSubsetOf(subset, superset, message));
                }
            }

            public static void IsNotSubsetOf(ICollection subset, ICollection superset, string message = null)
            {
                if (message == null)
                {
                    RegisterAction(() => CollectionAssert.IsNotSubsetOf(subset, superset));
                }
                else
                {
                    RegisterAction(() => CollectionAssert.IsNotSubsetOf(subset, superset, message));
                }
            }

            #region Singleton Instance For Extension Methods 
            private static Collections _that;
            public static Collections That => _that ?? (_that = new Collections());
            #endregion
        }
    }
}
