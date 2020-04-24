# Contribution Guidelines

Hi there, and thanks for your interest in the C# ALGORITHMS repository. This document serves as a set of general guidelines for open-source contribution.

Please follow the [Contributor Code of Conduct](CODE_OF_CONDUCT.md) for all your interactions.

## Types of Contributions

Please note that an [issue](https://github.com/aalhour/C-Sharp-Algorithms/issues) is **required** for all project contributions. The following is a list of the different types of contributions:

- _To help keep the project running smoothly_
    - [Report Bugs](https://github.com/aalhour/C-Sharp-Algorithms/issues)
    - [Fix Bugs](https://github.com/aalhour/C-Sharp-Algorithms/issues)

- _To help improve existing functionality_
    - [Change Request](https://github.com/aalhour/C-Sharp-Algorithms/issues)
    - [Implement Changes](https://github.com/aalhour/C-Sharp-Algorithms/issues)

- _To help increase the projects functionality_
    - [Feature Request](https://github.com/aalhour/C-Sharp-Algorithms/issues)
    - [Algorithm Request](https://github.com/aalhour/C-Sharp-Algorithms/issues)
    - [Data Structure Request](https://github.com/aalhour/C-Sharp-Algorithms/issues)

## Communication and Issues

Please make sure you check out the issues first, someone might have started working on a similar idea already. If you are sure that what you want to contribute is new, then please open an issue describing what you want to implement before you decide to submit a PR.

Please refer to the [Issue Templates](ISSUE_TEMPLATE) to see the different types of contributions.

## Submitting a Pull Requests

Pull requests should be submitted from your cloned repository's `master` branch to the upstream `master` branch. Please make sure that you rebase your local branch against the upstream branch before you submit your pull request.

Please make sure to refer to the [Pull Request Guideline](PULL_REQUEST_TEMPLATE.md) when submitting a new one.

## Coding Conventions

We follow the official Microsoft C# Coding Conventions (see: below). Most of the styleguide is supported by default on Visual Studio, but you need to set it up as a policy from the Solution/Project properties window, in case you are using Visual Studio Community.

Please refer to the following guidelines:

  * [MS C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions)
  * [MS Framework Design Guidelines](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/index?redirectedfrom=MSDN)

## NuGet and 3rd Party Libraries

If your implementation depends on a 3rd party library, and you think it is critical, then please communicate this before you change the solution/projects references. We are striving to provide a bare-bones library of data structures and algorithms.

In all cases, you should not commit the installed 3rd party libraries into the project. The README document will provide all the steps anyone would need to compile the library on their machine.

## Source Code Testing

You should write a test for every data structure and algorithm you implement. The test should be created under the `UnitTest` project and in the corresponding packages:

 * `AlgorithmsTests`: Package hosting unit tests for the Algorithms project.
 * `DataStructuresTests`: Package hosting unit tests for the Data Structures project.
