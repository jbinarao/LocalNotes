# LocalNotes
*A basic note-taking web application*


## Project
LocalNotes is an easy-to-use web browser application that is useful for creating
and storing simple notes within your own infrastructure.

### Concepts
The following topics and technologies are part of the construction of this
project.

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
In addition to the standard Visual Studio tooling, additional frameworks were
used in this project for the front-end, and are included in the
"Site\\[Widget](/LocalNotes/Site/Widget)" folder. These framework versions used
are as follows:

* jQuery 3.3.1
* Bootstrap 4.1.3

## Preparation
Before we're ready to start debugging this project within Visual Studio, let's
go over the environment and the setup within the environment.

### Environment
Here's a little bit of information about the environment in which this project
was built on.

* Microsoft Windows Server 2012 R2 Standard
* Microsoft Visual Studio 2017 Community Edition
* Microsoft SQL Server 2016 Standard Edition
* Microsoft SQL Server Management Studio 17

The target framework used for this project is Microsoft .NET Framework 4.6.1.

Microsoft SQL Server 2016 Standard is the database storage technology used to
build and test this project. Although different versions of Microsoft SQL Server
were not tested at the time of development, previous versions or other editions
should not be an issue.

### Setup
Assuming that the development environment is ready with the project, there are
two actions that need to be taken next.

* Prepare the SQL Server database
* Review the project's web.config file

#### 1. Database
In order to create the database created for this project in your environment, a
SQL *setup script* is included in this project. On a new SQL instance, the
database, table, columns, data types, and stored procedures are created.

##### *LocalNotes DB Setup Script.sql*
This script is located in the [Preparation](/Preparation) folder, and can be
used with SQL Server Management Studio will quickly setup the exact same
database environment to another SQL server in seconds.

> Note: *This script has been lab-tested. As a best practice, it is recommended
that running any script be done so in your lab, development, or sandbox
environment in order to ensure proper operation before doing so in any other
environment.*

To use the script to create the database, perform the following steps:

1. Login to **SQL Server Management Studio**.

2. Place the script inside the query view. Here are a few different ways to do
so:

  * Click on **File**, choose **Open**, click **File...**, and navigate to the
  *LocalNotes DB Setup Script.sql* file.

  * Right-click on the *LocalNotes DB Setup Script.sql* file, and in the context
  menu, choose **Open with**, then click the **SSMS** option.

  * Click on **New Query** to open a new query tab. Then, Open the *LocalNotes
  DB Setup Script.sql* file in a text editor, select all text, copy, and paste
  into the query editor.

  * Click on **New Query** to open a new query tab. Then, drag-and-drop the
  *LocalNotes DB Setup Script.sql* file onto the query editor.

3. Click on the **Execute** button... Done!

##### *Checking the Script Operation*
After running the *LocalNotes DB Setup Script.sql*, before closing SQL Server
Management Studio, inspect the Object Explorer to ensure the following items
were properly placed.
* Within the SQL instance, verify that the following database was created:
  * *LocalNotes*
* Within the *LocalNotes* database, under *Tables*, verify that the following
table was created:
  * *NoteData*
* Within the *LocalNotes* database, under *Programmibility*, then *Stored
Procedures*, verify that the following six stored procedures were created:
  * spInsertNoteData
  * spSelectActiveNote
  * spSelectNoteData
  * spUpdateNoteDetail
  * spUpdateNoteRemove
  * spUpdateNoteRestore

#### 2. Web.Config
Concerning the connection to the database, a connection string named
"AppConnString" was defined in the project's *web.config* file. Depending on the
environment, the *connection string* may need to be further modified, in
particular the *Data Source* value.

For example, a database instance using a default installation of SQL Server
Express is usually different when compared to other editions of SQL Server.

The following *connection string* in the *web.config* file shows the *Data
Source* defined as a *dot* (.) to indicate the local server.

```
<connectionStrings>
  <add
    name="AppConnString"
    connectionString="Data Source=.;
    Initial Catalog=LocalNotes;
    Integrated Security=True"
    providerName="System.Data.SqlClient"
    />
</connectionStrings>
```


## Operation
Now that the environment is ready, it's time to run the project to test the
operation.

* At the top of the page is the *Navigation* section where different pages of
the application can be selected.

* Further down the page is the *Actions* section, which contains relevant
actions for the existing page.

* Finally, the *Content* section is a scrollable region for the page.

### Pages
This project consists of a site master with four content pages.
* Home.aspx
* Note.aspx
* Junk.aspx
* About.aspx

The following describes the only three pages that are related to managing notes.

#### Home.aspx
This page is where a user would normally begin, and where new notes are created,
and existing notes are displayed. When there are no notes, the **New Note**
button will conditionally *blink* in order to aid the user as to what the next
action should be.

* To create a note, click the **New Note** button in the page *Actions* section.

* To modify an existing note, click on its *card* in the page *Content* section.

#### Note.aspx
This page is accessed by creating a new note or modifying an existing note.

To create or modify a note:
1. In the *Content* section, enter a *name* for the note.

2. Write some *text* for the note.

3. In the *Actions* section, click **Save** button to store the note.

To remove a note:

1. In the *Actions* section, click the **Remove** button.

To navigate back to the Home page without saving, either click on the **Home**
link in the *Navigation* section, or click on the **Cancel** button in the
*Actions* section.

#### Junk.aspx
Notes that are removed can be restored.

To restore a note:

1. In the *Content* section, select the note from the list.

2. In the *Actions* section, click the **Restore** button.

### Errors
When an exception occurs as the project is running, an effort is made to catch
it, and present information to the user in a thoughtful way. A *Log* folder is
created in the project root folder, where a daily log file keeps a record of the
exception details.

Referring back to the *web.config* file, a setting exists to define the number
of days to retain the logs.

```
<appSettings>
  <add key="DaysToRetainLog" value="7"/>
</appSettings>
```

## Author
Jimmy Binarao
jbinarao@gmail.com
