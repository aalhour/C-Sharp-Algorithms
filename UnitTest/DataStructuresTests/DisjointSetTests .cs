using System;
using DataStructures.Sets;
using Xunit;

public class DisjointSetTests
{
    [Fact]
    public void NewSet_EachElementIsItsOwnGroup()
    {
        var ds = new DisjointSet(5);

        Assert.Equal(5, ds.Count);
        Assert.False(ds.Connected(0, 1));
    }

    [Fact]
    public void Union_MergesTwoGroups()
    {
        var ds = new DisjointSet(4);

        Assert.True(ds.Union(0, 1));
        Assert.True(ds.Connected(0, 1));
        Assert.Equal(3, ds.Count);
    }

    [Fact]
    public void Union_SameGroupTwice_ReturnsFalse()
    {
        var ds = new DisjointSet(4);
        ds.Union(0, 1);

        Assert.False(ds.Union(1, 0));
        Assert.Equal(3, ds.Count);
    }

    [Fact]
    public void Connected_IsTransitive()
    {
        var ds = new DisjointSet(4);
        ds.Union(0, 1);
        ds.Union(2, 3);
        ds.Union(1, 2);

        Assert.True(ds.Connected(0, 3));
        Assert.Equal(1, ds.Count); // 4 elements, 3 successful unions -> 1 group
    }

    [Fact]
    public void Find_ReturnsSameRootForWholeGroup()
    {
        var ds = new DisjointSet(6);
        ds.Union(0, 1);
        ds.Union(1, 2);

        int root = ds.Find(0);
        Assert.Equal(root, ds.Find(1));
        Assert.Equal(root, ds.Find(2));
    }

    [Fact]
    public void Find_OutOfRange_Throws()
    {
        var ds = new DisjointSet(3);

        Assert.Throws<ArgumentOutOfRangeException>(() => ds.Find(3));
        Assert.Throws<ArgumentOutOfRangeException>(() => ds.Find(-1));
    }

    [Fact]
    public void NegativeSize_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new DisjointSet(-1));
    }
}