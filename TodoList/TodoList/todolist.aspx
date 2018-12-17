<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="todolist.aspx.cs" Inherits="TodoList.todolist" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-3.0.0.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="site.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container2">
            <button type="button" class="btn btn-info btn-lg" data-toggle="modal" data-target="#myModal" style="background: #009688;">Todo List</button>
            <div id="myModal" class="modal fade" role="form" style="width: 100%;">

                <div class="modal-dialog" style="max-width: 1105px;">
                    <div class="modal-content" style="background-color: #009688;">
                        <div class="modal-header">
                            <h4 class="modal-title" style="color: white; background-color: #009688;">Todo-List</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times; 
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="container">
                                <label for="todoName">Hedef</label>
                                <input type="text" id="todoName" runat="server" placeholder="Hedef giriniz." style="padding: 4px;" />
                                <label for="todoTime">Zaman</label>
                                <input type="text" id="todoTime" runat="server" placeholder="Zaman giriniz." style="padding: 4px;" />
                                <input type="button" id="add" value="Ekle" class="btn btn btn-primary" style="color: white; background-color: #009688;" runat="server" onserverclick="add_ServerClick" />
                                <br />
                                <br />
                                <asp:GridView ID="GridView1" CssClass="gridViewMain" runat="server" AutoGenerateColumns="False"
                                    DataKeyNames="id" OnRowDeleting="Silme" AllowPaging="true" PageSize="5" 
                                    Width="100%" GridLines="None" ShowHeader="false" OnRowCommand="GridView1_RowCommand" OnPageIndexChanging="GridView1_PageIndexChanging">
                                    <Columns>

                                        <asp:BoundField DataField="Hedef" ShowHeader="False"></asp:BoundField>
                                        <asp:BoundField DataField="time" ShowHeader="False"></asp:BoundField>
                                        <%--<asp:CommandField DeleteText="X"  ShowDeleteButton="True" CausesValidation="False">
                                       <ItemStyle HorizontalAlign="Right" />
                                        </asp:CommandField>--%>


                                        <asp:TemplateField FooterStyle-HorizontalAlign="Right">
                                            <ItemTemplate >
                                                <asp:LinkButton ID="LinkButton1"   OnClientClick="JavaScript:return confirm ('Silmek istediğinize emin misiniz?');"
                                                    runat="server" CommandArgument='<%# Eval("id") %>' CommandName="Delete" Text="x" Font-Size="Medium" > </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>

                                    <PagerSettings Mode="Numeric"
                                        Position="bottom"
                                        PageButtonCount="10" />

                                    <PagerStyle HorizontalAlign="Right" />
                                </asp:GridView>
                                <br />

                                <asp:AccessDataSource ID="accssdb" runat="server" DataFile="~/App_Data/db.mdb"
            SelectCommand="SELECT [link] FROM [download] ORDER BY [id] DESC">
        </asp:AccessDataSource>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-primary" style="color: #009688; background-color: white; text-decoration-style: double;" data-dismiss="modal" runat="server" onserverclick="add_ServerClick2">
                                Kapat</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            function openModal() {
                $('#myModal').modal('show');
            }
        </script>
    </form>
</body>
</html>
