2016-01-22 Twitter-like feed(tlf) application assessment.

This assessment could have been done in a single function but the problem
definition does state "of sufficient quality to run on a production system".
So in some cases I catered for how the application may fit in with 
other(unknown) parts of the system.

The program was designed based on:
Performance(For Real-time production system)
Maintainability
Readability

Some specific exceptions such as the argument exceptions were deliberately
excluded in the read functions but are checked in the unit tests. On runtime,
however they will be caught by the main program function.


To run the application:
1. See bin\release folder for exe.
2. The user and tweet files should already be copied in that location.
3. This is a command line program whihc takes in command line arguments.
4. The command line arguments are these two file names. You may specifiy
   a file path if these two files are not in the same directory as the exe.
5. There is already a command/bat file in the release folder. One may execute 
   this command(*.cmd) file to run the exe. The file names are included in the 
   command file.
6. The command file name is "call tlf.cmd". Please run this.