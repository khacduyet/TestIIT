$("#click-send").click(function () {
    var name = $("#nameContact").val();
    var mail = $("#emailContact").val()
    var msg = $("#msgContact").val();
    var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    if (!mailformat.test($("#emailContact").val())) {
        toastr.error("Email invalid!", "Notification!");
        return false;
    }
    if ($("#nameContact").val().length == 0) {
        toastr.error("Name not null!", "Notification!");
        return false;
    }
    if ($("#msgContact").val().length == 0) {
        toastr.error("Message not null!", "Notification!");
        return false;
    }
    $.ajax({
        type: "POST",
        url: "/Contact/sendContact",
        data: { Name: name, Email: mail, Message: msg },
        success: function () {
            toastr.success(
                'Your has send contact success!',
                'Notification!',
                {
                    timeOut: 1000,
                    fadeOut: 1000,
                    onHidden: function () {
                        window.location.reload();
                    }
                });
        }
    });
});
$("#btnBook").click(function () {
    $("#mcid").val($("#cid").val());
    $("#mcod").val($("#cod").val());
    $("#qmadult").val($("#qadult").val());
    $("#qmchild").val($("#qchild").val());
});
$("#submit-book").click(function () {
    var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    var phoneformat = /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/;
    var d = new Date();
    var strDate = d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate();
    var vld = [
        $("#mname").val(),
        $("#memail").val(),
        $("#mphone").val(),
        $("#midentity").val(),
        $("#qmroom").val(),
        $("#mcid").val(),
        $("#mcod").val(),
        $("#qmadult").val(),
        $("#qmchild").val()
    ];
    for (var i = 0; i < vld.length; i++) {
        if (vld[i].length == 0) {
            toastr.error("Please fill out all fields!", "Notification!");
            return false;
        }
    }
    if (!mailformat.test($("#memail").val())) {
        toastr.error("Email invalid!", "Notification!");
        return false;
    }
    if (!phoneformat.test($("#mphone").val())) {
        toastr.error("Phone invalid!", "Notification!");
        return false;
    }
    if ($("#midentity").val().length > 12) {
        toastr.error("Identity invalid!", "Notification!");
        return false;
    }
    if ($("#mcid").val() < strDate) {
        toastr.error("Checkin date invalid!", "Notification!");
        return false;
    }
    if ($("#mcid").val() > $("#mcod").val()) {
        toastr.error("Checkout date invalid!", "Notification!");
        return false;
    }

    var obj = {
        Name: $("#mname").val(),
        Email: $("#memail").val(),
        Phone: $("#mphone").val(),
        Identity: $("#midentity").val(),
        Address: $("#maddress").val(),
        IdTypeRoom: $("#mroomtype").val(),
        Qroom: $("#qmroom").val(),
        CID: $("#mcid").val(),
        COD: $("#mcod").val(),
        Qadult: $("#qmadult").val(),
        Qchild: $("#qmchild").val(),
        Note: $("#mnote").val()
    };
    $.ajax({
        type: "POST",
        url: "/Home/PostBook",
        data: obj,
        success: function (rs) {
            toastr.success(
                'Book room success, please agent staff hotel contact you!',
                'Notification!',
                {
                    timeOut: 1000,
                    fadeOut: 1000,
                    onHidden: function () {
                        window.location.reload();
                    }
                });
        },
        error: function (msg) {
            alert("fail");
        }
    });
});

function validform() {
    var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    var phoneformat = /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/;

    document.getElementById("mname").addEventListener("keyup", function (e) {
        if ($("#mname").val().length == 0) {
            $('#mname').addClass("is-invalid");
            $('.error-name').css('display', 'block');
            $('.error-name').html("Fill in your name!");
        } else {
            $('#mname').removeClass("is-invalid");
            $('#mname').addClass("is-valid");
            $('.error-name').css('display', 'none');
        }
    });
    document.getElementById("memail").addEventListener("keyup", function (e) {
        if ($("#memail").val().length == 0) {
            $('#memail').addClass("is-invalid");
            $('.error-email').css('display', 'block');
            $('.error-email').html('Email not null!');
        } else if (!mailformat.test($("#memail").val())) {
            $('#memail').addClass("is-invalid");
            $('.error-email').css('display', 'block');
            $('.error-email').html('Not a valid e-mail address!');
        } else {
            $('#memail').removeClass("is-invalid");
            $('#memail').addClass("is-valid");
            $('.error-email').css('display', 'none');
        }
    });
    document.getElementById("mphone").addEventListener("keyup", function (e) {
        if ($("#mphone").val().length == 0) {
            $('#mphone').addClass("is-invalid");
            $('.error-phone').css('display', 'block');
            $('.error-phone').html("Phone not null!");
        } else if (!phoneformat.test($("#mphone").val())) {
            $('#mphone').addClass("is-invalid");
            $('.error-phone').css('display', 'block');
            $('.error-phone').html("Not a valid phone!");
        } else {
            $('#mphone').removeClass("is-invalid");
            $('#mphone').addClass("is-valid");
            $('.error-phone').css('display', 'none');
        }
    });
    document.getElementById("midentity").addEventListener("keyup", function (e) {
        if ($("#midentity").val().length == 0) {
            $('#midentity').addClass("is-invalid");
            $('.error-iden').css('display', 'block');
            $('.error-iden').html("Identity not null!");
        } else if ($("#midentity").val().length > 12) {
            $('#midentity').addClass("is-invalid");
            $('.error-iden').css('display', 'block');
            $('.error-iden').html("Not a valid identity!");
        } else {
            $('#midentity').removeClass("is-invalid");
            $('#midentity').addClass("is-valid");
            $('.error-iden').css('display', 'none');
        }
    });
    document.getElementById("qmroom").addEventListener("keyup", function (e) {
        if ($("#qmroom").val().length == 0) {
            $('#qmroom').addClass("is-invalid");
            $('.error-room').css('display', 'block');
        } else {
            $('#qmroom').removeClass("is-invalid");
            $('#qmroom').addClass("is-valid");
            $('.error-room').css('display', 'none');
        }
    });
    document.getElementById("mcid").addEventListener("change", function (e) {
        var d = new Date();
        var strDate = d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate();
        if ($("#mcid").val() == null) {
            $('#mcid').addClass("is-invalid");
            $('.error-cid').css('display', 'block');
        } else if (strDate > $("#mcid").val()) {
            $('#mcid').addClass("is-invalid");
            $('.error-cid').css('display', 'block');
            $('.error-cid').html("Date Invalid!");
        } else {
            $('#mcid').removeClass("is-invalid");
            $('#mcid').addClass("is-valid");
            $('.error-cid').css('display', 'none');
        }
    });
    document.getElementById("mcod").addEventListener("change", function (e) {
        if ($("#mcod").val() == null) {
            $('#mcod').addClass("is-invalid");
            $('.error-cod').css('display', 'block');
        } else if ($("#mcid").val() > $("#mcod").val()) {
            $('#mcod').addClass("is-invalid");
            $('.error-cod').css('display', 'block');
            $('.error-cod').html("Checkout can not be smaller checkin!");
        } else {
            $('#mcod').removeClass("is-invalid");
            $('#mcod').addClass("is-valid");
            $('.error-cod').css('display', 'none');
        }
    });
    document.getElementById("qmadult").addEventListener("keyup", function (e) {
        if ($("#qmadult").val().length == 0) {
            $('#qmadult').addClass("is-invalid");
            $('.error-adults').css('display', 'block');
        } else {
            $('#qmadult').removeClass("is-invalid");
            $('#qmadult').addClass("is-valid");
            $('.error-adults').css('display', 'none');
        }
    });
    document.getElementById("qmchild").addEventListener("keyup", function (e) {
        if ($("#qmchild").val().length == 0) {
            $('#qmchild').addClass("is-invalid");
            $('.error-childs').css('display', 'block');
        } else {
            $('#qmchild').removeClass("is-invalid");
            $('#qmchild').addClass("is-valid");
            $('.error-childs').css('display', 'none');
        }
    });
};