using KellermanSoftware.CompareNetObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizify.Api.DAL.Common.Tests
{
    public static class DeepAssert
    {
        public static void Equal<T>(T? expected, T? actual, params string[] propertiesToIgnore)
        {
            CompareLogic compareLogic = new()
            {
                Config =
            {
                MembersToIgnore = propertiesToIgnore.ToList(),
                IgnoreCollectionOrder = true,
                IgnoreObjectTypes = true,
                CompareStaticProperties = false,
                CompareStaticFields = false
            }
            };

            ComparisonResult comparisonResult = compareLogic.Compare(expected!, actual!);
            if (!comparisonResult.AreEqual)
            {
                throw new ObjectEqualException(expected!, actual!, comparisonResult.DifferencesString);
            }
        }
    }
}
   