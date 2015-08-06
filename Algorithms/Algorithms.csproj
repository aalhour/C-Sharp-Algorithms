<?xml version="1.0" encoding="utf-8"?>
<Project DefaultTargets="Build" ToolsVersion="4.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProjectGuid>{8F59D573-A1B8-438F-A67A-A7F11FACF201}</ProjectGuid>
    <OutputType>Library</OutputType>
    <RootNamespace>Algorithms</RootNamespace>
    <AssemblyName>Algorithms</AssemblyName>
    <TargetFrameworkVersion>v4.5</TargetFrameworkVersion>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>bin\Debug</OutputPath>
    <DefineConstants>DEBUG;</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
    <ConsolePause>false</ConsolePause>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
    <DebugType>full</DebugType>
    <Optimize>true</Optimize>
    <OutputPath>bin\Release</OutputPath>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
    <ConsolePause>false</ConsolePause>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="System" />
  </ItemGroup>
  <ItemGroup>
    <Compile Include="Graphs\BellmanFordShortestPaths.cs" />
    <Compile Include="Graphs\CyclesDetector.cs" />
    <Compile Include="Graphs\DijkstraAllPairsShortestPaths.cs" />
    <Compile Include="Graphs\DijkstraShortestPaths.cs" />
    <Compile Include="Numeric\BinomialCoefficients.cs" />
    <Compile Include="Numeric\CatalanNumbers.cs" />
    <Compile Include="Properties\AssemblyInfo.cs" />
    <Compile Include="Sorting\BinarySearchTreeSorter.cs" />
    <Compile Include="Sorting\CountingSorter.cs" />
    <Compile Include="Sorting\HeapSorter.cs" />
    <Compile Include="Sorting\InsertionSorter.cs" />
    <Compile Include="Sorting\QuickSorter.cs" />
    <Compile Include="Sorting\MergeSorter.cs" />
    <Compile Include="Common\Comparers.cs" />
    <Compile Include="Common\Helpers.cs" />
    <Compile Include="Graphs\BreadthFirstShortestPaths.cs" />
    <Compile Include="Graphs\BreadthFirstSearcher.cs" />
    <Compile Include="Graphs\DepthFirstSearcher.cs" />
    <Compile Include="Graphs\TopologicalSorter.cs" />
    <Compile Include="Strings\Permutations.cs" />
  </ItemGroup>
  <Import Project="$(MSBuildBinPath)\Microsoft.CSharp.targets" />
  <ItemGroup>
    <ProjectReference Include="..\DataStructures\DataStructures.csproj">
      <Project>{464251A0-3667-42BA-A3D5-0581D65C442B}</Project>
      <Name>DataStructures</Name>
    </ProjectReference>
  </ItemGroup>
  <ItemGroup />
  <ItemGroup />
  <ItemGroup />
  <ItemGroup>
    <Folder Include="Strings\" />
  </ItemGroup>
</Project>