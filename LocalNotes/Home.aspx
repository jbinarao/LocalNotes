<%@ Page Title="" Language="C#" MasterPageFile="~/_Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="LocalNotes.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <!-- page actions -->
    <div class="site-action-fixed shadow-sm">
        <div class="btn-toolbar justify-content-end">
            <div class="btn-group d-print-none">
                <asp:Button ID="btnCreate" runat="server" Text="+ New Note" CssClass="btn btn-outline-success" OnClick="btnCreate_Click" />
            </div>
        </div>
    </div>

    <!-- scroll content -->
    <div class="site-scroll-content">

        <!-- data-missing -->
        <asp:UpdatePanel ID="UpdatePanelNoteAbsent" runat="server">
            <ContentTemplate>
                <div class="site-jumbotron-container">
                    <div class="jumbotron">
                        <p>
                            <asp:Label ID="lblNoteAbsent" runat="server"></asp:Label>
                        </p>
                        <asp:Button ID="btnNewNote" runat="server" Text="+ New Note" CssClass="btn btn-success" OnClick="btnCreate_Click" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <!-- page-detail -->
        <div class="site-card-row">
            <asp:Repeater ID="RepeaterItemDetail" runat="server">
                <ItemTemplate>
                    <div class="col-md-4 py-3">
                        <div class="card site-card list-group-item-action">
                            <a href="Note.aspx?noteid=<%# DataBinder.Eval(Container.DataItem, "NoteID") %>">
                                <div class="card-body">
                                    <h5 class="card-title"><%# DataBinder.Eval(Container.DataItem, "NoteName") %></h5>
                                    <p class="card-text"><%# DataBinder.Eval(Container.DataItem, "NoteAbbr") %></p>
                                    <small class="text-muted"><%# DataBinder.Eval(Container.DataItem, "Modified") %></small>
                                </div>
                            </a>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

    </div>

    <!-- info-holders -->
    <asp:HiddenField ID="hidRecords" runat="server" />

    <!-- page-script -->
    <script type="text/javascript">

        // Using jquery 3.3.1
        //(window).on('load') fires off BEFORE document.ready().

        $(window).on('load', function () {
            console.log("Window loaded.");
        });

        $(document).ready(function () {

            // Blink the Create button.
            $('#<%=btnCreate.ClientID%>').hide();
            var rowsCount = <%=hidRecords.Value %>;
            if (rowsCount > 0) {
                BlinkCreateButton(rowsCount);
            }

            // Restore the Create button to the default css class.
            $('#<%=btnCreate.ClientID%>').hover(function () {
                SetDefaultCreateClass();
            });

            console.log("DOM loaded.");
        });

        // Blink the new note button to assist a new user by drawing
        // attention to the next action on this page.
        function BlinkCreateButton(rowsCount) {
            var i;
            var notes = 3;  // The number of notes to apply the effect (3)
            var loops = 3;  // The number of blinks
            if (rowsCount <= notes) {
                $('#<%=btnCreate.ClientID%>').removeClass();
                $('#<%=btnCreate.ClientID%>').addClass('btn btn-success');
                $('#<%=btnCreate.ClientID%>').delay(300);
                for (i = 0; i < loops; i++) {
                    $('#<%=btnCreate.ClientID%>').fadeIn(300).fadeOut(300);
                }
                $('#<%=btnCreate.ClientID%>').fadeIn(300)
            }
            else {
                $('#<%=btnCreate.ClientID%>').show();
            }
        };

        // Restore the default css class to the create button.
        function SetDefaultCreateClass() {
            $('#<%=btnCreate.ClientID%>').removeClass();
            $('#<%=btnCreate.ClientID%>').addClass('btn btn-outline-success');
        };
    </script>

</asp:Content>
