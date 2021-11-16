function AjaxSend(form) {
    if ($("#txtTitle").val().length <= 10) {
        console.log(form.action)

        alert("Title can't less than 10 character!")
    }
    let data = new FormData(form);
    if (editor.getData().length >= 255) {
        data.set("article_content", editor.getData())

    } else {
        alert("Content can't less than 255 character!");
        return false;
    }
    if ($("#txtTitle").val().length >= 10 && editor.getData().length >= 255) {
        $.ajax({
            type: "POST",
            url: form.action,
            contentType: false,
            processData: false,
            beforeSend: () => {
                $(".preloader").fadeIn(1000);
            },
            data: data,
            success: (response) => {
                if (response) {
                    $(".preloader").fadeOut(1000);
                    alert("Add Article Success!")
                    window.location.assign(window.location.href)
                } else {
                    alert("Add Faild Something err")
                }
            },
           
            error: () => {
                alert("Some thing err")
            }
        })
    }
    return false;
}

function updateArticle(form) {
    
}
function loadDataForUpdate(data) {

    $("#txtTitle_up").val(data.result.article_title);
    editor_up.setData(data.result.article_content);
    $("#category_up").val(data.result.category_id);
    var thumSrc = data.result.article_thumbnail;
    thumSrc = thumSrc.slice(1, thumSrc.length);
    $("#currentThubm").attr("src", thumSrc);
    console.log(data)
    $(".preloader").fadeOut();
    $("#updateModal").modal("show");
}
function getDataToShowModal(article_id) {

    $("#updateModal").modal("hide");
    $(".preloader").fadeIn(1000);
    $.ajax({
        type: "POST",
        url: "GetArticleData",
        dataType: "json",
        data: jQuery.param({ id: article_id }),
        success: (data) => {
            if (data != null) {
                var data_ = data;
                loadDataForUpdate(data_)
            }
            else{
                $(".preloader").fadeOut(200);
                alert("Something err a");
            }
        },
        error: () => {
            $(".preloader").fadeOut(200);
            alert("Something err");
        }
    })
}
var editBtn = $(".editor");
editBtn.click((e) => {
    $("#txtId").val(e.target.dataset.id)
    getDataToShowModal(e.target.dataset.id);
})
var btnRemove = $(".remove");
function removeArticle(article_id) {
    $.ajax({
        type: "POST",
        url: "RemovedArticle",
        data: jQuery.param({ id: article_id }),
        beforeSend: () => {
            $(".preloader").show();
        },
        success: (response) => {
            $(".preloader").hide();
            if (response) {
                window.location.assign(window.location.href);
            }
        }
    })
}
btnRemove.click((e) => {

    if (confirm("Do You Really Remove this Article ?")) {
        removeArticle(e.target.dataset.id);
    }
})
$(function () {
    "use strict";
    // ============================================================== 
    // Newsletter
    // ============================================================== 
    var totalUser = $("#totalUsers").data("value");
    var totalArticle = $("#totalArticles").data("value");
    var totalComment = $("#totalComments").data("value");
    var new_series = [];
    for (var j = 0; j < 3; j++) {
        var change = totalArticle;
        if (j > 0) { change = totalUser;} else if (j > 1) { change = totalComment; }
        var new_array = [];
        for (var i = 0; i <= 7; i++) {
            new_array.push(Math.floor(Math.random() * change) + 1)
        }
        new_series.push(new_array);
    }
    var data = {
        labels: ['2014', '2015', '2016', '2017', '2018', '2019', '2020', '2021'],
        series: new_series
    };
    var option = {
        top: 0,
        low: 1,
        showPoint: true,
        fullWidth: true,
        plugins: [
            Chartist.plugins.tooltip()
        ],
        axisY: {
            labelInterpolationFnc: function (value) {
                return (value / 1) + 'k';
            }
        },
        showArea: true
    };
    //ct-visits
    new Chartist.Line('#ct-visits', data, option);


    var chart = [chart];

    var sparklineLogin = function () {
        $('#sparklinedash').sparkline([0, 5, 6, 10, 9, 12, 4, 9], {
            type: 'bar',
            height: '30',
            barWidth: '4',
            resize: true,
            barSpacing: '5',
            barColor: '#7ace4c'
        });
        $('#sparklinedash2').sparkline([0, 5, 6, 10, 9, 12, 4, 9], {
            type: 'bar',
            height: '30',
            barWidth: '4',
            resize: true,
            barSpacing: '5',
            barColor: '#7460ee'
        });
        $('#sparklinedash3').sparkline([0, 5, 6, 10, 9, 12, 4, 9], {
            type: 'bar',
            height: '30',
            barWidth: '4',
            resize: true,
            barSpacing: '5',
            barColor: '#11a0f8'
        });
        $('#sparklinedash4').sparkline([0, 5, 6, 10, 9, 12, 4, 9], {
            type: 'bar',
            height: '30',
            barWidth: '4',
            resize: true,
            barSpacing: '5',
            barColor: '#f33155'
        });
    }
    var sparkResize;
    $(window).on("resize", function (e) {
        clearTimeout(sparkResize);
        sparkResize = setTimeout(sparklineLogin, 500);
    });
    sparklineLogin();
});
var editor = CKEDITOR.replace('articleContent_add', {
    customConfig: '/Public/Admin/plugins/ckeditor/config.js',
});
var editor_up = CKEDITOR.replace('articleContent_up', {
    customConfig: '/Public/Admin/plugins/ckeditor/config.js',
});