


$(document).ajaxStop(function () {
    var toastrget = new XMLHttpRequest();
    toastrget.open("GET", "/AdminPanel/home/_toastr?ftimestamp=" + new Date().getTime(), false);
    toastrget.send(null);
    $("#alertDiv").html(null);
    $("#alertDiv").html(toastrget.responseText);

    if ($("#elementList").length) {
        var listget = new XMLHttpRequest();
        var url = $("#elementList").attr("link") + "?ftimestamp=" + new Date().getTime();
        listget.open("GET", url, false);
        listget.send(null);
        $("#elementList").html(null);
        $("#elementList").html(listget.responseText);
    }

});



//Modal init
var modalDiv = $("#bootstrap-Modal");
var modalContent = modalDiv.find(".block-content");
var modalTitle = modalDiv.find(".block-title");
var modalConfirm = modalDiv.find(".btn-primary");
var modalSize = modalDiv.children();

// Modal Action
function ActionElement(event) {
    var item = $(event);
    var action = item.attr("link");
    var title = item.attr("title");
    var confirmText = item.attr("confirmText");
    var sizeModal = item.attr("modalSize");
    var form = item.attr("form");

    $.get(action, function (data) {
        modalSize.addClass(sizeModal);
        modalDiv.modal("show");
        modalContent.html(data);
        modalTitle.html(title);
        modalConfirm.attr("link", action);
        modalConfirm.attr("form", form);
        modalConfirm.attr("onclick", "Confirm(this)");
        modalConfirm.html(confirmText);
    });

};

//Confirm button action
function Confirm(event) {
    var item = $(event);
    var form = $("#" + item.attr("form"));
    if (!form.length) {
        $.post(modalConfirm.attr("link"), function (data) {
            if (data === "True") {
                modalDiv.modal("hide");
            }
        });
    } else {
        if (form.valid()) {
            $.post(modalConfirm.attr("link"), form.serializeArray(), function (data) {
                if (data === "True") {
                    modalDiv.modal("hide");
                }
            });
        } else {
            toastr.warning("Alanları Kontrol Ediniz!");
        }
    }
};

// cancel button action
modalDiv.find(".btn-default").on("click", function () { modalDiv.modal("hide") });


