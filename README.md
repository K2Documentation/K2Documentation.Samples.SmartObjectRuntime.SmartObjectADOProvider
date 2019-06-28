# K2Documentation.Samples.SmartObjectRuntime.SmartObjectADOProvider
This project contains sample code that shows how to use the K2 ADO.NET provider dll to execute SQL-Like queries against K2 SmartObjecta at runtime, such as:  
* reading a signle record
* retrieving multiple records

## Prerequisites
The sample code has the following dependencies: 
* .NET Assemblies (assembly is included with K2 client-side tools install and are also included in the project's References folder)
  * SourceCode.Data.SmartObjectsClient.dll 

## Getting started
* Use these code snippets to learn how to perform common tasks with K2 SmartObjects through the  SMartObject ADO.NET client dll
* Note that this projects should compile and run as-is since it uses the standard Users_and_Groups and WorkflowReportingService_WorkflowInstanceReport SmartObjects. You may need to edit the code snippets to work in your environment and with your artifacts, if your environment does not contain the artifacts used by the sampel code. 
* Fetch or Pull the appropriate branch of this project for your version of K2. 
* The Master branch is considered the latest, up-to-date version of the sample project. Older versions will be branched. For example, there may be a branch called K2-Five-5.0 that is specific to K2 Five version 5.0. There may be another branch called K2-Five-5.1 that is specific to K2 Five version 5.3. Assume that the master branch is configured for the latest release version of K2 Five. 
* The Visual Studio project contains a folder called "References" where you can find the referenced .NET assemblies or other uncommon assemblies. By default, try to reference these assemblies from your own environment for consistency, but we provide the referenced assemblies as a convenience in case you are not able to locate or use the referenced assemblies in your environment. 
* The Visual Studio project contains a folder called "Resources". This folder contains addiitonal resources that may be required to use the same code, such as K2 deployment packages, sample files, SQL scripts and so on. 
   
## License
This project is licensed under the MIT license, which can be found in LICENSE.

## Notes
 * The sample code is provided as-is without warranty.
 * These sample code projects are not supported by K2 product support. 
 * The sample code is not necessarily comprehensive for all operations, features or functionality. 
 * We only accept code contributions that are compatible with the MIT license (essentially, MIT and Public Domain).
