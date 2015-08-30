NLogViewer
=============

A quick-n-dirty ASP.NET MVC application for viewing NLog logs stored in a SQL Server table. It currently shows the 100 most-recent log entries.


Requirements
------------

* Visual Studio 2015
* A SQL Server database containing an NLog table


Usage
-----

1.  Either clone the repository locally, or download the source code.
2.  Open NLogViewer.sln in Visual Studio.
3.  Modify NLogViewer.Models.Log to match the schema of your log table.
    - Be sure to make the same modifications to ~/Views/Home/Index.cshtml and ~/Views/Log/Detail.cshtml.
4.  Edit the connection string in Web.config to point to your database instance.
5.  Hit F5 to launch the site in your debugger.