

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


$("form").on("change", ".file-upload-field", function () {
    $(this).parent(".file-upload-wrapper").attr("data-text", $(this).val().replace(/.*(\/|\\)/, ''));
});


$("input[name='star']").click((e) => {
    var res = document.getElementById("result");
    res.value = e.currentTarget.className[10];
  

    //document.getElementById("whoreIdTxt").value = elem.getAttribute("name").split("&&")[0];
    //document.getElementById("userIdTxt").value = elem.getAttribute("name").split("&&")[1];

    
});

$("#ratingContainer").hide();


$("#ratingBtn").click((e) => {

    

    $("#ratingContainer").show();

    $("#whoreIdTxt").val(e.currentTarget.getAttribute("name").split("&&")[0]);
    $("#userIdTxt").val(e.currentTarget.getAttribute("name").split("&&")[1]);

    $('#ratingContainer').bPopup({
        speed: 650,
        transition: 'slideIn',
        transitionClose: 'slideBack'
    });

})
    
