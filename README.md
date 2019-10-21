# C# 算法集 [![Build Status](https://travis-ci.org/aalhour/C-Sharp-Algorithms.svg?branch=master)](https://travis-ci.org/aalhour/C-Sharp-Algorithms)

一个可直接运行使用的标准的数据结构和算法类库项目，包括了35个以上的数据结构和30个以上的算法。它们使用面向对象的方式被设计为单个的组件。虽然此项目是以学习目的创建的，但是其中实现的数据结构和算法却是很标准，有效，稳定和可测的。

## 描述

此项目起初是作为面试前的准备材料而发起的。可是，在 [reddit](https://redd.it/3etf9f)和[GitHubers](https://github.com/aalhour/C-Sharp-Algorithms/graphs/contributors) 上收到大量积极反馈后，便变得有些不同的意义。所以，我决定让此项目继续保持参考项目的身份，作为数据结构和算法的C#实现的参考。

#### 项目结构:

这是个C#.NET项目，包括了三个子项目：

1、[Algorithms](Algorithms): 一个包含了算法实现的类库项目。

2、[Data Structures](DataStructures): 一个包含了数据结构实现的类库项目。

3、 [UnitTest](UnitTest): 对 Algorithms 和 Data Structures 项目做单元测试的项目。

#### 环境需求:

1. .NET Core >= 2.0
2. XUnit

#### 贡献者需知:

如果你想为此项目做贡献，请先查阅[Contribution Guidelines](CONTRIBUTING.md)

注意：此项目使用 .NET Core 2.0.3 环境的 Visual Studio Community 版，在 OSX 10 中测试。

## 数据结构

#### 排序:
  * [冒泡排序](Algorithms/Sorting/BubbleSorter.cs).
  * [桶排序](Algorithms/Sorting/BucketSorter.cs).
  * [二叉搜索树排序](Algorithms/Sorting/BinarySearchTreeSorter.cs).
  * [梳排序](Algorithms/Sorting/CombSorter.cs).
  * [计数排序](Algorithms/Sorting/CountingSorter.cs).
  * [圈排序](Algorithms/Sorting/CycleSorter.cs).
  * [侏儒排序](Algorithms/Sorting/GnomeSorter.cs).
  * [堆排序](Algorithms/Sorting/HeapSorter.cs).
  * [插入排序](Algorithms/Sorting/InsertionSorter.cs).
  * [二叉基数排序](Algorithms/Sorting/LSDRadixSorter.cs).
  * [归并排序](Algorithms/Sorting/MergeSorter.cs).
  * [选择排序](Algorithms/Sorting/SelectionSorter.cs).
  * [希尔排序](Algorithms/Sorting/ShellSorter.cs).
  * [奇偶排序](Algorithms/Sorting/OddEvenSorter.cs).
  * [鸽巢排序](Algorithms/Sorting/PigeonHoleSorter.cs).
  * [快速排序](Algorithms/Sorting/QuickSorter.cs).

#### 图:
  * 图搜索:
    + [深度优先搜索](Algorithms/Graphs/DepthFirstSearcher.cs).
    + [广度优先搜索](Algorithms/Graphs/BreadthFirstSearcher.cs).
  * 最短路径:
    + [广度优先 最短路径](Algorithms/Graphs/BreadthFirstShortestPaths.cs).
    + [贝尔曼-福特算法 最短路径](Algorithms/Graphs/BellmanFordShortestPaths.cs).
    + [特斯拉 最短路径](Algorithms/Graphs/DijkstraShortestPaths.cs).
    + [特斯拉 所有结对点 最短路径](Algorithms/Graphs/DijkstraAllPairsShortestPaths.cs).
  * 深度优先的应用:
    + [循环检测](Algorithms/Graphs/CyclesDetector.cs).
    + [拓扑排序](Algorithms/Graphs/TopologicalSorter.cs).
  * 广度优先的应用:
    + [连通分量](Algorithms/Graphs/ConnectedComponents.cs).
    + [二分图渲染](Algorithms/Graphs/BipartiteColoring.cs).

#### 树:
  * [递归二叉树](Algorithms/Trees/BinaryTreeRecursiveWalker.cs).
    + 方法: PrintAll, ForEach, Contains and BinarySearch. Traversal Modes: Preorder, Inorder & Postorder.

#### 字符串:
  * [组合与异位](Algorithms/Strings/Permutations.cs).
  * [编辑距离](Algorithms/Strings/EditDistance.cs).
    + 使用基础类 EditDistanceCostsMap<T> 传递距离: [EditDistanceCostsMap\<T\>](Algorithms/Strings/EditDistanceCostsMap.cs).

#### 数:
  * [卡特兰数](Algorithms/Numeric/CatalanNumbers.cs).
  * [最大公约数](Algorithms/Numeric/GreatestCommonDivisor.cs)

#### 可视化:
  * [树结构的显示](DataStructures/Trees/TreeDrawer.cs).

## 贡献者

  * [Edgar Carballo Domínguez](https://github.com/karv).
  * [Lucas Lemaire](https://github.com/ZwoRmi).
  * [Samuel Kenney](https://github.com/samuelkenney).
  * [Ivandro Ismael Gomes Jao](https://github.com/ivandrofly)


## 许可
此项目遵守 [MIT 许可](LICENSE).