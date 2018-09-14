<%@ Page Title="" Language="C#" MasterPageFile="~/_Site.Master" AutoEventWireup="true" CodeBehind="Note.aspx.cs" Inherits="LocalNotes.Note" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <!-- page-actions -->
    <asp:UpdatePanel ID="UpdatePanelNoteAction" runat="server">
        <ContentTemplate>
            <div class="site-action-fixed shadow-sm">
                <div class="btn-toolbar mb-2 mb-md-0 d-print-none justify-content-end">
                    <asp:Label ID="lblSaveStatus" runat="server" CssClass="text-success"></asp:Label>
                    <div class="btn-group ml-2">
                        <asp:Button ID="btnSave" runat="server" Text=" Save " CssClass="btn btn-outline-success" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-outline-secondary" OnClick="btnCancel_Click" />
                    </div>
                    <asp:Button ID="btnRemove" runat="server" Text="Remove" CssClass="btn btn-outline-danger ml-2" OnClick="btnRemove_Click" />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>

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
                        <a href="Home.aspx" role="button" class="btn btn-primary">Home</a>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <!-- page-detail -->
        <asp:UpdatePanel ID="UpdatePanelNoteFields" runat="server">
            <ContentTemplate>
                <div class="site-field-container">
                    <div class="form-group">
                        <asp:TextBox ID="txtNoteName" runat="server" CssClass="form-control" placeholder="Give your note a name (Required)" onkeyup="saveButtonState();"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:TextBox ID="txtNoteText" runat="server" CssClass="form-control" placeholder="Begin writing your masterpiece..." TextMode="MultiLine" Rows="15" onkeyup="saveButtonState();"></asp:TextBox>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>

    <!-- info-holders -->
    <asp:UpdatePanel ID="UpdatePanelNoteHidden" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hidNoteID" runat="server" />
            <asp:HiddenField ID="hidNoteName" runat="server" />
            <asp:HiddenField ID="hidNoteText" runat="server" />
            <asp:HiddenField ID="hidNoteTime" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- page-script -->
    <script>

        // Using jquery 3.3.1
        //(window).on('load') fires off BEFORE document.ready().

        $(window).on('load', function () {
            console.log("Window loaded.");
        });

        $(document).ready(function () {
            saveButtonState();
            removeButtonState();
            console.log("DOM loaded.");
        });

        // Remove the save status
        $(document).mousemove(function () {
            $('#<%= lblSaveStatus.ClientID %>').delay(3000).fadeOut(300);
        });

        // Adjust the enablement state of the save button.
        function saveButtonState() {
            var oldNoteName = $("#" + '<%= hidNoteName.ClientID %>').val();
            //console.log('oldName: ' + oldNoteName);
            var oldNoteText = $("#" + '<%= hidNoteText.ClientID %>').val();
            //console.log('oldText: ' + oldNoteText);
            var newNoteName = $("#" + '<%= txtNoteName.ClientID %>').val();
            //console.log('newName: ' + newNoteName);
            var newNoteText = $("#" + '<%= txtNoteText.ClientID %>').val();
            //console.log('newText: ' + newNoteText);
            if (newNoteName.length == 0) {
                //console.log('saveButtonState Enable (0)');
                $('#<%= btnSave.ClientID %>').prop('disabled', true);
            }
            else if ((oldNoteName != newNoteName) || (oldNoteText != newNoteText)) {
                //console.log('saveButtonState Enable');
                $('#<%= btnSave.ClientID %>').prop('disabled', false);
            }
            else {
                //console.log('saveButtonState Disable');
                $('#<%= btnSave.ClientID %>').prop('disabled', true);
            };
        };

        // Adjust the enablement state of the remove button.
        function removeButtonState() {
            var noteID = $("#" + '<%= hidNoteID.ClientID %>').val();
            if (noteID > 0) {
                //console.log('removeButtonState Enable');
                $('#<%= btnRemove.ClientID %>').prop('disabled', false);
            }
            else {
                //console.log('removeButtonState Disable');
                $('#<%= btnRemove.ClientID %>').prop('disabled', true);
            };
        };
    </script>

</asp:Content>
