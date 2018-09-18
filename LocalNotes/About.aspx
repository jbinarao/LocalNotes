<%@ Page Title="" Language="C#" MasterPageFile="~/_Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="LocalNotes.About" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- page actions -->
    <div class="site-action-fixed shadow-sm">
        <!-- page actions placeholder -->
    </div>

    <!-- page scroll -->
    <div class="site-scroll-content">
        <div class="site-jumbotron-container">
            <div class="jumbotron text-left">

                <h4>Welcome</h4>
                <p>
                    LocalNotes is an easy-to-use web browser application that is useful for creating and storing simple notes within your own infrastructure.
                </p>
                <br />

                <h5>Technologies</h5>
                <p>
                    The following topics are highlighted in the construction of this project.
                </p>
                <ul>
                    <li>Developed using Visual Studio 2017</li>
                    <li>Active Server Pages (ASP.NET)</li>
                    <li>Code-behind for pages written in C#</li>
                    <li>.NET Framework version used is 4.6.</li>
                    <li>ADO.NET Data Provider for SQL Client</li>
                    <li>Microsoft SQL Server for data storage</li>
                    <li>Data access using Stored Procedures</li>
                    <li>Data binding to form controls</li>
                    <li>Http Request Parameter Query</li>
                    <li>Logging exceptions to a text file</li>
                    <li>Bootstrap to provide element styling</li>
                    <li>jQuery for enhanced client actions</li>
                    <li>Custom CSS for additional styling</li>
                </ul>
                <br />

                <h5>Author</h5>
                <p>
                    Jimmy Binarao<br />
                    Email: jbinarao@gmail.com<br />
                </p>

            </div>
        </div>
    </div>

</asp:Content>
