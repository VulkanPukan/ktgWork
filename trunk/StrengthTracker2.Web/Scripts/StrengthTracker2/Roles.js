var rolesdataSource = new kendo.data.DataSource({
    transport: {
        read: function (options) {
            $.ajax({
                type: "GET",
                url: "../Security/ListAllRoles",
                contentType: "application/json; charset=utf-8",
                dataType: 'json',
                success: function (result) {
                    options.success(result.Data);
                    $('body').addClass('loaded');
                }
            });
        }
    },
    pageSize: 20
});

$("#rolesGrid").kendoGrid({
    allowCopy: true,
    width: '800px',
    scrollable: true,
    sortable: true,
    filterable: true,
    resizable: true,
    columns: [
        {
            field: "IsActive",
            title: "Status",
            filterable: false,
            attributes: {
                style: 'white-space: nowrap '
            },
            width: 50,
            template: '#if (IsActive==true) {# <i class="fa fa-circle" aria-hidden="true" style="color:green;vertical-align: text-top;"></i> #} else {# <i class="fa fa-circle" aria-hidden="true" style="color:red;vertical-align: text-top;"></i> #}#',
            attributes: {
                style: 'white-space: nowrap;text-align:center;'
            }
        },
        {
            field: "RoleID",
            hidden: true,
            attributes: {
                style: 'white-space: nowrap '
            }
        },
        {
            title: "Name",
            field: "RoleName",
            width: "180px",
            template: '<a href="javascript:void(0)" onclick="ShowEditRoles(#=RoleID#);">#=RoleName#</a>'
        },
        {

            field: "RoleDescription",
            title: "Description",
            width: "300px",
            attributes: {
                style: 'white-space: nowrap '
            }
        },
        {
            field: "Actions",
            title: "Actions",
            filterable: false,
            width: "100px",
            template: '<div class="btn-group mb-sm">' +
                   '<button type="button" data-toggle="dropdown" class="btn btn-info">Select…</button>' +
                   '<button type="button" data-toggle="dropdown" class="btn btn-info dropdown-toggle">' +
                      '<span class="caret"></span>' +
                   '</button>' +
                   '<ul role="menu" class="dropdown-menu" style="min-width:102px !important;">' +
                      '<li><a href="javascript:void(0);" onclick="ShowActivationConfirmationPopup(\'#=RoleID#\');">#if(IsActive==true){# ' + 'Deactivate' + ' #}else{# ' + 'Activate' + '#}#</a></li>' +
                      '<li><a href="javascript:void(0);" onclick="ShowDeleteConfirmationPopup(\'#=RoleID#\');">Delete</a>' +
                      '</li>' +
                   '</ul>' +
                '</div>'
        }
    ],
    dataSource: rolesdataSource
});

rolesdataSource.read();
$("#rolesPager").kendoPager({
    autoBind: true,
    dataSource: rolesdataSource
});

function UpdateRoleStatus() {
    var roleId = $("#confirmActivation").data("id");
    $('#confirmActivation').modal('hide');
    $.ajax({
        url: "../Security/UpdateRoleById",
        type: "POST",
        async: false,
        data: { id: roleId },
        success: function (result) {
            if (result.success) {
                alert(result.message);
                rolesdataSource.read();
            }
            else {
                alert(result.message);
            }
        }, error: function () {
            alert('There was some issue');
        }
    });
}

function DeleteRoles() {
    var roleId = $("#confirmDeletion").data("id");
    $('#confirmDeletion').modal('hide');
    $.ajax({
        url: "../Security/DeleteRoleById",
        type: "POST",
        async: false,
        data: { id: roleId },
        success: function (result) {
            if (result.success) {
                alert(result.message);
                rolesdataSource.read();
            }
            else {
                alert(result.message);
            }

        }, error: function () {
            alert('There was some issue');
        }
    });
}

function ShowDeleteConfirmationPopup(roleID) {
    $("#confirmDeletion").data("id", roleID);
    $('#confirmDeletion').modal('show');
}
function ShowActivationConfirmationPopup(roleID) {
    $("#confirmActivation").data("id", roleID);
    $('#confirmActivation').modal('show');
}
