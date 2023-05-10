using Apparatus;
using System;
using System.Collections.Generic;
using Xunit;

namespace ApparatusTests
{

    public class CollectionExtensionTests
    {
        [Fact]
        public void IsNullOrEmptyIsTrueIfCollectionIsNull()
        {
            List<int> a = null;
            Assert.True(a.IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmptyIsTrueIfCollectionIsZeroElement()
        {
            List<int> a = new List<int>();
            Assert.True(a.IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmptyIsFalseIfCollectionIsHavingOneOrMoreElements()
        {
            List<int> a = new List<int>();
            a.Add(1);
            Assert.False(a.IsNullOrEmpty());
            a.Add(2);
            Assert.False(a.IsNullOrEmpty());
        }

        [Fact]
        public void RemoveAllThrowsNullExceptionForSourceIsNull()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                List<int> source = null;
                var dest = new List<int>();
                dest.Add(1);
                source.RemoveAll(dest);
            });

            Assert.Equal("source", ex.ParamName);
        }

        [Fact]
        public void RemoveAllThrowsNullExceptionForDestIsNull()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                List<int> source = new List<int>();
                List<int> dest = null;

                source.RemoveAll(dest);
            });

            Assert.Equal("items", ex.ParamName);
        }

        [Fact]
        public void RemoveAllRemoveCorrectItems()
        {
            var source = new List<int> { 1, 2, 3 };
            int[] dest = { 1, 2, 3 };
            source.RemoveAll(dest);

            Assert.Empty(source);

            source = new List<int> { 1, 2, 3, 4, 5 };
            source.RemoveAll(dest);

            Assert.Equal(2, source.Count);
            Assert.Equal(4, source[0]);
            Assert.Equal(5, source[1]);

            source = new List<int> { 6, 7, 8, 9 };
            int[] dest2 = { 8 };
            source.RemoveAll(dest2);

            Assert.Equal(3, source.Count);
            Assert.Equal(9, source[2]);
        }
    }
}
