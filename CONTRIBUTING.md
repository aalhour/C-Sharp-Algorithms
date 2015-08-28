# Contribution Guidelines

Hi there, and thanks for your interest in the C# ALGORITHMS repository. This document serves as a set of general guidelines for open-source contribution.

## Communication and Issues:

Whether you want to report a bug, an enhancement, a new data structure, or a new algorithm implementation please make sure you check out the issues, if there is any, regarding the data structures and/or algorithms you want to implement, someone might have started working on them already. If you are sure that what you want to contribute is new, then please open an issue describing what you want to accomplish before you decide to commit any code. This will guarantee no one else will work on the same file(s), data structure and/or algorithm you are working on.

## Coding Conventions:

The coding conventions are the official Microsoft C# Coding Conventions. Most of the official code styling is supported by default on Visual Studio, but you need to set it up as a policy on Xamarian Studio via the Solution/Project properties window, in case you are using Xamarian Studio.

Please refer to the [Framework Design Guidelines](https://msdn.microsoft.com/en-us/library/ms229042(v=vs.110).aspx) document on MSDN.

## NuGet and 3rd Party Libraries:

If your implementation depends on a 3rd party library, and you think it is critical, then please communicate this before you change the solution/projects references. We are striving to provide a bare-bones library of data structures and algorithms.

In all cases, you should not commit the installed 3rd party libraries into the project. The README document will provide all the steps anyone would need to compile the library on their machine.

## Source Code Testing

You should write a test for every data structure and algorithm you implement. The test should be created under the MainProgram project and in the corresponding folder:

 * AlgorithmsTests: The directory for all tests of implemented algorithms.
 * DataStructuresTests: THe directory for all tests of implemented data structures.

## Submitting Your Changes

After you commit your code, please submit your changes with a pull-request, you can do this on GitHub or through git. Make sure you provide a description message of your work with the pull-request.
