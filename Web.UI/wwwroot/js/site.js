$(".msg-history").animate({
    scrollTop: $(".msg-history").prop("scrollHeight")
});

$("button.btn.msg").click(function () {
    var umsg = $("#taMsg").val();
    if (umsg != "") {
        fnAppendUserMessage(umsg);
    }
    $("#taMsg").val("");
});

function fnAppendUserMessage(umsg) {
    var $msg = $("#msg-template").clone();
    var dt = new Date($.now());

    var d = (`0${dt.getDate()}`).slice(-2);
    var mn = (`0${(dt.getMonth() + 1)}`).slice(-2);
    var h = (`0${dt.getHours()}`).slice(-2);
    var m = (`0${dt.getMinutes()}`).slice(-2);
    var s = (`0${dt.getSeconds()}`).slice(-2);

    var dateStr = `${d}/${mn}/${dt.getFullYear()} ${h}:${m}:${s}`;
    $("#measureString").html(dateStr);

    $msg.find(".message-text").html(umsg);
    $msg.find(".message-timestamp").html(dateStr);
    $msg.find(".message-title").html("You:");

    $msg.find(".message-text").closest("div").css("min-width", `${$("#measureString").width()}px`);

    $(".msg-history > div").append($msg.html());
    fnScrollHistoryToBottom();

    fnSendMessage(umsg).then(res => fnAppendClientMessage(res));
}

function fnScrollHistoryToBottom() {
    $(".msg-history").animate({
        scrollTop: $(".msg-history").prop("scrollHeight")
    });
}

async function fnSendMessage(umsg) {
    var req = new Request("/SendMessage");
    var bodyStr = JSON.stringify(umsg);

    return await fetch(req, {
            method: 'POST',
            headers: {
                accept: 'application.json',
                'Content-Type': 'application/json'
            },
        body: bodyStr
        })
        .then(res => res.json());
}

function fnAppendClientMessage(cmsg) {
    var $msg = $("#msg-template").clone();

    $msg.find("section.messenger").closest("div").removeClass("justify-content-end").addClass("justify-content-start");
    $msg.find("section").removeClass("messenger").addClass("client-messenger");

    var dt = new Date($.now());

    var d = (`0${dt.getDate()}`).slice(-2);
    var mn = (`0${(dt.getMonth() + 1)}`).slice(-2);
    var h = (`0${dt.getHours()}`).slice(-2);
    var m = (`0${dt.getMinutes()}`).slice(-2);
    var s = (`0${dt.getSeconds()}`).slice(-2);

    var dateStr = `${d}/${mn}/${dt.getFullYear()} ${h}:${m}:${s}`;
    $("#measureString").html(dateStr);

    $msg.find(".message-text").html(cmsg);
    $msg.find(".message-timestamp").html(dateStr);
    $msg.find(".message-title").html("Client:");

    $msg.find(".message-text").closest("div").css("min-width", `${$("#measureString").width()}px`);

    $(".msg-history > div").append($msg.html());
    fnScrollHistoryToBottom();
}

$(function () {
    if ($("main").find("section.msg-wrapper-row").length > 0)
        $("body").css("background-color", "#002921");
    else
        $("body").css("background-color", "unset");
});