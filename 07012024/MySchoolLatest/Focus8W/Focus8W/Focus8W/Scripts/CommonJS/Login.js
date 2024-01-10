var url = '';

var LOGIN = {
    Login: function (ele) {
        debugger
        
        var userid = document.getElementById("txtUserId").value;
        var userpwd = document.getElementById("txtPwd").value;
        var usertype = parseInt(document.getElementById("ddlType").value);

        url = COREJS.ServerUrl("Login", "Home", "");
        $.ajax({
            url: url,
            cache: false,
            datatype: "html",
            type: "Post",
            data: {
                sUserId: userid,
                sUserPassword: userpwd,
                iUserType: usertype
            },
            success: function (data) {
                LOGIN.successLogin(true, data)
            }
        })
    },
    successLogin: function (bSuccess, data) {
        debugger;
        if (bSuccess) {
            if (data == 'Not valid user.') {
                document.getElementById("lblStatus").textContent = data;
                return;
            } else {
                //document.getElementById("dvBody").innerHTML = '';
                //document.getElementById("dvBody").innerHTML = data;
                $("#dvRenderBody").html('');
                $("#dvRenderBody").html(data);
            }
        }
    },
}
