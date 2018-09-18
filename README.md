# LocalNotes
LocalNotes - a simple note taking web application

## Project
LocalNotes is an easy-to-use web browser application that is useful for creating and storing simple notes within your own infrastructure.

### Concepts
The following topics and technologies are part of the construction of this project.

* Developed using Visual Studio 2017
* Active Server Pages (ASP.NET)
* Code-behind for pages written in C#
* .NET Framework version used is 4.6.
* ADO.NET Data Provider for SQL Client
* Microsoft SQL Server for data storage
* Data access using Stored Procedures
* Data binding to form controls
* Http Request Parameter Query
* Logging exceptions to a text file
* Bootstrap to provide element styling
* jQuery for enhanced client actions
* Custom CSS for additional styling

### Frameworks
Additional frameworks were used for this project for the front-end, and are included in the "Site\\Widget" folder. The versions used are as follows:

* jQuery 3.3.1 for enhanced client actions
* Bootstrap 4.1.3 to provide element styling

## Preparation
The next stop for this project project The following describes the o get started, make sure the following are installed:

Environment
Setup

### Environment
Here's a little bit of information about the environment that this project was built on.

* Microsoft Windows Server 2012 R2 Standard
* Visual Studio 2017 Community Edition
* Microsoft SQL Server 2016 Standard Edition
* SQL Server Management Studio 17

### Setup
Assuming that the development environment is installed, and the project is in Visual Studio, before we're ready to start debugging within the IDE, the two things that need to be done are to prepare the SQL Server database and prepare the web.config file.

#### Database
Microsoft SQL Server 2016 Standard is the database storage technology used to build and test this project. Although different versions of SQL Server have not been tested at this time, there should not be any reason why previous versions or other editions would be problematic.

In order to match the database created for this project in your environment, a SQL *setup script* is included in this project. Its review describes the details about the database, such as the table schema and data types used, so it can also make a good reference about the particulars.

##### LocalNotes DB Setup Script.sql

Using this SQL script will quickly setup the exact same database environment to a SQL server in seconds.

> Note: *This script has been lab-tested. As a best practice, it is recommended that running any script be done so in your lab, development, or sandbox environment in order to ensure proper operation before doing so in any other environment.*

1. Login to **SQL Server Management Studio**.

2. Place the script inside the query view. Here are some different ways to do so:

  * Click on **File**, choose **Open**, click **File...**, and navigate to the *LocalNotes DB Setup Script.sql* file.

  * Right-click on the *LocalNotes DB Setup Script.sql* file, and in the context menu, choose **Open with**, and click on the **SSMS** option.

  * Click on **New Query** to open a new query tab. Then, Open the *LocalNotes DB Setup Script.sql* file in a text editor, select all text, copy, and paste into the query editor.

  * Click on **New Query** to open a new query tab. Then, drag-and-drop the *LocalNotes DB Setup Script.sql* file onto the query editor.

3. Click on the **Execute** button... Done!

##### Checking the Script Operation

After running the *LocalNotes DB Setup Script.sql*, before closing SQL Server Management Studio, inspect the Object Explorer to ensure the following items were properly placed.
* With the SQL instance, verify that the following database was created.
  * *LocalNotes*
* With the *LocalNotes* database, under *Tables*, verify that the following table was created.
  * *NoteData*
* With the *LocalNotes* database, under *Programmibility*, *Stored Procedures*, verify that the following six stored procedures were created:
  * spInsertNoteData
  * spSelectActiveNote
  * spSelectNoteData
  * spUpdateNoteDetail
  * spUpdateNoteRemove
  * spUpdateNoteRestore

#### Web.Config

Concerning the connection to the database, a connection string named "AppConnString" was defined in the project's web.config file. Depending on the environment, this connection string may need to be further modified, in particular the Data Source.

For example, the default instance and connection to a SQL Server Express installation is usually different than that of other editions of SQL Server, and may need to also be included as part of the Data Source definition.

```
<connectionStrings>
  <add name="AppConnString" connectionString="Data Source=.;Initial Catalog=LocalNotes;Integrated Security=True" providerName="System.Data.SqlClient"/>
</connectionStrings>
```

## Operation

Now that the environment is ready, it's time to run the project to test the operation.

### Home Page
