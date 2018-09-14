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
                    LocalNotes is an easy-to-use web browser application that is useful for creating and storing simple notes inside of your own infrastructure.
                </p>

                <br />
                <h5>Technologies</h5>
                <p>
                    LocalNotes was developed using Visual Studio 2017, and the code-behind for Active Server Pages (ASP) were written in C# .NET. Additional technologies used for this build include the following:
                </p>
                <ul>
                    <li>jQuery for enhanced client actions</li>
                    <li>Bootstrap to provide element styling</li>
                    <li>Custom CSS for additional page styling</li>
                    <li>Microsoft SQL Server for data storage</li>
                    <li>IIS Server to run active server pages</li>
                </ul>

                <br />
                <h5>Author</h5>
                <p>
                    Jimmy Binarao<br />
                    Email: jbinarao@gmail.com<br />
                    <br />
                    Experienced in .NET development using Visual Studio with both front and back end stacks in C# and VB .NET, as well as relational databases, including Microsoft SQL Server. Experienced with analyzing, planning, designing, and creating business workflows with emphasis on combining business process automation, data-driven technologies, and user adaption. Experienced managing document imaging and secure print solutions.
                </p>

            </div>
        </div>
    </div>

</asp:Content>
