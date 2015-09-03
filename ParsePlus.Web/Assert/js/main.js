﻿
$(function () {
    $('.file-upload').change(function () {
        var div = $(this).closest('.part');
        var dataDiv = div.find(".data");
        div.find(".loading").fadeIn().css("display", "inline-block").html("yükleniyor...");
        $('.form-upload').ajaxForm({
            success: function (data) {
                dataDiv.empty();
                
                div.find("#SourceFile").val(data.SourceFilePath);

                $.each(data.XmlNodes, function (i) {
                    dataDiv.append("<div class='tag-title'>" + data.XmlNodes[i].NodeName + "</div>");
                    $.each(data.XmlNodes[i].SubNodeList, function (k) {
                        dataDiv.append("<div class='tag-element'>" + data.XmlNodes[i].SubNodeList[k].NodeName + "</div>");
                    });
                });
            }
        }).submit();
        div.find(".loading").fadeOut();
    });

    $('body').on("click", ".data .tag-element", function () {
        $.ajax({
            url: "/Home/GetElementValues",
            data: { sourceFilePath: $('#SourceFile').val(), elementName: $(this).html() },
            success: function (data) {
                $('.element-values').empty();

                $.each(data, function (i) {
                    $('.element-values').append("<div class='value'>" + data[i] + "</div>");
                });
            },
            error: function (ex) { popup("Hata oluştu. Durum: " + ex, "Uyarı"); }
        });
    });
});