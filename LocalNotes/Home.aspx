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
        <asp:UpdatePanel ID="UpdatePanelDataAbsent" runat="server">
            <ContentTemplate>
                <div class="site-jumbotron-container">
                    <div class="jumbotron">
                        <p>
                            <asp:Label ID="lblDataAbsent" runat="server"></asp:Label>
                        </p>
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

            // Declare the Create button ...
            var buttonCreate = $('#<%=btnCreate.ClientID %>');

            // Hide the Create button to ease in the button ...
            buttonCreate.hide();

            // Get the number of records assigned by the server ...
            var recordCount = <%=hidRecords.Value %>;
            console.log("Record Count: " + recordCount);

            // Blink the Create button ...
            BlinkButtonConditionally(buttonCreate, recordCount);

            // Restore the default css class to the Create button on hover.
            buttonCreate.hover(function () {
                SetSuccessOutlineClass(buttonCreate);
            });

            console.log("DOM loaded.");
        });

        // Conditionally 'blink' the button to assist a new user by drawing
        // their attention to the next action on this page.
        function BlinkButtonConditionally(button, recordCount) {
            var i;
            var neededCount = 1;  // The number of needed notes to apply the effect (Suggested: 1)
            var blinksCount = 3;  // The number of blinks to apply (Suggested: 3)
            
            if (recordCount < neededCount) {
                button.removeClass();
                button.addClass('btn btn-success');
                button.delay(300);
                for (i = 0; i < blinksCount; i++) {
                    button.fadeIn(300).fadeOut(300);
                }
                button.fadeIn(300)
            }
            else {
                button.show();
            }
        };

        // Assign the normal css class to the button.
        function SetSuccessOutlineClass(button) {
            button.removeClass();
            button.addClass('btn btn-outline-success');
        };
    </script>

</asp:Content>
