<%@ Page Title="" Language="C#" MasterPageFile="~/_Site.Master" AutoEventWireup="true" CodeBehind="Junk.aspx.cs" Inherits="LocalNotes.Junk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <!-- page actions -->
    <div class="site-action-fixed shadow-sm">
        <div class="btn-toolbar justify-content-end">
            <div class="btn-group d-print-none">
                <asp:Button ID="btnRestore" runat="server" Text="Restore" CssClass="btn btn-outline-primary" OnClick="btnRestore_Click" />
            </div>
        </div>
    </div>

    <!-- scroll content -->
    <div class="site-scroll-content">

        <!-- data-missing -->
        <asp:UpdatePanel ID="UpdatePanelJunkAbsent" runat="server">
            <ContentTemplate>
                <div class="site-jumbotron-container">
                    <div class="jumbotron">
                        <p>
                            <asp:Label ID="lblJunkAbsent" runat="server"></asp:Label>
                        </p>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <!-- page detail -->
        <div class="junk-container">
            <asp:Repeater ID="RepeaterItemDetail" runat="server">
                <HeaderTemplate>
                    <ul id="junkResultList" class="list-group">
                        <label class="p-0 m-0" for="selectall">
                            <li class="list-group-item list-group-item-action pb-3">
                                <input id="selectall" type="checkbox" />
                                <span class="px-1">Select All</span>
                            </li>
                        </label>
                </HeaderTemplate>
                <ItemTemplate>
                    <label class="p-0 m-0" for="item<%# DataBinder.Eval(Container.DataItem, "NoteID") %>">
                        <li class="list-group-item list-group-item-action pb-3">
                            <input id="item<%# DataBinder.Eval(Container.DataItem, "NoteID") %>" type="checkbox" name="junk" value="<%# DataBinder.Eval(Container.DataItem, "NoteID") %>" />
                            <strong class="px-1"><%# DataBinder.Eval(Container.DataItem, "NoteName") %></strong>
                            <small class="px-1"><%# DataBinder.Eval(Container.DataItem, "Modified") %></small>
                            <span class="px-1"><%# DataBinder.Eval(Container.DataItem, "NoteAbbr") %></span>
                        </li>
                    </label>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>
            </asp:Repeater>
        </div>

    </div>

    <!-- info-holders -->
    <asp:HiddenField ID="hidChecked" runat="server" />

    <!-- page-script -->
    <script type="text/javascript">

        // Using jquery 3.3.1
        //(window).on('load') fires off BEFORE document.ready().

        $(window).on('load', function () {
            console.log("Window loaded.");
        });

        $(document).ready(function () {

            // Create a change function to update checkbox values into
            // the hidChecked field to be evaluated by the server.
            $('input:checkbox[name="junk"]').change(function () {
                updateChecked();
                var checks = $("#" + '<%= hidChecked.ClientID %>').val();
                console.log("hidChecked: " + checks);
            });

            // Create a click event handler to the Select All checkboxes
            // accordign to the checkbox name when selectall is checked.
            $('#selectall').click(function (event) {
                if (this.checked) {
                    // loop through each checkbox ...
                    $('input:checkbox[name="junk"]').each(function () {
                        // check the checkbox ...
                        $(this).prop('checked', true);
                    });
                }
                else {
                    // loop through each checkbox ...
                    $('input:checkbox[name="junk"]').each(function () {
                        // uncheck the checkbox ...
                        $(this).prop('checked', false);
                    });
                }
                updateChecked();
            });

            console.log("DOM loaded.");
        });

        // Update the checked checkbox values into the hidChecked field
        // accordign to the checkbox name.
        function updateChecked() {
            console.log("updateChecked() starting ...");
            var cboxes = document.getElementsByName("junk");
            var checks = [];
            for (var i = 0; i < cboxes.length; i++) {
                if (cboxes[i].checked) {
                    checks.push(cboxes[i].value);
                    console.log("+ checked " + cboxes[i].value);
                }
                else {
                    console.log("- skipping " + cboxes[i].value);
                }
            }
            console.log("checks: " + checks);

            // Assigned checks to hidChecked ...
            $('#<%= hidChecked.ClientID %>').val(checks);

            console.log("updateChecked() finished.");
        };

    </script>

</asp:Content>
