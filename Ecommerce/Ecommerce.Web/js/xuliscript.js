﻿
$(document).ready(function () {

});
function Addcart(value, quantity) {
    var url = "/Xuligiohang/Addcart";
    $.ajax({
        url: url,
        type: 'Get',
        cache: false,
        data: { sp: value, quantity: quantity },
        success: function (result) {
            $('.basket').html(result);
            window.location.href = "/Home/Cart"
        }
    });
}


function Huydonhang(value) {
    var url = "/Home/Huydonhang";
    $.ajax({
        url: url,
        type: 'Get',
        cache: false,
        data: { maDH: value },
        success: function (result) {
            $('.simpleCart_items').html(result);
        }
    });
}
// Cart
function xoagiohang(value) {
    var url = "/Xuligiohang/Xoagiohang";
    $.ajax({
        url: url,
        type: 'Get',
        cache: false,
        data: { index: value },
        success: function (result) {
            $('#cart-page').html(result.toString());
            updatecontentCart();
        }
    });
}
function changequality(value, tt) {

    var id = "quantity+" + value;
    temp = document.getElementById(id).value;
    if (temp > 0 && tt == 'G')
        temp--;
    if (tt == 'T')
        temp++;
    var url = "/Xuligiohang/Thaydoisoluong";
    $.ajax({
        url: url,
        type: 'Get',
        cache: false,
        data: { index: value, value: temp },
        success: function (result) {
            $('#cart-page').html(result.toString());
            updatecontentCart();
        }
    });
}
function updatecontentCart() {
    var url = "/Xuligiohang/UpdategiohangContent";
    $.ajax({
        url: url,
        type: 'Get',
        cache: false,
        success: function (result) {
            $('.basket').html(result);
        }
    });
}
function DangkiSPCM() {
    var url = "/Auction/RegisterProduct";
    $.ajax({
        url: url,
        type: 'POST',
        cache: false,
        data: { a: $(".form-horizontal").serialize() },
        success: function (result) {
            alert("Đăng kí thành công");
        }
    });
}

