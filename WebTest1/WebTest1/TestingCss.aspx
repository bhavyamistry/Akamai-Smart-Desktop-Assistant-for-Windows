<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestingCss.aspx.cs" Inherits="TestingCss" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Akamaii</title>
    <link rel="stylesheet" href="/Styling/StyleSheet.css"/>
    <link href='https://fonts.googleapis.com/css?family=Roboto' rel='stylesheet'>
</head>

<body>
    <div class="whole">
        <form id="form1" runat="server">
            <div class="elements">
            <div class="image_mic">
                <asp:imagebutton runat="server" ImageAlign="Middle" ImageUrl="~/Images/mic.jpg" Width="100%" Height="100%" PostBackUrl="~/Test.aspx" OnClick="Unnamed1_Click"></asp:imagebutton>                
            </div>
            <div class="fonting">
                <div id="titling">Akamaii</div>
            </div><br><br>                
            <div class="spantext">
            <div class="ak"> 
                <span>The Virtual Assistant</span>
            </div>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
