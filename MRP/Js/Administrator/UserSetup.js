var tableUserList = null;

function loadTable_UserList(reqTableUserListUrl, authToken) {
    tableUserList = $("#Table_UserList").DataTable(
        {
            "processing": true,
            "serverSide": false,
            "ajax":
            {
                "url": reqTableUserListUrl,
                "contentType": "application/json",
                "type": "Post",
                "dataType": "JSON",
                "beforeSend": function (xhr) {
                    xhr.setRequestHeader("Authorization",
                        "Bearer " + authToken);
                },
                "data": function (d) {
                    return d;
                }
            },
            "columns": [
                {
                    "data": "AccessId",
                    "visible": false,
                },
                { "data": "Name" },
                { "data": "LoginId" },
                { "data": "EmailAddress" },
                { "data": "Role" },
                { "data": "Status" }
                //{
                //    "data": "RoleStatus",
                //    "mRender": function (data, type, full) {
                //        if (data === 0)
                //            return "Active";
                //        else
                //            return "Inactive";
                //    }
                //}
            ],
            "initComplete": function (settings, json) {
                //draw tooltip after the table complete
                $('[data-toggle="tooltip"]').tooltip();
            },
            "searchable": true,
            "orderable": true,
            "responsive": true,
        }
    );
}

function addNewUser() {
    var dataStr = {
        loginID: $("#inputAddNewUser_LoginID").val(),
        staffNumber: $("#inputAddNewUser_StaffNumber").val(),
        staffName: $("#inputAddNewUser_Name").val(),
        email: $("#inputAddNewUser_EmailAddress").val(),
        telno: $("#inputAddNewUser_PhoneNumber").val(),
        role: $(`#${selectAddNewUser_Roles}`).val(),
        status: $(`#${selectAddNewUser_Status}`).val(),
        password: $("#inputAddNewUser_Password").val(),
        userGroupID: $(`#${selectAddNewUser_UserTeam}`).val(),
        departmentID: $(`#${selectAddNewUser_Department}`).val(),
        designation: $("#inputAddNewUser_Designation").val()
    }

    return new Promise((resolve, error) => {
        $.ajax({
            url: reqSaveAddNewUserUrl,
            type: 'Post',
            dataType: 'json',
            data: JSON.stringify(dataStr),
            headers: {
                "Content-Type": 'application/json; charset=utf-8',
                "Authorization": "Bearer " + authToken,
            },
            success: function (data, status, xhr) {
                console.log(data)
                resolve();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                error(xhr);
            }
        })
    })
}