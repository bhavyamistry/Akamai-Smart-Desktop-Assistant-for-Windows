<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Test" %>

<!DOCTYPE html>
<html>
    <head runat="server">
        <title>Speech Recognition</title>
        <meta charset="utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <!-- <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/shoelace-css/1.0.0-beta16/shoelace.css"> -->
        <link rel="stylesheet" href="/Styling/styles.css">

    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.1/css/all.css" />
        <link href='https://fonts.googleapis.com/css?family=Roboto' rel='stylesheet'>

        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
        <script type="text/javascript">

            $(function () {
               
                try
                {
                    var recognition = new webkitSpeechRecognition();
                } catch (e)
                {
                   // var recognition = Object;
                }
                recognition.continuous = true;
                recognition.interimResults = true;
                recognition.lang = "en";
               
                recognition.onresult = function (event)
                {
                    var status = $("#<%=status.ClientID %>").text();
                    var txtRec = '';
                   
                    for (var i = event.resultIndex; i < event.results.length; ++i)
                    {
                        txtRec += event.results[i][0].transcript;
                       
                    }
                    $('#txtArea').val(txtRec);
                    var s1 = $('#txtArea').val();
                    setInterval(function ()
                    {
                        var s = $('#txtArea').val();
                        var a1 = new Array();
 
                        a1 = s.split(" ");
                        var counter = sessionStorage.getItem("code");
                        var event_name = sessionStorage.getItem("ename");
                        var event_value = sessionStorage.getItem("evalue");
                        var per = sessionStorage.getItem("permission");
                        var rem = localStorage.getItem("remember");
                        var np = sessionStorage.getItem("notepad");
                        var sze = a1.length;

                        if (a1[0] == "open" && counter == "1" && np == "0" && sze>1)
                        {
                            if (a1[1] == "Notepad")
                            {
                                window.location.href = "Fetch.aspx?type='" + s + "'";
                                sessionStorage.setItem("open_note", "1");
                            }
                            else if (a1[1] == "Paint" || a1[1] == "paint")
                            {
                                window.location.href = "Fetch.aspx?type='" + s + "'";
                                sessionStorage.setItem("open_paint", "1");
                            }
                            else if (a1[1] == "Chrome" || a1[1] == "chrome")
                            {
                                window.location.href = "Fetch.aspx?type='" + s + "'";
                                sessionStorage.setItem("open_chrome", "1");
                            }
                            else if (a1[1] == "file" && a1[2] == "explorer")
                            {
                                window.location.href = "Fetch.aspx?type='" + s + "'";
                                sessionStorage.setItem("open_explorer", "1");
                            }
                            else if (a1[1] == "word" || a1[1] == "Word")
                            {
                                window.location.href = "Fetch.aspx?type='" + s + "'";
                                sessionStorage.setItem("open_word", "1");
                            }
                            else if (a1[1] == "powerpoint" || a1[1] == "Powerpoint")
                            {
                                window.location.href = "Fetch.aspx?type='" + s + "'";
                                sessionStorage.setItem("open_ppt", "1");
                            }
                            else if (a1[1] == "excel" || a1[1] == "Excel")
                            {
                                window.location.href = "Fetch.aspx?type='" + s + "'";
                                sessionStorage.setItem("open_excel", "1");
                            }
                            else if (a1[1] == "calculator" || a1[1] == "Calculator")
                            {
                                window.location.href = "Fetch.aspx?type='" + s + "'";
                                sessionStorage.setItem("open_calc", "1");
                            }
                            else if(a1[1].includes("www") || a1[1].includes(".com"))
                            { 
                                    window.location.href = "Fetch.aspx?type='" + s + "'";
                            }
                            else if (a1[1] == "new" || a1[1] == "New")
                            {
                                if (a1[2] == "tab" || a1[2] == "Tab" || a1[2] == "window" || a1[2] == "Window") {
                                    window.location.href = "Fetch.aspx?type='" + s + "'";
                                }
                                else
                                {
                                    window.location.href = "Fetch.aspx?type='command not found'";
                                }
                            }
                            else
                            {
                                window.location.href = "Fetch.aspx?type='command not found'";
                            }
                            
                            sessionStorage.setItem("open", "1");
                        }
                        else if (np == "1")
                        {
                            if (a1[0] == "close")
                            {
                                if (sze > 1)
                                {
                                    if (a1[1] == "notepad" || a1[1] == "Notepad")
                                    {
                                        sessionStorage.setItem("notepad", "0");
                                        window.location.href = "Fetch.aspx?type='" + s + "'";
                                    }
                                }
                            }
                            window.location.href = "Fetch.aspx?type='" + s + "'";
                        }
                        else if (a1[0] == "restart" || a1[0] == "shut" || a1[0] == "shutdown" && counter=="1" && np=="0")
                        {
                            if (a1[1] == null)
                            {
                                window.location.href = "Fetch.aspx?type='" + s + "'";
                                sessionStorage.setItem("permission", "1");
                            }
                            else
                            {
                                window.location.href = "Fetch.aspx?type='command not found'";
                            }
                            //sessionStorage.setItem("code", "0");
                        }
                        else if ((a1[0] == "yes" || a1[0] == "Yes" || a1[0] == "no" || a1[0] == "No") && per=="1" && np=="0")
                        {
                            sessionStorage.setItem("permission", "0");
                            window.location.href = "Fetch.aspx?type='" + s + "'";                               
                        }
                        else if (a1[0] == "when" || a1[0] == "When" || a1[0]=="Where" || a1[0]=="where" && np=="0" && sze>1)
                        {
                            if (a1[1] == "is" || a1[1] == "Is")
                            {
                                window.location.href = "Fetch.aspx?type='" + s + "'";                               
                            }
                            else
                            {
                                window.location.href = "Fetch.aspx?type='command not found'";
                            }
                        }
                        else if (a1[0] == "Remember" || a1[0] == "remember" && np == "0" && sze>1)
                        {
                            if (a1[1] == "event" || a1[1] == "event")
                            {
                                sessionStorage.setItem("remember", "1");
                                window.location.href = "Fetch.aspx?type='" + s + "'";
                            }
                        }                       
                        else if (a1[0] == "How" || a1[0] == "how" || a1[0]=="how's" && np=="0" && sze > 1)
                        {
                            if (a1[1] == "is" && a1[2] == "the" && a1[3] == "Josh" || a1[3] == "josh")
                            {
                                window.location.href = "Fetch.aspx?type='" + s + "'";    
                            }
                            else
                            {
                                window.location.href = "Fetch.aspx?type='command not found'";
                            }
                        }
                        else if (a1[0] == "right" || a1[0] == "write" || status == "done" && np=="0")
                        {
                            window.location.href = "Fetch.aspx?type='" + s + "'";
                            sessionStorage.setItem("notepad", "1");
                        }
                        else if (a1[0] == "search" && counter=="1" && np=="0")
                        {
                            window.location.href = "Fetch.aspx?type='" + s + "'";
                        }
                            else if (s == "Scroll down" || s=="scroll down" || s=="Scroll Down"|| s == "Scroll up" || s=="scroll up" || s=="Scroll Up" && np=="0")
                        {
                            window.location.href = "Fetch.aspx?type='" + s + "'";
                        }
                        else if (s == "close tab" || s == "Close window" )
                        {
                            window.location.href = "Fetch.aspx?type='" + s + "'";
                        }
                        else if (a1[0] == "open" || s == "open new window")
                        {
                            window.location.href = "Fetch.aspx?type='" + s + "'";
                        }
                        else if (a1[0] == "hello" || a1[0] == "Hello" && a1[1] == "Jarvis" || a1[1] == "Jarwis")
                        {
                            window.location.href = "Fetch.aspx?type='" + s + "'";
                            sessionStorage.setItem("code", "1");
                        }
                        else if (a1[0] == "Hi" || a1[0] == "Hello" || a1[0] == "hello" || a1[0] == "hi" || a1[0] == "Hey" || a1[0] == "hey")
                        {

                            window.location.href = "Fetch.aspx?type='" + s + "'";
                            
                        }
                        else if (s == "Who are you" || s == "who are you")
                        {
                            window.location.href = "Fetch.aspx?type='" + s + "'";
                        }
                        else if (s == "How are you" || s == "how are you")
                        {
                            window.location.href = "Fetch.aspx?type='" + s + "'";
                        }
                        else if (s == "Who made you" || s == "who made you")
                        {
                            window.location.href = "Fetch.aspx?type='" + s + "'";
                        }
                        else if (s == "What can you do" || s == "what can you do" || s == "Which operations can you perform" || s == "which operations can you perform")
                        {
                            window.location.href = "Fetch.aspx?type='" + s + "'";
                        }
                        else if (a1[0] == "create" && a1[1] == "folder" && counter=="1") {
                            window.location.href = "Fetch.aspx?type='" + s + "'";
                            //sessionStorage.setItem("code", "0");
                        }
                        else if (a1[0] == "I" || a1[0]=="i")
                        {
                            if (a1[1] == "Love" || a1[1] == "love")
                            {
                                if (a1[2] == "You" || a1[2]== "you")
                                {
                                    window.location.href = "Fetch.aspx?type='" + s + "'";
                                }
                            }
                        }
                        else if (a1[0] == "Who" || a1[0] == "who")
                        {
                            if (a1[1] == "killed" || a1[1] == "Killed")
                            {
                                if (a1[2] == "thanos" || a1[2] == "Thanos")
                                {
                                    window.location.href = "Fetch.aspx?type='" + s + "'";
                                }
                            }
                        }
                        else if (a1[0] == "Who" || a1[0] == "who")
                        {
                            if (a1[1] == "Died" || a1[1] == "died")
                            {
                                if (a1[2] == "in" || a1[2] == "In")
                                {
                                    if (a1[3] == "End" || a1[3] == "end")
                                    {
                                        if (a1[4] == "game" || a1[4] == "Game")
                                        {

                                            window.location.href = "Fetch.aspx?type='" + s + "'";
                                        }
                                    }

                                }
                            }
                        }
                        else if (s == "Tell me a Joke" || s == "tell me a joke" || s == "Tell me a joke")
                        {
                            window.location.href = "Fetch.aspx?type='" + s + "'";
                        }
                        else if (s == "One more" || s == "one more" || s == "one More")
                        {
                            window.location.href = "Fetch.aspx?type='" + s + "'";
                        }
                        else if (a1[0] == "calculate" && counter == "1") {
                            if (s.includes("+") || s.includes("plus")) {
                                s = s.replace("+", "plus");
                            }
                            else if (s.includes("-")) {
                                s = s.replace("-", "minus");
                            }
                            else if (s.includes("*")) {
                                s = s.replace("*", "multiply");
                            }
                            else if (s.includes("divide"))
                            {
                                s = s.replace("divide", "/");
                            }
                            window.location.href = "Fetch.aspx?type='" + s + "'";
                            //sessionStorage.setItem("code", "0");
                        }
                        else if (a1[0] == "Close" || a1[0] == "close" && sze>1)
                        {
                            var file = sessionStorage.getItem("open_explorer");
                            if (a1[1] == "Notepad" || a1[1] == "notepad")
                            {
                                window.location.href = "Fetch.aspx?type='" + s + "'";
                                sessionStorage.setItem("open_note", "0");
                            }
                            else if (a1[1] == "Paint" || a1[1] == "paint" && sessionStorage.getItem("open_paint")=="1")
                            {
                                window.location.href = "Fetch.aspx?type='" + s + "'";
                                sessionStorage.setItem("open_paint", "0");
                            }
                            else if (a1[1] == "Chrome" || a1[1] == "chrome" && sessionStorage.getItem("open_chrome")=="1")
                            {
                                window.location.href = "Fetch.aspx?type='" + s + "'";
                                sessionStorage.setItem("open_chrome", "0");
                            }
                            else if (a1[1] == "File" || a1[1] == "file" && a1[2] == "explorer" || a1[2] == "Explorer" && file == "1")
                            {
                                window.location.href = "Fetch.aspx?type='" + s + "'";
                                sessionStorage.setItem("open_explorer", "0");
                            }
                            else if (a1[1] == "word" || a1[1] == "Word" && sessionStorage.getItem("open_word")=="1")
                            {
                                window.location.href = "Fetch.aspx?type='" + s + "'";
                                sessionStorage.setItem("open_word", "0");
                            }
                            else if (a1[1] == "powerpoint" || a1[1] == "Powerpoint" && sessionStorage.getItem("open_ppt")=="1")
                            {
                                window.location.href = "Fetch.aspx?type='" + s + "'";
                                sessionStorage.setItem("open_ppt", "0");
                            }
                            else if (a1[1] == "excel" || a1[1] == "Excel" && sessionStorage.getItem("open_excel")=="1")
                            {
                                window.location.href = "Fetch.aspx?type='" + s + "'";
                                sessionStorage.setItem("open_excel", "1");
                            }
                            else if (a1[1] == "calculator" || a1[1] == "Calculator" && sessionStorage.getItem("open_calc")=="1")
                            {
                                window.location.href = "Fetch.aspx?type='" + s + "'";
                                sessionStorage.setItem("open_calc", "1");
                            }
                            else if (a1[1] == "tab" || a1[1] == "Tab" || a1[1] == "window" || a1[1] == "Window")
                            {
                                window.location.href = "Fetch.aspx?type='" + s + "'";    
                            }
                            else
                            {
                                window.location.href = "Fetch.aspx?type='command not found'";
                            }
                            sessionStorage.setItem("open", "0");  
                        }
                        else if (rem == "1" || sessionStorage.getItem("remember")=="1")
                        {
                            if (a1[0] !="")
                            {
                                sessionStorage.setItem("remember", "0");
                                sessionStorage.setItem("ename", "1");
                                window.location.href = "Fetch.aspx?type='" + s + "'";
                            }
                            else
                            {                                
                                sessionStorage.setItem("remember", "0");
                                window.location.href = "Fetch.aspx?type='" + s + "'";
                            }
                        }
                        else if (event_name == "1" || sessionStorage.getItem("ename")=="1")
                        {
                            if (a1[0] != "") {
                                sessionStorage.setItem("ename", "0");
                                sessionStorage.setItem("evalue", "1");
                                window.location.href = "Fetch.aspx?type='" + s + "'";
                            }
                            else
                            {
                                sessionStorage.setItem("ename", "0");
                                window.location.href = "Fetch.aspx?type='" + s + "'";
                            }
                        }
                        else if (event_value == "1" || sessionStorage.getItem("evalue")=="1")
                        {
                            if (a1[0] != "") {
                                sessionStorage.setItem("evalue", "0");
                                window.location.href = "Fetch.aspx?type='" + s + "'";
                            }
                            else
                            {
                                sessionStorage.setItem("evalue", "0");
                                window.location.href = "Fetch.aspx?type='" + s + "'";
                            }
                        }                        
                        else
                        {
                            
                            window.location.href = "Fetch.aspx?type='command not found'";
                            sessionStorage.setItem("notepad", "0");
                        }
                        //counter = 0;
                   }, 3500);
                };
                
                $('#startRecognition').click(function ()
                {
                     $("#<%=Label2.ClientID%>").text("on");
                    $("#<%=startRecognition.ClientID%>").css("display", "none");
                    $("#recording").css("display", "none");
                    $("#txtArea").css("display", "initial");
                    $("#<%=stopRecognition.ClientID%>").css("display", "initial");
                    $('#txtArea').focus();
                    recognition.start();
                    return false;
                });
               $('#stopRecognition').click(function ()
                {
                    $("#<%=Label2.ClientID%>").text("off");
                    $("#txtArea").css("display", "none");
                    $("#recording").css("display", "initial");
                    $("#<%=startRecognition.ClientID%>").css("display", "initial");
                    $("#<%=stopRecognition.ClientID%>").css("display", "none");
                    recognition.stop();
                    return false;
                });
                 var action = $("#<%=Label1.ClientID %>").text();
                if (action != "")
                {
                    $("#txtArea").css("display", "initial");
                    $("#<%=Label2.ClientID%>").text("on");
                    recognition.start();
                    $("#<%=startRecognition.ClientID%>").css("display", "none");
                    $("#recording").css("display", "none");
                    $("#<%=stopRecognition.ClientID%>").css("display", "initial");
                }
                else
                {
                    $("#txtArea").css("display", "none");
                    $("#<%=Label2.ClientID%>").text("off");
                     $("#recording").css("display", "initial");
                    $("#<%=startRecognition.ClientID%>").css("display", "initial");
                    $("#<%=stopRecognition.ClientID%>").css("display", "none");
                }
            });
           
    </script> 
    </head>
    <body>
       
       <div class="whole">
      <form runat="server">

        <asp:Label ID="Label1" runat="server" Text="Label" style="display: none;"></asp:Label>
        <asp:Label ID="status" runat="server" Text="Label" style="display: none;"></asp:Label>
        <%--<asp:HiddenField ID="HiddenField1" runat="server" />--%>
          
        <div class="top">            
            <div class="text_top">
                Akamaii
            </div>
        </div>
          <div class="app">
            <center>
                <div class="recog">Speech Recognition</div><br><br>
                
                <asp:Panel HorizontalAlign="Center" runat="server">
                 
                    <asp:LinkButton ID="startRecognition" title="Start Recording"  runat="server"><i class="fa fa-microphone" style="font-size: 50px; color: indianred;"></i></asp:LinkButton>
                    <asp:LinkButton ID="stopRecognition" runat="server" style="display:none" title="Pause Recording"><i class="fa fa-stop" style="font-size: 50px; color: indianred;"></i></asp:LinkButton>
                    <br/>
                    <br/>   
                    <textarea id="txtArea" style="display: none;"></textarea>
                    <asp:Label ID="Label2" runat="server" Text="off" style="display: none;"></asp:Label>
                    <br/>
                </asp:Panel>
                <br/>
                <p id="recording">Press the <strong>Start Recognition</strong> button and
                    allow access.</p>
            </center>
            </div>
          </div>
</form>
    </body>
</html>
