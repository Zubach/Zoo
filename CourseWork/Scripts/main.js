

$("#pimpsTbl").hide();



function pimpsBtnClick() {
    $("#usersTbl").hide();

    $("#pimpsTbl").show();

    document.getElementById("pimpsBtn").className = "btn btn-primary";
    document.getElementById("usersBtn").className = "btn";
}


function usersBtnClick() {

    $("#usersTbl").show();

    $("#pimpsTbl").hide();

    document.getElementById("pimpsBtn").className = "btn";
    document.getElementById("usersBtn").className = "btn btn-primary";

}
