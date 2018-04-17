function Logout(logoutURL) {
    $.ajax({
        url: logoutURL,
        type: "POST",
        contentType: 'application/json',
        dataType: "json",
        success: function (resp) {
            if (resp.Status === 3) {
                window.location.href = resp.RedirectLocation;
            }
            else {
                alert(resp.Message);
            }
        },
        error: function (resp) {
            alert("Error in processing. Please try again after sometime");
        }
    });
}

function ShowSpinner(isShow, obj) {
    $(obj).append("<div id='spinner' class='spinner' style='display: none;'></div>");

    console.log(obj);

    if (isShow) {
        $("#spinner").show();
    } else {
        $("#spinner").hide('slow');
        $("#spinner").remove();
    }
}

function loading_img(data) {
    if (data) {
        $(document).ajaxSend(function (event, request, settings) {
            //$('#loading').height($('#userlogin').height());
            $('#loading').show();
        });
            $('body').removeClass('loaded');
    }
    else {
        $(document).ajaxComplete(function (event, request, settings) {
            //$('#loading').hide();
        });
            $('body').addClass('loaded');
    }
}

function AuthenticateUser(loginURL) {
    if ($("#userName").val().toLowerCase() === "athlete") {
        window.location = athleteDashboardURL;
    }
    else {
        if ($("#userName").val() === "") {
            alert("User Name is required");
            return false;
        }

        if ($("#userPassword").val() === "") {
            alert("Password is required");
            return false;
        }
        var loginModel = {
            'UserName': $("#userName").val(),
            'Password': $("#userPassword").val()
        };
        loading_img(true);
        $.ajax({
            url: loginURL,
            type: "POST",
            data: JSON.stringify({ "loginModel": loginModel }),
            contentType: 'application/json',
            dataType: "json",
            success: function (resp) {
                if (resp.Status === 3) {
                    window.location = resp.RedirectLocation;
                }
                else if (resp.Status === 4) {
                    $("#hdNavigateUserTo").val(resp.RedirectLocation);
                    $('#loginOption').modal('show');
                }
                else {
                    alert(resp.Message);
                }
                loading_img(false);
            },
            error: function (resp) {
                alert("Error in processing. Please try again after sometime");
                loading_img(false);
            }
        });
    }
}
function NavigateUserToRequiredPage(where) {
    var navigationPaths = $("#hdNavigateUserTo").val();
    $('#loginOption').modal('hide');
    if (where === "admin") {
        window.location = navigationPaths.split('|')[0];
    }
    else {
        window.location = navigationPaths.split('|')[1];
    }
}
function getForgotPasswordUrl(forgotPasswordUrl) {
    $.ajax({
        url: forgotPasswordUrl,
        type: "POST",
        data: JSON.stringify({ "rawQry": $("#resetInputEmail1").val() }),
        contentType: 'application/json',
        dataType: "json",
        success: function (resp) {
            if (resp.Status === 3) {
                alert(resp.Message);
                window.location = resp.RedirectLocation;
            }
            else {
                alert(resp.Message);
            }
        },
        error: function (resp) {
            alert("Error in processing. Please try again after sometime");
        }
    });
}

//Confirmation popup can be used in common
function ConfirmationMessageBox(params) {

    if (!params.confirmationMessage) { params.confirmationMessage = "Are you sure you want to delete this record."; }
    if (!params.popupTitle) { params.popupTitle = "Delete"; }

    var confirmModal =
      $('<div id="confirmDeletion" tabindex="-1" role="dialog" aria-labelledby="confirmDeletionLabel" aria-hidden="true" class="modal fade">' +
        '<div class="modal-dialog">' +
            '<div class="modal-content">' +
                '<div class="modal-header">' +
                        '<button type="button" data-dismiss="modal" aria-hidden="true" class="close">×</button>' +
                '<h4 id="confirmDeletionLabel" class="modal-title">' + params.popupTitle + '</h4>' +
                '</div>' +
                '<div class="modal-body">' +
                    params.confirmationMessage +
                '</div>' +
                '<div class="modal-footer">' +
                    '<button type="button" id="btnCancel" data-dismiss="modal" class="btn btn-default">Cancel</button>' +
                    '<button type="button" id="btnDelete" class="btn btn-primary">OK</button>' +
                '</div>' +
            '</div>' +
        '</div>' +
       '</div>');

    confirmModal.find('#btnDelete').click(function (event) {
        params.callback();
        confirmModal.modal('hide');
    });
    confirmModal.find('#btnCancel').click(function (event) {
        if (params.callbackCancel)
            params.callbackCancel(params.callbackCancelParam);
        confirmModal.modal('hide');
    });

    confirmModal.modal('show');
}
//confirmation popup end


$(document).ajaxStop(function () {
    //console.log("ajaxStop");
    $('body').addClass('loaded');
})

//Get Query string - access the query str var qrystr1 = getUrlVars()["qrystr1"];
function getUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

$(document).ready(function () {
    $("input[type=email]").verimail({});
});